using QFramework;
using QuickLinker.QuickLaunch.Models;
using QuickLinker.QuickLaunch.Utils;
using QuickLinker.Systems;
using System;
using System.Collections.Generic;

namespace QuickLinker.QuickLaunch.Systems
{
    /// <summary>
    /// 对入口进行统一管理,增加入口，平级数据结构(list)，支持任意摆放顺序(固定大小范围)
    /// </summary>
    public class QuickEntitySystem : AbstractSystem, IComparer<Entity>
    {
        /// <summary> 添加入口成功 </summary>
        public EasyEvent<Entity> AddEntityEvent = new EasyEvent<Entity>();
        public EasyEvent<Entity> RemoveEntityEvent = new EasyEvent<Entity>();
        public EasyEvent<Entity> OpenEntityEvent = new EasyEvent<Entity>();
        public EasyIndexEvent<Entity> ChangeEntityEvent = new EasyIndexEvent<Entity>();
        public BindableProperty<int> needSaveNum = new BindableProperty<int>();

        private readonly List<int> removeIndexTempList = new List<int>();//移除的位置列表，用作改动计数(保存后清空)
        private EntityCache entitieCache;
        private readonly Entity DEFAULT = new Entity();
        protected override void OnInit()
        {
            var stroe = this.GetSystem<IStroeSystem>();
            entitieCache = stroe.Load<EntityCache>();
            if (entitieCache == null)
                entitieCache = new EntityCache();
            foreach (var item in entitieCache.entities)
            { 
                item.bitmapImage = ImageUtil.ByteArrToImage(item.imageByteArr);
            }
        }

        /// <summary>
        /// 插入指定位置，index < 0 时代表自动往空余位置加
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="index"></param>
        /// <param name="canParse">解析快捷方式</param>
        /// <returns></returns>
        internal bool Insert(string filePath, int index, bool canParse)
        {
            //先移除指定位置
            Remove(index, false);

            //按指定空位插入Entity
            Entity entity = CommonCode.GetIconInfoByPath(filePath, canParse);
            entity.needSave.Value = true;
            return Insert(entity, index);
        }
        internal bool Insert(Entity entity, int index)
        {
            //从小到大 查找空位
            if (index < 0)
                index = FindZeroIndex();
            entity.index = index;
            var insertIndex = FindInsertIndex(index);
            entitieCache.entities.Insert(insertIndex, entity);
            entity.needSave.RegisterWithInitValue(Event_NeedSave);
            if (removeIndexTempList.Remove(index))
                Event_NeedSave(false);
            ChangeEntityEvent.Trigger(entity.index, entity);
            AddEntityEvent.Trigger(entity);
            return false;
        }

        //移除指定位置的入口，不存在返回false
        internal Entity Remove(int index, bool fireEvent)
        {
            if (index < 0)
                return null;
            DEFAULT.index = index;
            var findIndex = entitieCache.entities.BinarySearch(DEFAULT, this);
            if (findIndex >= 0)
            {
                Entity findEntity = entitieCache.entities[findIndex];
                entitieCache.entities.Remove(findEntity);
                removeIndexTempList.Add(index);
                Event_NeedSave(true);
                ChangeEntityEvent.Trigger(findEntity.index, null);
                if (fireEvent)
                    RemoveEntityEvent.Trigger(findEntity);
                return findEntity;
            }
            return null;
        }

        //从小到大 查找空位
        internal int FindZeroIndex()
        {
            int index = 0;
            foreach (Entity entity in entitieCache.entities)
            {
                if (entity.index != index)
                    return index;
                ++index;
            }
            return index;
        }
        internal int FindInsertIndex(int index)
        {
            var insertIndex = entitieCache.entities.FindLastIndex(item => item.index < index);
            if (insertIndex < 0)
                return 0;
            else
                return insertIndex + 1;
        }

        internal void Copy(int fromIndex, int toIndex)
        {
            Entity fromEntity = Find(fromIndex);
            Remove(toIndex, false);
            if (fromEntity != null)
            {
                var newEntity = (Entity)fromEntity.Clone();
                Insert(newEntity, toIndex);
            }
        }

        //交换指定位置的入口
        internal void Switch(int fromIndex, int toIndex)
        {
            Entity fromEntity = Remove(fromIndex, false);
            Entity toEntity = Remove(toIndex, false);
            if (fromEntity != null)
                Insert(fromEntity, toIndex);
            if (toEntity != null)
                Insert(toEntity, fromIndex);
        }
        internal void Align(int fromIndex, int toIndex)
        {
            //1.从前往后放 从左往右依次移动
            if (fromIndex < toIndex)
            {
                Entity fromEntity = Remove(fromIndex, false);
                for (int i = fromIndex + 1; i <= toIndex; i++)
                {
                    Entity tempEntity = Remove(i, false);
                    if (tempEntity != null)
                        Insert(tempEntity, i - 1);
                }
                if (fromEntity != null)
                {
                    Insert(fromEntity, toIndex);
                }
            }
            //2.从后往前放 从右往左依次移动
            else
            {
                Entity fromEntity = Remove(fromIndex, false);
                for (int i = fromIndex - 1; i >= toIndex; i--)
                {
                    Entity tempEntity = Remove(i, false);
                    if (tempEntity != null)
                        Insert(tempEntity, i + 1);
                }
                if (fromEntity != null)
                {
                    Insert(fromEntity, toIndex);
                }
            }
        }
        //打开指定入口
        internal bool Open(int index)
        {
            Entity entity = Find(index);
            if (entity == null)
                return false;
            if (entity.iconType == Constant.OpenType.OTHER)
                this.GetUtility<IProcessUtil>().RunEntity(entity);
            else
            {
                string protocol = this.GetUtility<IURIUtil>().Protocol;
                if (entity.Path.StartsWith($"{protocol}:"))
                {
                    Uri uri = new Uri(entity.Path);
                    this.GetSystem<ICommandSystem>().RunCommand(uri.LocalPath);
                }
            }
            OpenEntityEvent.Trigger(entity);
            return false;
        }
        //用资源管理器打开入口所在文件夹
        internal bool ShowInExplore(int index)
        {
            Entity entity = Find(index);
            if (entity == null)
                return false;
            this.GetUtility<IProcessUtil>().ShowInExplore(entity);
            return false;
        }
        //执行跟随启动的应用
        internal void AutoStart()
        {
            for (int i = 0; i < entitieCache.entities.Count; i++)
            {
                var entity = entitieCache.entities[i];
                if (!entity.launchOnStartup)
                    continue;
                if (entity.iconType == Constant.OpenType.OTHER)
                    this.GetUtility<IProcessUtil>().RunEntity(entity);
                else
                {
                    string protocol = this.GetUtility<IURIUtil>().Protocol;
                    if (entity.Path.StartsWith($"{protocol}:"))
                    {
                        Uri uri = new Uri(entity.Path);
                        this.GetSystem<ICommandSystem>().RunCommand(uri.LocalPath);
                    }
                }
            }
        }
        internal void Save()
        {
            var needSave = entitieCache.entities.Exists(item => item.needSave.Value) || removeIndexTempList.Count > 0;
            if (!needSave)
                return;
            var stroe = this.GetSystem<IStroeSystem>();
            stroe.Save(entitieCache);

            //清除需保存的状态
            entitieCache.entities.ForEach(item => item.needSave.Value = false);
            for (int i = 0; i < removeIndexTempList.Count; i++)
                Event_NeedSave(false);
            removeIndexTempList.Clear();
        }
        private void Event_NeedSave(bool needSave)
        {
            if (needSave)
                ++needSaveNum.Value;
            else
                --needSaveNum.Value;
        }

        /// <summary>
        /// 查找指定位置的入口信息
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public Entity Find(int index)
        {
            DEFAULT.index = index;
            var findIndex = entitieCache.entities.BinarySearch(DEFAULT, this);
            if (findIndex >= 0)
                return entitieCache.entities[findIndex];
            else
                return null;
        }

        /// <summary>
        /// 查询同时满足多个flag的入口
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public List<Entity> QueryDataWithAllFlags(string[] flags)
        {
            List<Entity> reault = new List<Entity>();
            foreach (var entity in entitieCache.entities)
            {
                var forAll = Array.TrueForAll(flags, flag => Array.Exists(entity.flags, flag.Equals));
                if (forAll)
                    reault.Add(entity);
            }
            return reault;
        }

        /// <summary>
        /// 查询满足查询flag组中的任意一个的入口
        /// </summary>
        /// <param name="flags"></param>
        /// <returns></returns>
        public List<Entity> QueryDataWithAnyFlag(string[] flags)
        {
            if (flags.Length == 0)
                return new List<Entity>(entitieCache.entities);
            List<Entity> reault = new List<Entity>();
            foreach (var entity in entitieCache.entities)
            {
                var exist = Array.Exists(flags, flag => Array.Exists(entity.flags, flag.Equals));
                if (exist)
                    reault.Add(entity);
            }
            return reault;
        }

        public Entity QueryWithGuid(Guid guid)
        {
            foreach (var entity in entitieCache.entities)
            {
                if (entity.guid == guid)
                    return entity;
            }
            return null;
        }

        public int Compare(Entity x, Entity y)
        {
            return x.index.CompareTo(y.index);
        }
    }
}
