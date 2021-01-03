using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[ExecuteInEditMode]
public class UICustomToggle : MonoBehaviour
{
    public Button button;
    public Image target;
    [Space]
    public bool isOn;
    public Sprite spriteOn;
    public Sprite spriteOff;
    [Space]
    public UnityEvent OnToggleEvent;
    public event Action<bool> OnToggle;
    
    void Awake()
    {
        button.onClick.AddListener(HandleClick);
    }
    
    void OnDestroy()
    {
        button.onClick.RemoveListener(HandleClick);
    }
    
    void HandleClick()
    {
        isOn = !isOn;
        
        target.sprite = isOn ? spriteOn : spriteOff;
        
        OnToggle?.Invoke(isOn);
        OnToggleEvent.Invoke();
    }
    
    #if UNITY_EDITOR
    void Update()
    {
        if(!Application.isPlaying && Application.isEditor)
        {
            target.sprite = isOn ? spriteOn : spriteOff;
        }
    }
    #endif
}
