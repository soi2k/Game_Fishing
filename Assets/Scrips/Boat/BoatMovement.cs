using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using System.Collections;
using Spine;
using Spine.Unity;

public class BoatMovement : Movement
{
    protected SkeletonAnimation skeletonBoat;
    protected IMoveToTarget imovetotarget;
    protected bool blOneTime = true;
    protected bool blStartMoveBoat = true;

    protected override void Start()
    {
        duration = 2.5f;
        timeElapse = 0;
        skeletonBoat = GetComponent<SkeletonAnimation>();
        imovetotarget = gameObject.GetComponent<IMoveToTarget>();
    }

    protected override void Update()
    {
        if (blStartMoveBoat)
        {
            blStartMoveBoat = false;
            StartCoroutine(BoatMoveIn());
        }    

        if (EventManager.Instance.blAnimEnding && blOneTime)
        {
            blOneTime = false;
            StartCoroutine(ActionEnding());
        }
    }
    private IEnumerator BoatMoveIn()
    {
        startPst = _transform.position;
        targetPst = new Vector2(0, _transform.position.y);
        imovetotarget.MoveToTarget(duration, startPst, targetPst);
        yield return new WaitForSeconds(2.5f);
        SoundManager.Instance.StopSound();
        EventManager.Instance.blMoveCameraHook = true;
    }

    protected IEnumerator ActionEnding()
    {
        skeletonBoat.AnimationState.SetAnimation(0, "Ending", false);
        yield return new WaitForSeconds(0.25f);
        SoundManager.Instance.PlayAudio("Victory");
        yield return new WaitForSeconds(1.5f);
        SoundManager.Instance._audiosource.loop = true;
        SoundManager.Instance.PlayAudio("Hurrah");
        yield return new WaitForSeconds(2.25f);
        skeletonBoat.AnimationState.SetAnimation(0, "Bien mat", false);
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(BoatMoveOut());
    }

    private IEnumerator BoatMoveOut()
    {
        startPst = _transform.position;
        targetPst = new Vector2(12, _transform.position.y);
        imovetotarget.MoveToTarget(duration, startPst, targetPst);
        yield return new WaitForSeconds(2.5f);
        SoundManager.Instance._audiosource.loop = false;
    }
}