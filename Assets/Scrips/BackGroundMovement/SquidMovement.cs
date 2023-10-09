using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SquidMovement: Movement
{
    public float radius = 4.0f; // Bán kính của hình tròn
    private float angle = 0.0f; // Góc hiện tại
    protected float pstx, psty;
    protected override void Start()
    {
        pstx = _transform.position.x;
        psty = _transform.position.y;
        moveSpeed = 0.2f;
    }
    protected override void Update()
    {
        Move();
    }
    protected override void Move()
    {
        // Tính toán vị trí mới của đối tượng dựa trên thời gian và bán kính
        angle += moveSpeed * Time.deltaTime;
        float x = pstx + Mathf.Cos(angle) * radius;
        float y = psty + Mathf.Sin(angle) * radius;

        // Đặt vị trí của đối tượng
        _transform.position = new Vector3(x, y, _transform.position.z);
    }
}
