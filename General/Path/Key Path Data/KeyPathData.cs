using UnityEngine;

[System.Serializable]
public class KeyPathData 
{
    public KeyPathData()
    {
        _key = "";
    }
    
    public KeyPathData(string key)
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
