using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using TMPro;
public class HookMoveController : Singleton<HookMoveController>
{
    protected float timeElapse = 0;
    protected IMoveToTarget moveToTarget ;
    protected void Awake()
    {
        moveToTarget = gameObject.GetComponent<IMoveToTarget>();
    }

    public void MoveToDistination(float duration, Vector3 startPst, Vector3 targetPst)
    {
        moveToTarget.MoveToTarget(duration, startPst, targetPst);     
    }
}
