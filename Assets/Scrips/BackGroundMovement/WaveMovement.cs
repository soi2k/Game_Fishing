using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveMovement : Movement
{
   
    protected float distancelimited = 0.04f; // Khoảng cách cuộn (10 đơn vị)

    private bool movediraction = true; // Ban đầu bắt đầu cuộn sang phải

    protected override void Start()
    {
        moveSpeed = 0.02f;
    }

    protected override void Update()
    {
        Move();
    }
    protected override void Move()
    {
        // Tính toán sự thay đổi vị trí của texture
        float offsetChange = moveSpeed * Time.deltaTime;

        if (movediraction)
        {
            // Cuộn sang phải
            _meshRenderer.material.mainTextureOffset += new Vector2(offsetChange, 0);

            // Kiểm tra xem đã di chuyển đủ khoảng cách chưa
            if (_meshRenderer.material.mainTextureOffset.x >= distancelimited)
            {
                movediraction = false;
            }
        }
        else
        {
            // Cuộn sang trái
            _meshRenderer.material.mainTextureOffset -= new Vector2(offsetChange, 0);

            // Kiểm tra xem đã di chuyển đủ khoảng cách chưa
            if (_meshRenderer.material.mainTextureOffset.x <= 0)
            {
                movediraction = true;
            }
        }
    }
}
