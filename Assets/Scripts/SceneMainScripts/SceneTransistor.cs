using System;
using UnityEngine;

public class SceneTransistor : ISceneTransistor
{
    private TransitiorView _transitionView;
    private GameObject _trasistionPrefab;
    private GameObject _trasistionObject;
    private Action _enterCallback;
    private Action _exitCallback;

    public bool isShowen { get; private set; }

    public bool isAnimationPlaying { get; private set; }

    public SceneTransistor()
    {
        _trasistionPrefab = (GameObject)Resources.Load("LoadScreen");
    }

    public void EnterTheTransition(Action callback)
    {
        _enterCallback = callback;
        if (_trasistionObject == null)
        {
            _trasistionObject = CreateTransistionObject();
            _transitionView = _trasistionObject.GetComponentInChildren<TransitiorView>();
        }
        _trasistionObject.SetActive(true);
        isShowen = true;
        isAnimationPlaying = true;
        _transitionView.ShowLoadWindow(LoadWindowShowed);
    }

    public void ExitTheTransition(Action callback)
    {
        _exitCallback = callback;
        isAnimationPlaying = true;
        _transitionView.HideLoadWindow(LoadWindowHied);
    }

    private void LoadWindowShowed()
    {
        isAnimationPlaying = false;
        _enterCallback?.Invoke();
    }

    private GameObject CreateTransistionObject()
    {
        GameObject obj = GameObject.Instantiate(_trasistionPrefab);
        GameObject.DontDestroyOnLoad(obj);
        return obj;
    }
    private void LoadWindowHied()
    {
        isShowen = false;
        isAnimationPlaying = false;
        _trasistionObject.SetActive(false);
        _exitCallback?.Invoke();
    }
}
