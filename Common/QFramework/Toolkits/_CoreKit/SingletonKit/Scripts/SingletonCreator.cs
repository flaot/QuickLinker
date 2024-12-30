/****************************************************************************
 * Copyright (c) 2015 - 2022 liangxiegame UNDER MIT License
 * 
 * http://qframework.cn
 * https://github.com/liangxiegame/QFramework
 * https://gitee.com/liangxiegame/QFramework
 ****************************************************************************/


using System;
using System.Reflection;

namespace QFramework
{
    /// <summary>
    /// 普通单例创建类
    /// </summary>
    internal static class SingletonCreator
    {
        static T CreateNonPublicConstructorObject<T>() where T : class
        {
            var type = typeof(T);
            // 获取私有构造函数
            var constructorInfos = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic);

            // 获取无参构造函数
            var ctor = Array.Find(constructorInfos, c => c.GetParameters().Length == 0);

            if (ctor == null)
            {
                throw new Exception("Non-Public Constructor() not found! in " + type);
            }

            return ctor.Invoke(null) as T;
        }

        public static T CreateSingleton<T>() where T : class, ISingleton
        {
            var instance = CreateNonPublicConstructorObject<T>();
            instance.OnSingletonInit();
            return instance;
        }
    }
}