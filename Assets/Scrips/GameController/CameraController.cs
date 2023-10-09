using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : Movement
{
    protected GameControlLogic gameControl;
    protected IMoveToTarget imovetotarget;

    [SerializeField] protected float numberRemovedItems;
    protected Vector3 startPosition;
    protected Vector3 targetPosition;

    protected bool blOneTimeMoveDown = true;
    protected bool blOneTimeMoveUp = true;

    protected override void Start()
    {
        Application.targetFrameRate = 60;
        duration = 1.0f;
        startPosition = _transform.position;
        targetPosition = new Vector3(startPosition.x, -17.5f,startPosition.z); 
        imovetotarget = gameObject.GetComponent<IMoveToTarget>();
    }

    protected override void Update()
    {
        if(blOneTimeMoveDown && EventManager.Instance.blMoveCameraHook)
        {
            blOneTimeMoveDown = false;
            StartCoroutine(MoveDown());
        }    
        numberRemovedItems = EventManager.Instance.numberRemovedItems;
        if (numberRemovedItems == 4 && blOneTimeMoveUp)
        {
            blOneTimeMoveUp = false;
            StartCoroutine(MoveUp());
        }
    }

    private IEnumerator MoveDown()
    {
        yield return new WaitForSeconds(0.75f); // Đợi 0.75 giây trước khi bắt đầu di chuyển
        imovetotarget.MoveToTarget(duration, startPosition, targetPosition);
    }

    protected IEnumerator MoveUp()
    {
        imovetotarget.MoveToTarget(duration, targetPosition, startPosition);
        yield return new WaitForSeconds(duration);
        EventManager.Instance.blAnimEnding = true;
    }
}