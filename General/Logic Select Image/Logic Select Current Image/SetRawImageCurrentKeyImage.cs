using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Устанавливает картинку по полученному ключу
/// </summary>
public class SetRawImageCurrentKeyImage : MonoBehaviour
{
    [SerializeField]
    private RawImage _rawImage;
    
    [SerializeField]
    private CurrentSelectKeyImage _currentSelectKeyImage;
    
    [SerializeField]
    private GetDKOPatch _patchStorageKeyAndSpriteImage;
    private StorageKeyAndSpriteImage _storageKeyAndSpriteImage;
    
    private void Awake()
    {
        if (_patchStorageKeyAndSpriteImage.Init == false)
        {
            _patchStorageKeyAndSpriteImage.OnInit += OnInitStoragePanel;
        }
        

        CheckInit();
    }

    private void OnInitStoragePanel()
    {
        _patchStorageKeyAndSpriteImage.OnInit -= OnInitStoragePanel;
        CheckInit();
    }
    
    
    private void CheckInit()
    {
        if (_patchStorageKeyAndSpriteImage.Init == true)
        {
            if (_currentSelectKeyImage.IsInit == true)
            {
                InitCurrentSelectKeyImage();
            }
            else
            {
                _currentSelectKeyImage.OnInit += OnInitCurrentSelectKeyImage;
            }
           
        }
    }

    private void OnInitCurrentSelectKeyImage()
    {
        if (_currentSelectKeyImage.IsInit == true)
        {
            _currentSelectKeyImage.OnInit -= OnInitCurrentSelectKeyImage;
            InitCurrentSelectKeyImage();
        }
    }
    
    private void InitCurrentSelectKeyImage()
    {
        _currentSelectKeyImage.OnUpdateKeyImage += OnUpdateKeyImage;
        OnUpdateKeyImage();
    }

    private void OnUpdateKeyImage()
    {
        var DKOData = (DKODataInfoT<StorageKeyAndSpriteImage>)_patchStorageKeyAndSpriteImage.GetDKO();
        _storageKeyAndSpriteImage = DKOData.Data;

        if (_storageKeyAndSpriteImage.IsInit == true)
        {
            SetImage();
        }
        else
        {
            _storageKeyAndSpriteImage.OnInit -= OnInitStorageKeyAndSpriteImage;
            _storageKeyAndSpriteImage.OnInit += OnInitStorageKeyAndSpriteImage;
        }
        
       
    }

    private void OnInitStorageKeyAndSpriteImage()
    {
        if (_storageKeyAndSpriteImage.IsInit == true)
        {
            _storageKeyAndSpriteImage.OnInit -= OnInitStorageKeyAndSpriteImage;
            SetImage();
        }
    }

    private void SetImage()
    {
        CustomMethodImage.SetSpriteRawImage(_rawImage, _storageKeyAndSpriteImage.GetImage(_currentSelectKeyImage.KeyImage));
    }

    private void OnDestroy()
    {
        _currentSelectKeyImage.OnUpdateKeyImage -= OnUpdateKeyImage;
    }
}
