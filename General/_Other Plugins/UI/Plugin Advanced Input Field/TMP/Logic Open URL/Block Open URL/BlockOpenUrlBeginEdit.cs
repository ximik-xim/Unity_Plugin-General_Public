using System;
using AdvancedInputFieldPlugin;
using UnityEngine;

/// <summary>
/// Будет блокировать возможность перехода по ссылке, пока находимся в режиме редактирования
/// </summary>
public class BlockOpenUrlBeginEdit : MonoBehaviour
{
   [SerializeField]
   private AdvancedInputField _advancedInputField;
   [SerializeField]
   private LogicOpenUrlTextTPM _openUrlTextTpm;
   private void Awake()
   {
      _advancedInputField.OnBeginEdit.AddListener(OnBeginEdit);
      _advancedInputField.OnEndEdit.AddListener(OnEndEdit);
      _advancedInputField.OnTextSelectionChanged.AddListener(OnTextSelectionChanged);
   }

   private void OnTextSelectionChanged(int start, int end)
   {
      //Если есть выделенный текст, то блокирую переход по ссылке
      if (start != end) 
      {
         _openUrlTextTpm.SetStatusBlockLinc(true);   
      }
      else
      {
         //Если нет выделенного текста, то разрешаю переход по ссылке
         _openUrlTextTpm.SetStatusBlockLinc(false);   
      }
   }

   private void OnBeginEdit(BeginEditReason arg0)
   {
      //Если есть выделенный текст, то блокирую переход по ссылке
      if (_advancedInputField.GetTextSelectionStartPosition() != _advancedInputField.GetTextSelectionEndPosition()) 
      {
         _openUrlTextTpm.SetStatusBlockLinc(true);   
      }
      else
      {
         //Если нет выделенного текста, то разрешаю переход по ссылке
         _openUrlTextTpm.SetStatusBlockLinc(false);   
      }
   }
   
   private void OnEndEdit(string arg0, EndEditReason arg1)
   {
      //По окончанию режима выделения, разрешаю переход по ссылке
      _openUrlTextTpm.SetStatusBlockLinc(false);
   }

   private void OnDestroy()
   {
      _advancedInputField.OnBeginEdit.RemoveListener(OnBeginEdit);
      _advancedInputField.OnEndEdit.RemoveListener(OnEndEdit);
      _advancedInputField.OnTextSelectionChanged.RemoveListener(OnTextSelectionChanged);
   }
}
