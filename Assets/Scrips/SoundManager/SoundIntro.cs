using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundIntro : GameMonoBehaviour
{
    protected override void Start()
    {
        base.Start();
        SoundManager.Instance.PlayAudio("BlueSea");
        StartCoroutine(PlaysoundWave());
    }

    private IEnumerator PlaysoundWave()
    {
        yield return new WaitForSeconds(3.25f);
        SoundManager.Instance.PlayAudio("WaveSound");
    }
}
