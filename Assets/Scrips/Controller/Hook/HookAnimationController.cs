using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HookAnimationController : MonoBehaviour, IGetAnimationHook
{
    protected SkeletonAnimation skeletonHook;

    protected void Start()
    {
        skeletonHook = GameObject.Find("HookCtrl").GetComponent<SkeletonAnimation>();
    }

    public void GetAnimOpen()
    {
        skeletonHook.AnimationState.SetAnimation(0, "Moc gap do Open", false);
    }
    public void GetAnimClose()
    {
        skeletonHook.AnimationState.SetAnimation(0, "Moc gap do Close", false);
    }
}
