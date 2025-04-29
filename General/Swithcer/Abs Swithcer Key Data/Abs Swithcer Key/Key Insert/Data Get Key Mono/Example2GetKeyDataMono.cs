using System;

public class Example2GetKeyDataMono : AbsGetKeyDataMono<Example2KeySwithcer,Example2GetKeyData>
{
    public override bool IsInit { get; }
    public override event Action OnInit;
    
    //тут типа откудо то(возможно через Dko) взяли ключ
    public override Example2GetKeyData GetKeyData()
    {
        throw new NotImplementedException();
    }
}
