using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

namespace SaveSystem
{
    public class JSONToFileStorageService : IStoregeService
    {
        public void Save(string key, object data, Action<bool> callback = null)
        {
            string path = BuildPath(key);
            string json = JsonConvert.SerializeObject(data); 

            using (var file = new StreamWriter(path))
            {
                file.Write(json);
            }

            callback?.Invoke(true);
        }
        public void Load<T>(string key, Action<T> callback)
        {
            string path = BuildPath(key);

            using (var file = new StreamReader(path))
            {
                string json = file.ReadToEnd();
                T data = JsonConvert.DeserializeObject<T>(json);
                if (data == null)
                {
                    Debug.LogWarning("Failed to load data");
                }
                callback.Invoke(data);
            }
        }

        private string BuildPath(string key)
        {
            return Path.Combine(Application.persistentDataPath, key);
        }
    }
}
