using QFramework;
using QuickLinker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public List<QuickAction> QuickActions;

        protected override void OnInit()
        {
            QuickActions = new List<QuickAction>();
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
        internal void ProcessQuickActionHotKey(string hotkey)
        {
            if (string.IsNullOrEmpty(hotkey)) return;

            // check if we stil have the hotkey bound to a quickaction
            if (QuickActions.All(x => x.HotKey != hotkey))
            {
                LogKit.W("[HOTKEY] Registered hotkey no longer bound to a QuickAction: {hotkey}", hotkey);
                return;
            }

            // fetch the associated quickaction
            var quickAction = QuickActions.Find(x => x.HotKey == hotkey);
            if (quickAction == null)
            {
                LogKit.E("[HOTKEY] Registered hotkey not found: {hotkey}", hotkey);
                return;
            }

            if (!quickAction.HotKeyEnabled)
            {
                LogKit.W("[HOTKEY] QuickAction bound to hotkey has 'hotkey enabled' set to false: {hotkey}", hotkey);
                return;
            }

            //// is it an internal command?
            //if (quickAction.Domain == HassDomain.HASSAgentCommands)
            //{
            //    // execute local command
            //    Task.Run(() => CommandsManager.ExecuteCommandByName(quickAction.Entity));
            //}
            //else
            //{
            //    // execute the command through HA
            //    Task.Run(() => HassApiManager.ProcessQuickActionAsync(quickAction));
            //}
        }

        private void InitializeGlobalQuickActionsHotKey()
        {
            var appConfig = this.GetModel<AppConfig>();

            // check if it's configured
            if (string.IsNullOrWhiteSpace(appConfig.actionHotKey.Value)) return;
            var hotKey = new Hotkey(appConfig.actionHotKey.Value);
            if (hotKey.ToString() == "None") return;
            // all good, bind
            HotKeyListener?.Add(hotKey);

            LogKit.I("[HOTKEY] Completed bind for global quickaction hotkey");
        }

        private void InitializeIndividualQuickActionsHotKeys()
        {
            var count = 0;
            foreach (var quickAcion in QuickActions.Where(x => x.HotKeyEnabled && !string.IsNullOrWhiteSpace(x.HotKey)))
            {
                try
                {
                    HotKeyListener?.Add(new Hotkey(quickAcion.HotKey));
                    count++;
                }
                catch (Exception ex)
                {
                    LogKit.E(ex.ToString() + "[HOTKEYS] Unable to bind individual quickaction hotkey '{hotkey}': {msg}", quickAcion.HotKey, ex.Message);
                }
            }

            if (count == 0) return;
            LogKit.I("[HOTKEY] Completed bind for {count} individual quickaction hotkeys", count);
        }

    }

    public class QuickAction
    {
        public Guid Id { get; set; } = Guid.Empty;
        public string Entity { get; set; }
        public bool HotKeyEnabled { get; set; }
        public string HotKey { get; set; }
        public string Description { get; set; }
    }
}
