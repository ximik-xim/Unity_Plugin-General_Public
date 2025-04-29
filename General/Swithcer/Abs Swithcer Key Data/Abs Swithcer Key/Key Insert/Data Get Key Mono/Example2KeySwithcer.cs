using UnityEngine;

[System.Serializable]
public class Example2KeySwithcer :  IGetKey<string>
{
    [SerializeField]
    private string key;

    public string GetKey()
    {
        return key;
    }
}
