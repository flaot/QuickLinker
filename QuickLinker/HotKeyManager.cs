using QFramework;
using QuickLinker.Model;
using QuickLinker.Plugin;
using QuickLinker.Plugin.Events;
using QuickLinker.QuickLaunch.Command;
using QuickLinker.QuickLaunch.Models;
using QuickLinker.QuickLaunch.Systems;
using QuickLinker.Utils;
using System;
using System.Collections.Generic;
using WK.Libraries.HotkeyListenerNS;

namespace QuickLinker
{
    public class HotKeyManager : AbstractSystem
    {
        private HotkeyListener _hotKeyListener;
        internal HotkeyListener HotKeyListener
        {
            get
            {
                if (_hotKeyListener == null)
                    _hotKeyListener = new HotkeyListener();
                return _hotKeyListener;
            }
        }

        private List<QuickAction> hotKeyEntities;

        protected override void OnInit()
        {
            hotKeyEntities = new List<QuickAction>();
        }
        /// <summary>
        /// Initializes the quickaction hotkeys
        /// </summary>
        internal void InitializeQuickActionsHotKeys()
        {
            // first the global hotkey
            InitializeGlobalQuickActionsHotKey();

            // then the individual hotkeys
            InitializeIndividualQuickActionsHotKeys();
        }
        internal void RemoveAllQuickActions()
        {
            _hotKeyListener?.RemoveAll();
        }

        /// <summary>
        /// Reloads the global- and individual quickaction hotkey bindings
        /// </summary>
        internal void ReloadQuickActionsHotKeys()
        {
            // remove all bindings
            HotKeyListener?.RemoveAll();

            // reload
            InitializeQuickActionsHotKeys();
        }

        /// <summary>
        /// Looks up the specific quick action bound to the specified hotkey, and executes it
        /// </summary>
        /// <param name="hotkey"></param>
        internal void ProcessQuickActionHotKey(Hotkey hotkey)
        {
            if (hotkey == null) return;

            var findIndex = hotKeyEntities.FindIndex(x => x.hotKey == hotkey);
            if (findIndex < 0)
            {
                LogKit.W($"[HOTKEY] Registered hotkey no longer bound to a QuickAction: {hotkey}");
                return;
            }
            var quickAction = hotKeyEntities[findIndex];

            Selection.activeContext = null;
            Selection.activeEntity = quickAction.entity;
            TypeEventSystem.Global.Send(new ClickItemPreEvent());
            if (Selection.activeEntity != null)
            {
                ((ISystem)this).GetArchitecture().SendCommand(new QuickEntityOpenCommand() { index = quickAction.entity.index });
                TypeEventSystem.Global.Send(new ClickItemPostEvent());
            }
        }

        private void InitializeGlobalQuickActionsHotKey()
        {
            var appConfig = this.GetModel<AppConfig>();

            // check if it's configured
            if (string.IsNullOrWhiteSpace(appConfig.actionHotKey.Value)) return;
            var hotKey = HotKeyUtil.Convert(appConfig.actionHotKey.Value);
            if (hotKey.ToString() == "None") return;
            // all good, bind
            HotKeyListener?.Add(hotKey);

            LogKit.I("[HOTKEY] Completed bind for global quickaction hotkey");
        }

        private void InitializeIndividualQuickActionsHotKeys()
        {
            var count = 0;
            var quickEntitySystem = this.GetSystem<QuickEntitySystem>();
            hotKeyEntities.Clear();
            foreach (var entity in quickEntitySystem.QueryDataWithAnyFlag(Array.Empty<string>()))
            {
                if (string.IsNullOrWhiteSpace(entity.actionHotKey))
                    continue;
                try
                {
                    var hotKey = HotKeyUtil.Convert(entity.actionHotKey);
                    HotKeyListener?.Add(hotKey);
                    hotKeyEntities.Add(new QuickAction() { entity = entity, hotKey = hotKey});
                    count++;
                }
                catch (Exception ex)
                {
                    LogKit.E(ex.ToString() + $"[HOTKEYS] Unable to bind individual quickaction hotkey '{entity.actionHotKey}': {ex.Message}");
                }
            } 
            if (count == 0) return;
            LogKit.I($"[HOTKEY] Completed bind for {count} individual quickaction hotkeys");
        }

    }

    public struct QuickAction
    {
        public Entity entity;
        public Hotkey hotKey;
    }
}
