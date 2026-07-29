using System;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Отвечает за отслеживания включения клавиатуры на телефоне
/// (В случае необходимости тестирования в редакторе,
/// необходимо вручную дергать переключатель о том что клава включена)
/// </summary>
public class TriggerOpenKeyboardMobileDefault : MonoBehaviour
{
    [SerializeField]
    private LogicSetSelectObjectOnTopKeyboard _logicSetSelectObjectOnTopKeyboard;
    
    private bool _keyboardIsVisible = false;

#if UNITY_EDITOR
    [SerializeField]
    private bool _keyboardIsVisibleEditor;
#endif
    

#if UNITY_EDITOR
    private void OnValidate()
    {
        _keyboardIsVisible = _keyboardIsVisibleEditor;
        CheckVisibleKeyboard(_keyboardIsVisible);
    }
#endif

#if !UNITY_EDITOR
    private void Update()
    {
        if (TouchScreenKeyboard.visible != _keyboardIsVisible)
        {
            _keyboardIsVisible = TouchScreenKeyboard.visible;
            CheckVisibleKeyboard(_keyboardIsVisible);
        }
        
    }
#endif

    private void CheckVisibleKeyboard(bool isVisibleKeyboard)
    {
        if (isVisibleKeyboard == true)
        {
            GameObject currentGM = EventSystem.current.currentSelectedGameObject;
            if (currentGM != null) 
            {
                _logicSetSelectObjectOnTopKeyboard.SetTargetObject(currentGM);    
            }
        }
        else
        {
            _logicSetSelectObjectOnTopKeyboard.RemoveTargetObject();
        }
        
    }
    
    
}
