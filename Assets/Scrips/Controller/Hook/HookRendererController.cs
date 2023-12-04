using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookRendererController : MonoBehaviour,ISetActiveRenderer
{
    public MeshRenderer _meshRenderer;
    protected void Awake()
    {
        _meshRenderer = GameObject.Find("HookCtrl").GetComponent<MeshRenderer>();
    }
    
    public void SetActiveRenderer(bool blActive)
    {
        _meshRenderer.enabled = blActive;
    }
}
