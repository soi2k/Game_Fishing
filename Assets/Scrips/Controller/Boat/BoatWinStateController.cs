using System.Collections;
using UnityEngine;

public class BoatWinStateController : AbsMovement
{
    protected IGetAnimationBoat getAnimationBoat ;
    protected IMoveToTarget moveToTarget;

    protected override void Start()
    {
        timeMove = 2.5f;
        getAnimationBoat = gameObject.GetComponent<IGetAnimationBoat>();
        moveToTarget = gameObject.GetComponent<IMoveToTarget>();
    }
    protected void OnEnable()
    {
        EventManager.Instance.onWinState += WinSate;
    }

    protected void WinSate()
    {
        StartCoroutine(BoatWinState());
    }
    protected IEnumerator BoatWinState()
    {
        getAnimationBoat.GetAnimEnding();
        yield return new WaitForSeconds(0.25f);
        SoundManager.Instance.PlayAudio("Victory");
        yield return new WaitForSeconds(1.5f);
        SoundManager.Instance._audiosource.loop = true;
        SoundManager.Instance.PlayAudio("Hurrah");
        yield return new WaitForSeconds(4.5f);
        SoundManager.Instance._audiosource.loop = false;
        yield return new WaitForSeconds(0.25f);
        
        // Move off the Screen
        startPst = _transform.position;
        targetPst = new Vector2(12, _transform.position.y);
        moveToTarget.MoveToTarget(timeMove, startPst, targetPst);
        yield return new WaitForSeconds(2.5f);
    }
}
