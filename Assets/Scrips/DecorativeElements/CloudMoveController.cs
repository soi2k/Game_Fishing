using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMoveController : AbsMovement
{
    protected override void Start()
    {
        speedMove = 0.1f;
    }
        
    protected override void Update()
    {
        Move();
    }

    protected override void Move()
    {
        _meshRenderer.material.mainTextureOffset += new Vector2(Time.deltaTime * speedMove, 0);
    }
}
