using UnityEngine;

/// <summary>
/// Нужен на случай если нужны еще доп. методы для работы с Text
/// </summary>
public abstract class AbsUIComponentTextMethod : AbsGetAndSetText
{
    public abstract float GetSizeText();
    
    public abstract void SetSizeText(float sizeText);
}
