using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMoveController : AbsMovement
{
   
    private float distancelimited = 0.04f; // Khoảng cách cuộn (10 đơn vị)

    private bool movediraction = true;

    protected override void Start()
    {
        speedMove = 0.02f;
    }

    protected override void Update()
    {
        Move();
    }
    protected override void Move()
    {
        // Tính toán sự thay đổi vị trí của texture
        float offsetChange = speedMove * Time.deltaTime;

        if (movediraction)
        {
            _meshRenderer.material.mainTextureOffset += new Vector2(offsetChange, 0);
            if (_meshRenderer.material.mainTextureOffset.x >= distancelimited)
            {
                movediraction = false;
            }
        }
        else
        {
            _meshRenderer.material.mainTextureOffset -= new Vector2(offsetChange, 0);
            if (_meshRenderer.material.mainTextureOffset.x <= 0)
            {
                movediraction = true;
            }
        }
    }
}
