
using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Устанавливает картинку по полученному ключу у триггера
/// </summary>
public class TriggerSetRawImageCurrentKeyImage : MonoBehaviour
{
    [SerializeField]
    private LogicSetCurrentKeyImage _currentKeyImage;

    [SerializeField]
    private RawImage _rawImage;

    [SerializeField]
    private GetDKOPatch _patchStorageKeyAndSpriteImage;

    private StorageKeyAndSpriteImage _storageKeyAndSpriteImage;

    private void Awake()
    {
        if (_patchStorageKeyAndSpriteImage.Init == false)
        {
            _patchStorageKeyAndSpriteImage.OnInit += OnInitStoragePanel;
        }

        if (_currentKeyImage.IsInit == false)
        {
            _currentKeyImage.OnInit += OnInitCurrentKeyImage;
        }

        CheckInit();
    }

    private void OnInitStoragePanel()
    {
        _patchStorageKeyAndSpriteImage.OnInit -= OnInitStoragePanel;
        CheckInit();
    }

    private void OnInitCurrentKeyImage()
    {
        if (_currentKeyImage.IsInit == true)
        {
            _currentKeyImage.OnInit -= OnInitCurrentKeyImage;
            CheckInit();
        }
    }

    private void CheckInit()
    {
        if (_patchStorageKeyAndSpriteImage.Init == true && _currentKeyImage.IsInit == true)
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
        CustomMethodImage.SetSpriteRawImage(_rawImage, _storageKeyAndSpriteImage.GetImage(_currentKeyImage.KeyImage));
    }
}
