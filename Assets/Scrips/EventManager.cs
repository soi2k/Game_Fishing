using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : Singleton<EventManager>
{
    public float numberRemovedItems;

    public bool blMoveCameraHook;
    public bool blActiveHookSwinging;
    public bool blAnimEnding;

    protected override void Start()
    {
        numberRemovedItems = 0;

        blMoveCameraHook = false;
        blActiveHookSwinging = false;
        blAnimEnding = false;
    }
}
