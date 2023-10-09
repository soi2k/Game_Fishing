using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;
using TMPro;
public class Hook : Singleton<Hook>
{
    protected float timeElapse = 0;
    public SkeletonAnimation skeletonAnimation;
    public MeshRenderer meshRenderer;

    protected IMoveToTarget imovement;
    protected override void Start()
    {
        base.Start();
        imovement = gameObject.GetComponent<IMoveToTarget>();
    }
    protected override void LoadComponentBase()
    {
        base.LoadComponentBase();
        LoadSkeletonAnimation();
        LoadMesrenderer();
    }

    protected virtual void LoadSkeletonAnimation()
    {
        if (this.skeletonAnimation != null) return;
        this.skeletonAnimation = GetComponent<SkeletonAnimation>();
    }

    protected virtual void LoadMesrenderer()
    {
        if (this.meshRenderer != null) return;
        this.meshRenderer = GetComponent<MeshRenderer>();
    }
    public void MoveToDistination(float duration, Vector3 startPst, Vector3 targetPst)
    {
        imovement.MoveToTarget(duration, startPst, targetPst);
        
    }

    public void AnimOpen()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "Moc gap do Open", false);
    }

    public void AnimClose()
    {
        skeletonAnimation.AnimationState.SetAnimation(0, "Moc gap do Close", false);
    }
}
