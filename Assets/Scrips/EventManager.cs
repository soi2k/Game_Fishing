﻿using UnityEngine;
using System;

public class EventManager : Singleton<EventManager>
{
    // Khai báo các sự kiện theo các kiểu dữ liệu
    public event Action<bool> onActiveHookSwinging;
    public event Action onCameraMoveUp;
    public event Action onWinState;

    // Gọi các sự kiện tương ứng khi cần
    public void OnActiveHookSwinging(bool isActive)
    {
        onActiveHookSwinging?.Invoke(isActive);
    }

    public void OnCameraMoveUp()
    {
        onCameraMoveUp?.Invoke();
    }  
    public void OnWinState()
    {
        onWinState?.Invoke();
    }
}
