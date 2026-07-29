using UnityEngine;

[System.Serializable]
public class KeyImage 
{
    public KeyImage()
    {
        _key = "";
    }
    
    public KeyImage(string key)
    {
        _key = key;
    }
    
    [SerializeField]
    private string _key;

    public string GetKey()
    {
        return _key;
    }
}
