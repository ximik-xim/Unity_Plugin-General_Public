using System;
using System.Text.RegularExpressions;
using AdvancedInputFieldPlugin;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Автоматически находит вставки URL ссылок и помечает их 
/// </summary>
public class LogicAllocationURL : MonoBehaviour
{

    [SerializeField]
    private AdvancedInputField _advancedInputField;

    /// <summary>
    /// Нужно ли трегирить каждое изменение текста
    /// </summary>
    [SerializeField]
    private bool _triggerUpdateText;
    
    [SerializeField]
    private bool _triggerEndEdit;
    
    [SerializeField]
    private Color _colorUrl = Color.blue;

    void Awake()
    {
        if (_triggerUpdateText == true)
        {
            _advancedInputField.OnValueChanged.AddListener(OnUpdateText);
        }

        if (_triggerEndEdit == true)
        {
            _advancedInputField.OnEndEdit.AddListener(OnUpdateText);
        }

        _advancedInputField.SetText(GetNewText());
    }

    private void OnUpdateText(string arg0, EndEditReason arg1)
    {
        _advancedInputField.SetTextAndPreserveSelection(GetNewText());
    }

    private void OnUpdateText(string arg0)
    {
        _advancedInputField.SetTextAndPreserveSelection(GetNewText());
    }

    private string GetNewText()
    {
            
        string text = _advancedInputField.RichText;
        
        //Ищю в тексте ранее созданные вставки и удаляю их 
        text = Regex.Replace(text, @"<link=""[^""]+""><color=.*?>(.+?)</color></link>", "$1", RegexOptions.Singleline);
        
        //Находим все URL адреса и делаю вставки в текст 
        text = Regex.Replace(text, @"https?://\S+|(?<!https?://)www\.\S+", match =>
        {
            return GetInsertText(_colorUrl, match.Value);; // остальные не трогаем
        });

        return text;
    }
    
    
    /// <summary>
    /// Вернет вставку для Url адреса
    /// </summary>
    /// <param name="color"></param>
    /// <param name="url"></param>
    /// <returns></returns>
    private string GetInsertText(Color color, string url)
    {
        Debug.Log("SEt URL = " + url);
        
            string result =
            $"<link=\"{url}\">" +
            $"<color=#{ColorUtility.ToHtmlStringRGBA(color)}>{url}</color>" +
            $"</link>";

        return result;
    }



    /// <summary>
    /// Вырезает текст между вставок начального и конечного символа
    /// (при этом вставка может быть одна в другой и даже так вырежет все вставки)
    /// </summary>
    private string RemoveTargetText(string text, string startRemoveText, string stopRemoveText)
    {
        //получаем список совпадений для стартового символа
        var matchesStartChar = Regex.Matches(text, startRemoveText);
        
        if (matchesStartChar.Count > 0)
        {
            //получаем список совпадений для конечного символа
            var matchesEndChar = Regex.Matches(text, stopRemoveText);

            //нужен т.к будет укорачиваться изначальная стока 
            int offest = 0;
            //копия слова 
            string copyText = text;
            for (int j = 0; j < matchesStartChar.Count; j++)
            {
                int length;

                //на случай если есть стартовый символ, а конечного символа не нашлось , то обрезаю текст до конца
                if (matchesStartChar.Count > matchesEndChar.Count && j >= matchesEndChar.Count)
                {
                    // находим длину слова кторую надо удалить
                    // формула длина текста - id послед встреченного открыв (нету коф -1,т.к сразу работаю тут с длинной а не с id)
                    length = copyText.Length - matchesStartChar[matchesStartChar.Count - j - 1].Index;
                    //убираю из текста найденную вставку
                    copyText = copyText.Remove(matchesStartChar[matchesStartChar.Count - j - 1].Index, length);
                    
                    break;
                }
                else
                {
                    // находим длину слова кторую надо удалить
                    // формула (id первого встреченного закрыв. символа - id послед встреченного открыв + 1(т.к это длинна а не id) - смещение, если слово редактируеться несколько раз и каждый раз смещаеться из за удаления текста
                    length = matchesEndChar[j].Index - matchesStartChar[matchesStartChar.Count - j - 1].Index + 1 - offest;
                    //убираю из текста найденную вставку
                    copyText = copyText.Remove(matchesStartChar[matchesStartChar.Count - j - 1].Index, length);

                    //добавляю к смещению кол-во вырезаных символов
                    offest += length;
                }
            }
            
            return copyText;
            //тут начинаю вырезать
        }

        else
        {
            return text;
        }
    }
    
    

    private void OnDestroy()
    {
        _advancedInputField.OnValueChanged.RemoveListener(OnUpdateText);
        _advancedInputField.OnEndEdit.RemoveListener(OnUpdateText);
    }
}