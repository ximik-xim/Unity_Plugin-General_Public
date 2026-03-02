using System.Collections.Generic;
using AdvancedInputFieldPlugin;
using UnityEngine;

/// <summary>
/// Фильтр ввода текста
/// Удаляет все табуляции, переносы строк и т.д
/// </summary>
public class LiveProcessingFilterClear_n_t_r : LiveProcessingFilter
{
    public override TextEditFrame ProcessTextEditUpdate(TextEditFrame textEditFrame, TextEditFrame lastTextEditFrame)
    {
        if(textEditFrame.text == lastTextEditFrame.text)
        {
            return textEditFrame; 
        }
        else
        {
            textEditFrame.text = textEditFrame.text.Replace("\n", "").Replace("\r", "").Replace("\t", "");

            return textEditFrame;
        }
    }
}
