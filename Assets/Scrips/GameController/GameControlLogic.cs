using System.Collections;
using UnityEngine;
public class GameControlLogic : Movement
{
    protected Collider2D hitCollider;
    protected GameObject item;

    protected const float pstYItem = -15.5f;
    protected const float positionMidY = -11f;
    protected const float positionOutY = -2f;
    protected float pstXItem;
    protected float startPositionX;
    protected float startPositionY;
    protected float delaySoundTime;
    protected float numberRemovedItems;

    protected bool blWaittingClickItem = false;
    protected bool blCheckFirstAudioGuiding = true;
    protected bool blCallAudioGuiding = true;
    protected bool blActiveHookSwinging;
    protected bool blCheckSecondAudiGuiding;
    protected override void Update()
    {
        if (blCallAudioGuiding && blActiveHookSwinging)
        {
            blWaittingClickItem = true;
            StartCoroutine(AudioGuiding());
        } 

        ClickItem();
    }

    protected void OnEnable()
    {
        EventManager.Instance.onActiveHookSwinging += SetActiveHookSwinging;
    }

    protected void SetActiveHookSwinging(bool blActiveHookSwinging)
    {
        this.blActiveHookSwinging = blActiveHookSwinging;
    }
    protected void ClickItem()
    {
        // Kiểm tra xem người chơi đã click chuột chưa
        if (blWaittingClickItem )
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                hitCollider = Physics2D.OverlapPoint(mousePosition);

                if (hitCollider != null)
                {
                    blWaittingClickItem = false;
                    blActiveHookSwinging = false;
                    EventManager.Instance.OnActiveHookSwinging(blActiveHookSwinging);
                    SoundManager.Instance.StopSound();
                    item = hitCollider.gameObject;
                    Move();
                }
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
        else if (blCheckSecondAudiGuiding)
        {
            blCheckSecondAudiGuiding = false;
            while (delaySoundTime < 10)
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
        numberRemovedItems += 1;
        if (numberRemovedItems < 4)
        {
            // [Folow] Delay 0.25s => tiếng móc
            yield return new WaitForSeconds(0.25f);
            SoundManager.Instance.PlayAudio("HookSound");
            StartCoroutine(BackInitialPosition());
        }
        else
        {
            Hook.Instance.meshRenderer.enabled = false;
            EventManager.Instance.OnCameraMoveUp();
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
       
        blWaittingClickItem = true;
        blCallAudioGuiding = true;
        blActiveHookSwinging = true;
        blCheckSecondAudiGuiding = true;
        EventManager.Instance.OnActiveHookSwinging(blActiveHookSwinging);
    }
}





