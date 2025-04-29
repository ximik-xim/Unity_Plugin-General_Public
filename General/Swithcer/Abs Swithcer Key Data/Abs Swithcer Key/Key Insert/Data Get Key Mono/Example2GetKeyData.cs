using System;

public class Example2GetKeyData : AbsGetKeyData<Example2KeySwithcer>
{
    public override Example2KeySwithcer GetCurrentKey { get; }
    public override event Action OnUpdateKey;
}
