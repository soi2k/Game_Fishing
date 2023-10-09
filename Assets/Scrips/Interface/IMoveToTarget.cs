using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMoveToTarget
{
    void MoveToTarget(float duration, Vector3 startPst, Vector3 targetPst);
}
