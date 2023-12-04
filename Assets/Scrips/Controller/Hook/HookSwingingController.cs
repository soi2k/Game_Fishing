using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookSwingingController : AbsMovement
{
    protected const float AMPLITUDE = 0.2f;     // Biên độ của chuyển động
    protected const float FREQUENCE = 1f;     // Tần số của chuyển động

    public bool blActiveHookSwinging;
    protected override void Update()
    {
        if (blActiveHookSwinging)
        {
            HookSwinging();
        }
    }
    protected void OnEnable()
    {
        EventManager.Instance.onActiveHookSwinging += SetActiveHookSwinging;
    }

    protected void SetActiveHookSwinging(bool blActiveHookSwinging)
    {
        this.blActiveHookSwinging = blActiveHookSwinging;
        startPst = _transform.position;
    }

    protected void HookSwinging()
    {
        speedMove = 2f;
        float yOffset = AMPLITUDE * Mathf.Sin(FREQUENCE * Time.time * speedMove);

        // Áp dụng vị trí mới cho đối tượng
        _transform.position = startPst + new Vector3(0, yOffset, 0);
    }
}
