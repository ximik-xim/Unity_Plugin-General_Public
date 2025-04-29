using TListPlugin;
using UnityEngine;

[System.Serializable]
public class KeyIndifNameSODataDKODataKey : AbsIdentifierAndData<DKOIndifNameSO,string,DKODataKey>
{

    [SerializeField] 
    private DKODataKey _dataKey;


    public override DKODataKey GetKey()
    {
        return _dataKey;
    }
}
