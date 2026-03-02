using System;
using AdvancedInputFieldPlugin;
using UnityEngine;

/// <summary>
/// Нужен на случай если используем плагин AdvancedInputField
/// т.к у него другая логика проверки отображена ли клавиатура
/// (он перехватывет упр. клавиатурой и по этому дефолтные методы Unity не сработают)
/// </summary>
public class TriggerOpenKeyboardMobilePluginAdvancedInputField : AbsTriggerOpenKeyboardMobile
{
    public override bool KeyboardIsVisible => _keyboardIsVisible;
    private bool _keyboardIsVisible = false;
    public override event Action<bool> OnUpdateStatusKeyboardIsVisible;
    
    void Start()
    {
        NativeKeyboardManager.Keyboard.AddKeyboardHeightChangedListener(OnTriggerOpenKeyboard);
    }

    private void OnTriggerOpenKeyboard(int keyboardHeight)
    {
        if (keyboardHeight > 0)
        {
            _keyboardIsVisible = true;
            OnUpdateStatusKeyboardIsVisible?.Invoke(_keyboardIsVisible);
            return;
        }
        else if (NativeKeyboardManager.Keyboard.State == KeyboardState.VISIBLE) 
        {
            _keyboardIsVisible = true;
            OnUpdateStatusKeyboardIsVisible?.Invoke(_keyboardIsVisible);
            return;
        }
        
        if (keyboardHeight == 0)
        {
            _keyboardIsVisible = false;
            OnUpdateStatusKeyboardIsVisible?.Invoke(_keyboardIsVisible);
            return;
        }
        else if (NativeKeyboardManager.Keyboard.State == KeyboardState.HIDDEN) 
        {
            _keyboardIsVisible = false;
            OnUpdateStatusKeyboardIsVisible?.Invoke(_keyboardIsVisible);
            return;
        }
        
        
    }

    private void OnDestroy()
    {
        if(NativeKeyboardManager.Keyboard!=null)
        {
            NativeKeyboardManager.Keyboard.RemoveKeyboardHeightChangedListener(OnTriggerOpenKeyboard);
        }
    }
}
