using UnityEngine;

[System.Serializable]
public class KeyTimerData 
{
    [SerializeField]
    private string _key;

    public string GetKey()
    {
        return _key;
    }
}
