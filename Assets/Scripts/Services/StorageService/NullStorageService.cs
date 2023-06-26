using System;

namespace Services.StorageService
{
    public class NullStorageService : IStoregeService
    {
        public void Initialize()
        { }

        public void Load<T>(string key, Action<T> callback)
        { }

        public void Save(string key, object data, Action<bool> callback = null)
        { }
    }
}

