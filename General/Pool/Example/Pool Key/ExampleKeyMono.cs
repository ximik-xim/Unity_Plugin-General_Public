using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExampleKeyMono : MonoBehaviour,IGetKey<TestPoolKey>
{
    [SerializeField]
    private TestPoolKey _key;

    public void SetKey(TestPoolKey key)
    {
        _key = key;
    }
    public TestPoolKey GetKey()
    {
        return _key;
    }
}
