using System.Collections;
using UnityEngine;
public class GameControlLogic : Movement
{
    protected Collider2D hitCollider;
    protected GameObject item;

    protected float pstXItem;
    protected float pstYItem;
    protected float startPositionX;
    protected float startPositionY;
    protected float positionMidY;
    protected float positionOutY;
    protected float delaySoundTime;

    protected bool blWaittingClickItem = false;
    protected bool blCheckFirstAudioGuiding = true;
    protected bool blCallAudioGuiding = true;
    protected override void Update()
    {
        if (blCallAudioGuiding && EventManager.Instance.blActiveHookSwinging)
        {
            blWaittingClickItem = true;
            StartCoroutine(AudioGuiding());
        } 
        ClickItem();
    }

    protected void ClickItem()
    {
        // Kiểm tra xem người chơi đã click chuột chưa
        if (blWaittingClickItem && Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hitCollider = Physics2D.OverlapPoint(mousePosition);

            if (hitCollider != null)
            {
                blWaittingClickItem = false;
                EventManager.Instance.blActiveHookSwinging = false;
                SoundManager.Instance.StopSound();
                item = hitCollider.gameObject;
                Move();
            }
        }
    }

    protected IEnumerator AudioGuiding()
    {
        blCallAudioGuiding = false;
        delaySoundTime = 0;

        if (blCheckFirstAudioGuiding)
        {
            blCheckFirstAudioGuiding = false;
            SoundManager.Instance.PlayAudio("AudioGuiding");
            while (delaySoundTime < 13)
            {
                if (!blWaittingClickItem) yield break;
                delaySoundTime += Time.deltaTime;
                yield return null;
            }
            blCallAudioGuiding = true;
        }

        else
        {
            SoundManager.Instance.PlayAudio("AudioGuiding");
            while (delaySoundTime < 8)
            {
                if (!blWaittingClickItem) yield break;
                delaySoundTime += Time.deltaTime;
                yield return null;
            }
            blCallAudioGuiding = true;
        }
    }

    protected override void Move()
    {
        //Lấy tọa độ x của item
        startPositionX = Hook.Instance.transform.position.x;
        startPositionY = Hook.Instance.transform.position.y;
        pstXItem = item.transform.position.x + 1.89f;
        positionMidY = -11f;
        positionOutY = -2f;
        pstYItem = -15.5f;
        StartCoroutine(MoveItempstX());
    }

    protected IEnumerator MoveItempstX()
    {
        duration = 0.2f;
        startPst = Hook.Instance.transform.position;
        targetPst = new Vector3(pstXItem, startPst.y,startPst.z);
        Hook.Instance.MoveToDistination(duration, startPst, targetPst);
        yield return new WaitForSeconds(duration);
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(MoveItempstY());
    }

    protected IEnumerator MoveItempstY()
    {
        SoundManager.Instance.PlayAudio("HookSound");
        duration = 1f;
        startPst = Hook.Instance.transform.position;
        targetPst = new Vector3(pstXItem, pstYItem,startPst.z);
        Hook.Instance.MoveToDistination(duration, startPst, targetPst);
        yield return new WaitForSeconds(duration);

        // Set item => con Hook
        item.transform.SetParent(Hook.Instance.transform);

        //Delay 0.25s sau đó móc
        yield return new WaitForSeconds(0.25f);
        Hook.Instance.AnimOpen();
        yield return new WaitForSeconds(0.2f);
        Hook.Instance.AnimClose();

        // Delay 0.25s => mid
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(MoveMid());
    }

    protected IEnumerator MoveMid()
    {
        SoundManager.Instance.PlayAudio("HookSound");
        duration = 0.5f;
        startPst = Hook.Instance.transform.position;
        targetPst = new Vector3(Hook.Instance.transform.position.x, positionMidY, startPst.z);
        Hook.Instance.MoveToDistination(duration, startPst, targetPst);
        yield return new WaitForSeconds(duration);

        // Delay 0.25s => audio từ
        yield return new WaitForSeconds(0.25f);

        // và phát âm thanh Item

        SoundManager.Instance.PlayAudio(item.name);
        ItemName.Instance.DisplayItemName(item.name);
        yield return new WaitForSeconds(1.4f);
        StartCoroutine(MoveOut());
    }

    protected IEnumerator MoveOut()
    {
        duration = 0.5f;
        startPst = Hook.Instance.transform.position;
        targetPst = new Vector3(startPst.x, positionOutY, startPst.z);
        Hook.Instance.MoveToDistination(duration, startPst, targetPst);
        yield return new WaitForSeconds(duration);

        Destroy(item);
        EventManager.Instance.numberRemovedItems += 1;
        if (EventManager.Instance.numberRemovedItems != 4)
        {
            // [Folow] Delay 0.25s => tiếng móc
            yield return new WaitForSeconds(0.25f);
            SoundManager.Instance.PlayAudio("HookSound");
            StartCoroutine(BackInitialPosition());
        }
        else
        {
            Hook.Instance.meshRenderer.enabled = false;
            WinState.Instance.Active();
        }
    }

    protected IEnumerator BackInitialPosition()
    {
        duration = 0.4f;
        startPst = _transform.position;
        targetPst = new Vector3(startPositionX, startPositionY,startPst.z);
        Hook.Instance.MoveToDistination(duration, startPst, targetPst);
        yield return new WaitForSeconds(duration);

        // [Folow] Delay 0.2s => set lượt chơi mới
        yield return new WaitForSeconds(0.2f);
        if (EventManager.Instance.numberRemovedItems < 4)
        {
            blWaittingClickItem = true;
            blCheckFirstAudioGuiding = true;
            blCallAudioGuiding = true;
            EventManager.Instance.blActiveHookSwinging = true;
        }
    }
}





