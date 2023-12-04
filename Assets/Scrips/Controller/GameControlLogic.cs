using System.Collections;
using UnityEngine;
public class GameControlLogic : AbsMovement
{
    private GameObject item;
    private IGetAnimationHook getAnimationHook;
    private ISetActiveRenderer setActiveRenderer;

    private const float POSITION_MID_Y = -11f;
    private const float POSITION_OUT_Y = -2f;

    private float pstXItem;
    private float pstYItem;
    private float startPositionHookX;
    private float startPositionHookY;
    private float delaySoundTime;

    private bool blWaittingClickItem = false;
    private bool blCheckFirstAudioGuiding = true;
    private bool blCheckSecondAudiGuiding;
    private bool blCallAudioGuiding = false;

    protected override void Start()
    {
        getAnimationHook = gameObject.GetComponent<IGetAnimationHook>();
        setActiveRenderer = gameObject.GetComponent<ISetActiveRenderer>();
    }
    protected override void Update()
    {
        if (blCallAudioGuiding)
        {
            StartCoroutine(AudioGuiding());   
        } 
        ClickItem();
    }

    protected void OnEnable()
    {
        EventManager.Instance.onActiveHookSwinging += SetActiveHookSwinging;
    }

    protected void SetActiveHookSwinging(bool blPlayStart)
    {
        blWaittingClickItem = blPlayStart;
        blCallAudioGuiding = blPlayStart;
    }
    protected void ClickItem()
    {
        if (blWaittingClickItem)
        {
            if (ItemCheckClickController.Instance.isClickItem())
            {
                blWaittingClickItem = false;
                EventManager.Instance.OnActiveHookSwinging(blWaittingClickItem);
                SoundManager.Instance.StopSound();
                item = ItemCheckClickController.Instance.hitCollider.gameObject;
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
        startPositionHookX = HookMoveController.Instance.transform.position.x;
        startPositionHookY = HookMoveController.Instance.transform.position.y;
        pstXItem = item.transform.position.x;
        pstYItem = item.transform.position.y;
        StartCoroutine(MoveItempstX());
    }

    protected IEnumerator MoveItempstX()
    {
        timeMove = 0.2f;
        startPst = HookMoveController.Instance.transform.position;
        targetPst = new Vector3(pstXItem, startPst.y,startPst.z);
        HookMoveController.Instance.MoveToDistination(timeMove, startPst, targetPst);
        yield return new WaitForSeconds(timeMove);
        yield return new WaitForSeconds(0.2f);
        StartCoroutine(MoveItempstY());
    }

    protected IEnumerator MoveItempstY()
    {
        SoundManager.Instance.PlayAudio("HookSound");
        timeMove = 1f;
        startPst = HookMoveController.Instance.transform.position;
        targetPst = new Vector3(pstXItem, pstYItem, startPst.z);
        HookMoveController.Instance.MoveToDistination(timeMove, startPst, targetPst);
        yield return new WaitForSeconds(timeMove);

        // Set item => con Hook
        item.transform.SetParent(HookMoveController.Instance.transform);

        //Delay 0.25s sau đó móc
        yield return new WaitForSeconds(0.25f);
        getAnimationHook.GetAnimOpen();
        yield return new WaitForSeconds(0.2f);
        getAnimationHook.GetAnimClose();

        // Delay 0.25s => mid
        yield return new WaitForSeconds(0.25f);
        StartCoroutine(MoveMid());
    }

    protected IEnumerator MoveMid()
    {
        SoundManager.Instance.PlayAudio("HookSound");
        timeMove = 0.5f;
        startPst = HookMoveController.Instance.transform.position;
        targetPst = new Vector3(HookMoveController.Instance.transform.position.x, POSITION_MID_Y, startPst.z);
        HookMoveController.Instance.MoveToDistination(timeMove, startPst, targetPst);
        yield return new WaitForSeconds(timeMove);

        // Delay 0.25s => audio từ
        yield return new WaitForSeconds(0.25f);

        // và phát âm thanh Item
        SoundManager.Instance.PlayAudio(ItemGetNameController.getName);
        ItemDisplayNameController.Instance.DisplayItemName(ItemGetNameController.getName);
        yield return new WaitForSeconds(1.4f);
        StartCoroutine(MoveOut());
    }

    protected IEnumerator MoveOut()
    {
        timeMove = 0.5f;
        startPst = HookMoveController.Instance.transform.position;
        targetPst = new Vector3(startPst.x, POSITION_OUT_Y, startPst.z);
        HookMoveController.Instance.MoveToDistination(timeMove, startPst, targetPst);
        yield return new WaitForSeconds(timeMove);

        Destroy(item);
        if (ItemCheckEmtyController.Instance.IsEmty())
        {
            setActiveRenderer.SetActiveRenderer(false);
            EventManager.Instance.OnCameraMoveUp();
            WinStateController.Instance.Active();
        }
        else
        {
            yield return new WaitForSeconds(0.25f);
            SoundManager.Instance.PlayAudio("HookSound");
            StartCoroutine(BackInitialPosition());
        }
    }

    protected IEnumerator BackInitialPosition()
    {
        timeMove = 0.4f;
        startPst = _transform.position;
        targetPst = new Vector3(startPositionHookX, startPositionHookY,startPst.z);
        HookMoveController.Instance.MoveToDistination(timeMove, startPst, targetPst);
        yield return new WaitForSeconds(timeMove);

        // [Folow] Delay 0.2s => set lượt chơi mới
        yield return new WaitForSeconds(0.2f);
       
        blWaittingClickItem = true;
        blCallAudioGuiding = true;
        blCheckSecondAudiGuiding = true;
        EventManager.Instance.OnActiveHookSwinging(blWaittingClickItem);
    }
}





