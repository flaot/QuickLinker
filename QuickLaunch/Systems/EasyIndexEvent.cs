using QFramework;
using System;
using System.Collections.Generic;

namespace QuickLinker.QuickLaunch.Systems
{
    public class EasyIndexEvent<T> : IEasyEvent
    {
        Dictionary<int, Action<T>> _dicEntityByIndex = new Dictionary<int, Action<T>>();
        private Action<T> mAllOnEvent = e => { };

        public IUnRegister Register(int index, Action<T> onEvent)
        {
            if (index < 0)
            {
                mAllOnEvent += onEvent;
                return new CustomUnRegister(() => { UnRegister(-1, mAllOnEvent); });
            }
            if (!_dicEntityByIndex.TryGetValue(index, out var mOnEvent))
                _dicEntityByIndex.Add(index, mOnEvent = e => { });
            mOnEvent += onEvent;
            _dicEntityByIndex[index] = mOnEvent;
            return new CustomUnRegister(() => { UnRegister(index, onEvent); });
        }
        public void UnRegister(int index, Action<T> onEvent)
        {
            if (index < 0)
            {
                mAllOnEvent -= onEvent;
                return;
            }
            if (!_dicEntityByIndex.TryGetValue(index, out var mOnEvent))
                return;
            mOnEvent -= onEvent;
            _dicEntityByIndex[index] = mOnEvent;
        }
        public void Trigger(int index, T t)
        {
            mAllOnEvent?.Invoke(t);
            if (!_dicEntityByIndex.TryGetValue(index, out var mOnEvent))
                return;
            mOnEvent?.Invoke(t);
        }
        IUnRegister IEasyEvent.Register(Action onEvent)
        {
            return Register(-1, Action);
            void Action(T _) => onEvent();
        }
    }
}
