using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMonoBehaviour : MonoBehaviour
{

    protected virtual void Awake()
    {
        this.LoadComponentBase();
    }
    protected virtual void Start()
    {

    }
    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
    protected virtual void Reset()
    {
        this.LoadComponentBase();
    }
    protected virtual void LoadComponentBase()
    {

    }
}
