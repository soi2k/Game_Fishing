using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMoveController : AbsMovement,IObserver
{
    protected IMoveToTarget moveToTarget;
    private Subject subject;

    protected const float targetPstY = -17.5f;

    protected override void Start()
    {
        timeMove = 1.0f;
        startPst = _transform.position;
        targetPst = new Vector3(startPst.x, targetPstY, startPst.z);
        moveToTarget = gameObject.GetComponent<IMoveToTarget>();
    }

    private void OnEnable()
    {
        subject = FindObjectOfType<BoatMoveInController>();
        subject.AddObserver(this);
        EventManager.Instance.onCameraMoveUp += CameraMoveUp;
    }
    private void OnDisable()
    {
        subject.RemoveObserver(this);
    }
    protected void CameraMoveUp()
    {
        StartCoroutine(MoveUp());
    }
    public void OnNotify()
    {
        StartCoroutine(MoveDown());
    }
    private IEnumerator MoveDown()
    {
        yield return new WaitForSeconds(0.75f); // Đợi 0.75 giây trước khi bắt đầu di chuyển
        moveToTarget.MoveToTarget(timeMove, startPst, targetPst);
    }

    protected IEnumerator MoveUp()
    {
        moveToTarget.MoveToTarget(timeMove, targetPst, startPst);
        yield return new WaitForSeconds(timeMove);
        EventManager.Instance.OnWinState();
    }
}