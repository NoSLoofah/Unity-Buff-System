using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace NoSLoofah.BuffSystem.Dependence
{
    /// <summary>
    /// Mono的懒汉模式单例基类
    /// </summary>
    /// <typeparam name="T">新单例的类</typeparam>
    public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
    {
        protected static T instance;

        public static T GetInstance() => instance;

        protected virtual void Awake()
        {
            if (instance != null)
            {
                Destroy(this.gameObject);
            }
            else
            {
                instance = (T)this;
                DontDestroyOnLoad(this);
            }
        }

    }
}