using UnityEngine; 
using TListPlugin; 
[System.Serializable]
public class IdentifierAndData_KeyImage : AbsIdentifierAndData<IndifNameSO_KeyImage, string, KeyImage>
{

 [SerializeField] 
 private KeyImage _dataKey;

 public override KeyImage GetKey()
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
 _dataKey = JsonUtility.FromJson<KeyImage>(json);
 }
#endif
}
