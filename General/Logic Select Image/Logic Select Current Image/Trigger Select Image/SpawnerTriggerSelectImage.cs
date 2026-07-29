using System;
using UnityEngine;

public class SpawnerTriggerSelectImage : MonoBehaviour
{
   [SerializeField]
   private SO_Data_KeyImage _soStorageKeyImage;
   
   [SerializeField]
   private LogicSetCurrentKeyImage _prefab;
   
   [SerializeField]
   private GameObject _parent;

   [SerializeField]
   private CurrentSelectKeyImage _currentSelectKeyImage;
   private void Awake()
   {
      foreach (var VARIABLE in _soStorageKeyImage.GetAllData())
      {
         var data = Instantiate(_prefab, _parent.transform);
         data.Init(_currentSelectKeyImage, VARIABLE);
      }
   }
}
