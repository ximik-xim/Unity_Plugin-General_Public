using TListPlugin;
using UnityEngine;
[System.Serializable]
public class KeyIndifNameSOData_MBS_DKO : AbsIdentifierAndData<MessengerDKOBetweenSceneIndifNameSO,string,MessengerBetweenSceneKeyDKO>
{
    [SerializeField] 
    private MessengerBetweenSceneKeyDKO dataKeyDKO;


    public override MessengerBetweenSceneKeyDKO GetKey()
    {
        return dataKeyDKO;
    }
}
