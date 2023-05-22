using System;

namespace SaveSystem 
{ 
    public interface IStoregeService
    {
        public void Save(string key, object data, Action<bool> callback);
        public void Load<T>(string key, Action<T> callback);
    }
}

