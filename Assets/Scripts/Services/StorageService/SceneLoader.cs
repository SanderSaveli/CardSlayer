using UnityEngine.SceneManagement;

namespace Services.StorageService
{
    public class SceneLoader : DontDestroyOnLoadSingletone<SceneLoader>
    {
        private struct ScenePair
        {
            public string sceneName;
            public ISceneData sceneData;
        }
        public ISceneData currentSceneData { get; private set; }
        private ISceneTransistor _sceneTransistor;

        private bool _hasLazyLoading;
        private ScenePair _LazyLoading;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            _sceneTransistor = new SceneTransistor();
        }

        public void LoadScene(string sceneName, ISceneData sceneData = null)
        {
            if (_sceneTransistor.isShowen)
            {
                if (_sceneTransistor.isAnimationPlaying)
                {
                    _LazyLoading.sceneName = sceneName;
                    _LazyLoading.sceneData = sceneData;
                    _hasLazyLoading = true;
                    return;
                }
                _sceneTransistor.ExitTheTransition();
            }
            currentSceneData = sceneData;
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        public void LoadTransistor()
        {
            _sceneTransistor.EnterTheTransition(RunLoadQueue);
        }

        public void RunLoadQueue()
        {
            if (_hasLazyLoading)
            {
                _hasLazyLoading = false;
                LoadScene(_LazyLoading.sceneName, _LazyLoading.sceneData);
            }
        }
    }
}

