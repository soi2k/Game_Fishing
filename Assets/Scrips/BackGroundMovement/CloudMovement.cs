using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMovement : Movement
{
    protected override void Start()
    {
        moveSpeed = 0.1f;
    }
        
    protected override void Update()
    {
        Move();
    }

    protected override void Move()
    {
        _meshRenderer.material.mainTextureOffset += new Vector2(Time.deltaTime * moveSpeed, 0);
    }
}
