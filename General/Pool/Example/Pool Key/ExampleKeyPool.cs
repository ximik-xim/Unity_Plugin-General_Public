using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleKeyPool : MonoBehaviour
{
    [SerializeField]
    private CustomPoolKey<TestPoolKey, ExampleKeyMono> _examplePoolKey;

    [SerializeField] 
    private List<ExampleKeyMono>  _prefabs;
    private void OnEnable()
    {
        _examplePoolKey = new CustomPoolKey<TestPoolKey, ExampleKeyMono>(CreateData, null, null);
        var obj = _examplePoolKey.GetObject(TestPoolKey.One);
        //_examplePoolKey.Release( obj);
        
        
    }

    private ExampleKeyMono CreateData(TestPoolKey arg)
    {
        foreach (var VARIABLE in _prefabs)
        {
            if (VARIABLE.GetKey() == arg) 
            {
                return Instantiate(VARIABLE);
            }
        }

        return null;
    }
}

public enum TestPoolKey
{
    None,
    One,
    Two
}