using System;
using UnityEngine;

public class TransitiorView : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _loadIcon;
    private Canvas _canvas;
    private Action _enterCallback;
    private Action _exitCallback;

    public void ShowLoadWindow(Action callback) 
    {
        _canvas = GetComponentInParent<Canvas>();
        _canvas.worldCamera = Camera.main;
        _enterCallback = callback;
    }
    public void HideLoadWindow(Action callback = null)
    {
        _loadIcon.SetActive(false);
        _animator.SetTrigger("Exit");
        _exitCallback = callback;
    }

    public void ShowAnimationEnd() 
    {
        _loadIcon.SetActive(true);
        _enterCallback?.Invoke();
    }
    public void HideAnimationEnd()
    {
        _exitCallback?.Invoke();
    }
}
