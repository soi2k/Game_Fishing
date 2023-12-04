using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatAnimationController : MonoBehaviour, IGetAnimationBoat
{
    protected SkeletonAnimation skeletonBoat;

    protected void Start()
    {
        skeletonBoat = GameObject.Find("BoatCtrl").GetComponent<SkeletonAnimation>();
    }

    public void GetAnimEnding()
    {
        skeletonBoat.AnimationState.SetAnimation(0, "Ending", false);
    }
}
