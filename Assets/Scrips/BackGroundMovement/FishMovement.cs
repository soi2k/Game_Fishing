using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishMovement : Movement
{
    protected float leftLimit;
    protected float rightLimit;

    protected override void Start()
    {
        base.Start();
        leftLimit = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0)).x;
        rightLimit = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0)).x;
    }
    protected override void Update()
    {
        base.Update();
        Move();

    }
    protected override void Move()
    {
        // Di chuyển vật thể sang trái hoặc phải với tốc độ moveSpeed
        _transform.Translate(moveSpeed * Time.deltaTime * Vector3.right);

        // Kiểm tra nếu vật thể chạm vào giới hạn của camera
        if (_transform.position.x >= rightLimit)
        {
            // Đổi hướng và xoay 180 độ
            if(_transform.rotation != Quaternion.Euler(0, 0, 0))
            {
                _transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else _transform.rotation = Quaternion.Euler(0, 180, 0);
        }
        // Kiểm tra xem đã gặp giới hạn trái chưa
        else if (_transform.position.x <= leftLimit)
        {
            if(_transform.rotation != Quaternion.Euler(0, 180, 0))
            {
                _transform.rotation = Quaternion.Euler(0, 180, 0);
            }
            // Đổi hướng và xoay 180 độ
            else _transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
