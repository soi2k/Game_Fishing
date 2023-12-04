using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Collections;
using Spine;
using Spine.Unity;

public class BoatMoveInController : Subject
{
    protected IMoveToTarget moveToTarget;

    protected override void Start()
    {
        timeMove = 2.5f;
        moveToTarget = gameObject.GetComponent<IMoveToTarget>();
        StartCoroutine(BoatMoveIn());
    }

    private IEnumerator BoatMoveIn()
    {
        SoundManager.Instance.PlayAudio("BlueSea");
        startPst = _transform.position;
        targetPst = new Vector2(0, _transform.position.y);
        moveToTarget.MoveToTarget(timeMove, startPst, targetPst);
        yield return new WaitForSeconds(2.5f);
        SoundManager.Instance.StopSound();
        NotifyObserver();
    }
}