using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[CreateAssetMenu(menuName = "Key Image/SO Storage Key Image")]
/// <summary>
/// Хранит в себе изображения по ключу
/// </summary>
public class StorageKeyAndSpriteImage : MonoBehaviour
{
   public bool IsInit => _isInit;
   private bool _isInit = false;
   public event Action OnInit;

   [SerializeField]
   private List<AbsKeyData<GetDataSO_KeyImage, Sprite>> _listImage;

   private Dictionary<string, Sprite> _data = new Dictionary<string, Sprite>();
   
   private void Awake()
   {
      foreach (var VARIABLE in _listImage)
      {
         _data.Add(VARIABLE.Key.GetData().GetKey(), VARIABLE.Data);
      }
      
      _isInit = true;
      OnInit?.Invoke();
   }

   public Sprite GetImage(KeyImage key)
   {
      return _data[key.GetKey()];
   }
}
