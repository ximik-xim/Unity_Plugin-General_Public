using System;
using AdvancedInputFieldPlugin;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Нужен на случай если используем плагин AdvancedInputField
/// т.к у него другая логика проверки отображена ли клавиатура
/// (он перехватывет упр. клавиатурой и по этому дефолтные методы Unity не сработают)
/// </summary>
public class TriggerOpenKeyboardMobilePluginAdvancedInputField : MonoBehaviour
{
    [SerializeField]
    private LogicSetSelectObjectOnTopKeyboard _logicSetSelectObjectOnTopKeyboard;
    
    private bool _keyboardIsVisible = false;
    
    void Start()
    {
        NativeKeyboardManager.Keyboard.AddKeyboardHeightChangedListener(OnTriggerOpenKeyboard);
    }

    private void OnTriggerOpenKeyboard(int keyboardHeight)
    {
        if (keyboardHeight > 0)
        {
            _keyboardIsVisible = true;
            CheckVisibleKeyboard(_keyboardIsVisible);
            return;
        }
        else if (NativeKeyboardManager.Keyboard.State == KeyboardState.VISIBLE) 
        {
            _keyboardIsVisible = true;
            CheckVisibleKeyboard(_keyboardIsVisible);
            return;
        }
        
        if (keyboardHeight == 0)
        {
            _keyboardIsVisible = false;
            CheckVisibleKeyboard(_keyboardIsVisible);
            return;
        }
        else if (NativeKeyboardManager.Keyboard.State == KeyboardState.HIDDEN) 
        {
            _keyboardIsVisible = false;
            CheckVisibleKeyboard(_keyboardIsVisible);
            return;
        }
        
        
    }
    
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

    private void OnDestroy()
    {
        if (NativeKeyboardManager.Keyboard != null) 
        {
            NativeKeyboardManager.Keyboard.RemoveKeyboardHeightChangedListener(OnTriggerOpenKeyboard);
        }
    }
}
