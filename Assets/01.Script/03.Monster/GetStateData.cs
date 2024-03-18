using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetStateData : MonoBehaviour
{
    IBaseState controllerData;
    public void Start()
    {
        controllerData = GetComponentInParent<IBaseState>();
    }
    public StateData GetData()
    {
        return controllerData.IGetState();
    }
}
