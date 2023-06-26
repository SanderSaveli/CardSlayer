using Services.StorageService;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Services
{
    public class ServiceLockator : DontDestroyOnLoadSingletone<ServiceLockator>
    {
        private Dictionary<Type, IService> _services = new();

        public void RegisterService<T>(T service) where T:IService
        {
            var type = typeof(T);
            if (_services.ContainsKey(type))
                _services[type] = service;
            else
                _services.Add(type, service);
            service.Initialize();
            Debug.Log("Service " + type + " successfully registered");
        }

        public T GetService<T>() where T : IService
        {
            var type = typeof(T);
            if (!_services.ContainsKey(type))
            {
                _services.Add(type, CreateNullService<T>());
                Debug.LogWarning("Service of type " + type + " was registered and passed a null object.");
            }
            return (T)_services[type];
        }

        private IService CreateNullService<T>() where T : IService
        {
            var type = typeof(T);
            if (type == typeof(IAudioService))
                return new NullAudioService();
            if (type == typeof(IStoregeService))
                return new NullStorageService();
            else 
            {
                Debug.LogWarning("There is no null object for type " + typeof(T));
                return null;
            }
        }
    }
}

