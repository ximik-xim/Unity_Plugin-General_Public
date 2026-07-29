using UnityEngine; 
using TListPlugin; 
[System.Serializable]
public class IdentifierAndData_KeyPathData : AbsIdentifierAndData<IndifNameSO_KeyPathData, string, KeyPathData>
{

 [SerializeField] 
 private KeyPathData _dataKey;

 public override KeyPathData GetKey()
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
 _dataKey = JsonUtility.FromJson<KeyPathData>(json);
 }
#endif
}
