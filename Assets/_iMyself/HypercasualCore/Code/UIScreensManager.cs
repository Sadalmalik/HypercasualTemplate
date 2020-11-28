using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class UIScreensManager : MonoBehaviour
{
    public static UIScreensManager Instance;
    
    private Dictionary<string, UIScreen> _screens = new Dictionary<string, UIScreen>();
    
    [SerializeField] protected Image blackFilter;
    [SerializeField] protected float fadeDuration;
    
    [NonSerialized] public UIScreen _startScreen;
    [NonSerialized] public UIScreen _currentScreen;
    
    public UIScreen StartScreen => _startScreen;
    public UIScreen CurrentScreen => _currentScreen;
    
    private Sequence _fadeSequence;
    
    void Awake()
    {
        if(Instance!=null)
        {
            gameObject.SetActive(false);
            Debug.LogError($"UIScreensManager duplicates: {Instance.name}, {gameObject.name}");
            return;
        }
        Instance = this;
        
        var screens = FindObjectsOfType<UIScreen>();
        
        foreach (var screen in screens)
        {
            _screens.Add(screen.gameObject.name, screen);
            
            if (screen.isStartScreen)
            {
                if (_startScreen==null)
                {
                    _startScreen = screen;
                }
                else
                {
                    Debug.LogWarning($"Can be only one start screen! Check duplicates: {_startScreen.name}, {screen.name}");
                }
            }
            
            screen.gameObject.SetActive(false);
        }
        
        _startScreen.gameObject.SetActive(true);
        _currentScreen = _startScreen;
        
        blackFilter.gameObject.SetActive(true);
        _fadeSequence = DOTween.Sequence();
        _fadeSequence.Append(blackFilter.DOFade(0, fadeDuration));
        _fadeSequence.AppendCallback(()=>blackFilter.raycastTarget = false);
        _fadeSequence.Play();
    }

    public void SetScreen(string screenName, bool instant, Action callback)
    {
        if (instant)
        {
            SwitchScreen(screenName);
            callback?.Invoke();
        }
        else
        {
            _fadeSequence?.Kill();
            _fadeSequence = DOTween.Sequence();
            _fadeSequence.AppendCallback(()=>blackFilter.raycastTarget = true);
            _fadeSequence.Append(blackFilter.DOFade(1, fadeDuration));
            _fadeSequence.AppendCallback(()=>SwitchScreen(screenName));
            _fadeSequence.Append(blackFilter.DOFade(0, fadeDuration));
            _fadeSequence.AppendCallback(()=>blackFilter.raycastTarget = false);
            _fadeSequence.AppendCallback(()=>callback?.Invoke());
            _fadeSequence.Play();
        }
    }
    
    private void SwitchScreen(string screenName)
    {
        _currentScreen.gameObject.SetActive(false);
        if (!_screens.TryGetValue(screenName, out _currentScreen))
        {
            Debug.LogWarning($"Unknown screen '{screenName}'!");
            _currentScreen = _startScreen;
        }
        _currentScreen.gameObject.SetActive(true);
    }
}
