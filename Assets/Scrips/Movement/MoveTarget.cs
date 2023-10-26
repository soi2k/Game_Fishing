using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour, IMoveToTarget
{
    private float timeElapse;
    public virtual void MoveToTarget(float duration, Vector3 startPst, Vector3 targetPst)
    {
        StartCoroutine(MoveIEnumerator(duration, startPst, targetPst));
    }

    protected IEnumerator MoveIEnumerator(float timeMove, Vector3 startPst, Vector3 targetPst)
    {
        timeElapse = 0;
        while (timeElapse < timeMove)
        {
            timeElapse += Time.deltaTime;
            float t = Mathf.Clamp01(timeElapse / timeMove);
            Vector3 newPosition = Vector3.Lerp(startPst, targetPst, t);
            transform.position = newPosition;
            yield return null;
        }
        timeElapse = 0;
    }
}
