using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapInfo : MonoBehaviour
{
    public int mapID;
    public bool isPlaced;

    public int getMapID()
    {
        return mapID;
    }

    public void setPlaceState(bool b)
    {
        isPlaced = b;
    }

    public bool getPlaceState()
    {
        return isPlaced;
    }
}
