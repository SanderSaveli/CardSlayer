using UnityEngine;
using UnityEngine.SceneManagement;

namespace Services.StorageService
{
    public class SceneLoader : MonoBehaviour
    {
        private static SceneLoader _instance;
        public static SceneLoader instance
        {
            get
            {
                if (_instance == null)
                {
                    GameObject gameObject = new GameObject('[' + typeof(SceneLoader).Name + ']');
                    _instance = gameObject.AddComponent<SceneLoader>();
                }
                return _instance;
            }
        }

        public ISceneData currentSceneData { get; private set; }

        private void Awake()
        {
            _instance = this;
            DontDestroyOnLoad(this);
        }

        public void LoadScene(string sceneName, ISceneData sceneData = null)
        {
            currentSceneData = sceneData;
            SceneManager.LoadScene(sceneName);
        }
    }
}

