using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenAdjust : MonoBehaviour
{
    public ScreenOrientation so;
    void Start()
    {
        Screen.orientation = so;
    }
}
