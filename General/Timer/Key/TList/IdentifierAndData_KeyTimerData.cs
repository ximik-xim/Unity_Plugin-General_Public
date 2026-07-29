using UnityEngine; 
using TListPlugin; 
[System.Serializable]
public class IdentifierAndData_KeyTimerData : AbsIdentifierAndData<IndifNameSO_KeyTimerData, string, KeyTimerData>
{

 [SerializeField] 
 private KeyTimerData _dataKey;

 public override KeyTimerData GetKey()
 {
  return _dataKey;
 }
 
#if UNITY_EDITOR
  public override string GetJsonSaveData()
 {
 return JsonUtility.ToJson(_dataKey);
 }
 
  public override void SetJsonData(string json)
 {
 _dataKey = JsonUtility.FromJson<KeyTimerData>(json);
 }
#endif
}
