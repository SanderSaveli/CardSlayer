using Services.StorageService;
using UnityEngine;

namespace Services
{
    public class ServiceRegistrar : MonoBehaviour
    {
        private void Awake()
        {
            ServiceLockator lockator = ServiceLockator.instance;
            //lockator.RegisterService<IStoregeService>(new JSONToFileStorageService());
        }
    }
}

