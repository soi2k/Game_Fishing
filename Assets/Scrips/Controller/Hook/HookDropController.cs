using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookDropController : AbsMovement,IObserver
{
    protected IMoveToTarget moveToTarget;
    ISetActiveRenderer setActiveRenderer;
    private Subject subject;

    protected const float TARGET_Y = -6.6f;

    public bool blActiveHookSwinging = false;
    protected override void Start()
    {
        moveToTarget = gameObject.GetComponent<IMoveToTarget>();
        setActiveRenderer = gameObject.GetComponent<ISetActiveRenderer>();
        speedMove = 2f;
    }

    private void OnEnable()
    {
        subject = FindObjectOfType<BoatMoveInController>();
        subject.AddObserver(this);
    }
    private void OnDisable()
    {
        subject.RemoveObserver(this);
    }
    public void OnNotify()
    {
        StartCoroutine(Drop());
    }
    private IEnumerator Drop()
    {
        yield return new WaitForSeconds(0.75f); 
        SoundManager.Instance.PlayAudio("WaveSound");
        timeMove = 1.75f;
        startPst = _transform.position;
        targetPst = new Vector3(startPst.x, TARGET_Y, startPst.z);
        moveToTarget.MoveToTarget(timeMove, startPst, targetPst);
        yield return new WaitForSeconds(0.75f);
        setActiveRenderer.SetActiveRenderer(true);
        yield return new WaitForSeconds(1f);
        blActiveHookSwinging = true;
        EventManager.Instance.OnActiveHookSwinging(blActiveHookSwinging);
    }
}


        

