using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public abstract class AbsMovement : GameMonoBehaviour
{
    [Header("Movement")]
    [SerializeField] protected Transform _transform;
    [SerializeField] public MeshRenderer _meshRenderer;

    [SerializeField] protected float speedMove;
    [SerializeField] protected float timeMove;
    [SerializeField] protected float timeElapse = 0;
    protected Vector3 targetPst;
    protected Vector3 startPst;

    protected override void LoadComponentBase()
    {
        base.LoadComponentBase();
        LoadTransform();
        LoadMesRenderer();
    }
    protected virtual void LoadTransform()
    {
        if (this._transform != null) return;
        this._transform = GetComponent<Transform>();
    }

    protected virtual void LoadMesRenderer()
    {
        if (this._meshRenderer != null) return;
        this._meshRenderer = GetComponent<MeshRenderer>();
    }
    protected virtual void Move() { }
}