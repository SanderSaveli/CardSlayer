using System;

namespace Services.StorageService 
{ 
    public interface IStoregeService: IService
    {
        public void Save(string key, object data, Action<bool> callback = null);
        public void Load<T>(string key, Action<T> callback);
    }
}

