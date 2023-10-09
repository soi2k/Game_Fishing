using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookParent : Movement
{
    protected IMoveToTarget imovement;

    [SerializeField] private float targetY = -6.6f;
    protected float amplitude = 0.2f;     // Biên độ của chuyển động
    protected float frequency = 1f;     // Tần số của chuyển động

    protected bool blOneMoveHook = true;

    protected Vector3 startSwingingPst;

    protected override void Start()
    {
        imovement = gameObject.GetComponent<IMoveToTarget>();

        moveSpeed = 2f;
    }
    protected override void Update()
    {
        if (blOneMoveHook && EventManager.Instance.blMoveCameraHook)
        {
            blOneMoveHook = false;
            StartCoroutine(HookDropStart());
        }
        if (EventManager.Instance.blActiveHookSwinging)
        {
            HookSwinging();
        }
    }
    private IEnumerator HookDropStart()
    {
        yield return new WaitForSeconds(0.75f); // [Note] Đợi 0.75 giây trước khi bắt đầu di chuyển
        duration = 1.75f;
        startPst = _transform.position;
        targetPst = new Vector3(startPst.x, targetY, startPst.z);
        imovement.MoveToTarget(duration, startPst, targetPst);
        yield return new WaitForSeconds(0.75f);
        Hook.Instance.meshRenderer.enabled = true;
        yield return new WaitForSeconds(1f);
        startSwingingPst = targetPst;
        EventManager.Instance.blActiveHookSwinging = true;
    }

    protected void HookSwinging()
    {
        float yOffset = amplitude * Mathf.Sin(frequency * Time.time * moveSpeed);

        // Áp dụng vị trí mới cho đối tượng
        transform.position = startSwingingPst + new Vector3(0, yOffset, 0);
    }
}


        

