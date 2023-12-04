using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinStateController : Singleton<WinStateController>
{
    public void Active()
    {
        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(true);
        }
    }
}

