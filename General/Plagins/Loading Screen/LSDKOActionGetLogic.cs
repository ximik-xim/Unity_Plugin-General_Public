using UnityEngine;

public class LSDKOActionGetLogic : DKOTargetAction
{
    [SerializeField] 
    private LSLoadingScreenLogic _generalCanvas;
    
    private void Awake()
    {
        LocalAwake();
    }
    
    protected override DKODataRund InvokeRun()
    {
        return new DKODataLSDKOActionGetLogic(_generalCanvas);
    }
}

public class DKODataLSDKOActionGetLogic : DKODataRund
{
    private LSLoadingScreenLogic _loadingScreenLogic;

    public DKODataLSDKOActionGetLogic(LSLoadingScreenLogic loadingScreenLogic )
    {
        _loadingScreenLogic = loadingScreenLogic;
    }
    
    public bool IsOpen => _loadingScreenLogic.IsOpen;

    public void AddTask(LSLoadingScreenLogicDataTask task)
    {
        _loadingScreenLogic.AddTask(task);
    }


    public void RemoveTask(LSLoadingScreenLogicDataTask task)
    {
        _loadingScreenLogic.RemoveTask(task);
    }

    
    public void Open()
    {
        _loadingScreenLogic.Open();
    }

    public void Close()
    {
        _loadingScreenLogic.Close();
    }
}