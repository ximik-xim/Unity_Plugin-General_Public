using System;
using UnityEngine;

public abstract class AbsCallbackGetData<GetType, ArgData> : MonoBehaviour
{
    public abstract bool IsInit { get; }
    public abstract event Action OnInit;
    public abstract GetServerRequestData<GetType> GetData(ArgData data);
}