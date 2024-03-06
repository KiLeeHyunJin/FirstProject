using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateLayerMask
{
    public enum PlayerKeyLayer
    {
        Jump, BasicAttack , Move, Run, 
    }
    int layer;
    public void OnLayer(PlayerKeyLayer checkKey)
    {
        int addLayer = 1 << (int)layer;
        layer |= addLayer; 
    }
    public bool ContainLayer(PlayerKeyLayer checkKey)
    {
        int checkLayer = 1 << (int)checkKey;
        checkLayer &= layer;
        if (checkLayer > 0)
            return true;
        return false;
    }
    public void OffLayer(PlayerKeyLayer checkKey)
    {
        int offLayer = 1 << (int)layer;
        layer &= ~offLayer;
    }
    public bool[] CheckLayer(params PlayerKeyLayer[] checkKeys)
    {
        bool[] checkLayers = new bool[checkKeys.Length];
        for (int i = 0; i < checkKeys.Length; i++)
            checkLayers[i] = ContainLayer(checkKeys[i]);
        return checkLayers;
    }
}
