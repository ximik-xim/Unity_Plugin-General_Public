using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleDefaultPool : MonoBehaviour
{
    [SerializeField]
    private CustomPool<ExampleDefaultPoolData> _examplePoolKey;

    [SerializeField] 
    private ExampleDefaultPoolData  _prefabs;
    private void OnEnable()
    {
        _examplePoolKey = new CustomPool<ExampleDefaultPoolData>(CreateData, null, null);
        var obj = _examplePoolKey.GetObject();
        //_examplePoolKey.Release( obj);
        
    }

    private ExampleDefaultPoolData CreateData()
    {
        return Instantiate(_prefabs);
    }
}
