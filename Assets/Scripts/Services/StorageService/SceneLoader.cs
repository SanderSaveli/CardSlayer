using UnityEngine.SceneManagement;
using UnityEngine;
using System;
using System.Collections.Generic;

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
        private ISceneTransistor sceneTransistor;

        private bool hasLazyLoading;
        ScenePair _LazyLoading;

        private void Awake()
        {
            DontDestroyOnLoad(this);
            sceneTransistor = new SceneTransistor();
        }

        public void LoadScene(string sceneName, ISceneData sceneData = null)
        {
            if (sceneTransistor.isShowen) 
            {
                if (sceneTransistor.isAnimationPlaying) 
                {
                    _LazyLoading.sceneName = sceneName;
                    _LazyLoading.sceneData = sceneData;
                    hasLazyLoading = true;
                    return;
                }
                sceneTransistor.ExitTheTransition();
            }
            currentSceneData = sceneData;
            SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
        }
        public void LoadTransistor() 
        {
            sceneTransistor.EnterTheTransition(RunLoadQueue);
        }

        public void RunLoadQueue() 
        {
            hasLazyLoading = false;
            LoadScene(_LazyLoading.sceneName, _LazyLoading.sceneData);
        }
    }
}

