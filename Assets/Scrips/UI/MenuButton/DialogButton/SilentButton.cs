using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SilentButton : BaseButton
{
    [SerializeField] protected TextMeshProUGUI tmp;
    protected bool isActive = true;
    protected override void OnClick()
    {   
        Transform childtransform = transform.Find("Text_ActiveSound");
        if (isActive)
        {
            SoundManager.Instance._audiosource.enabled = false;
            childtransform.GetComponent<TextMeshProUGUI>().text = "Bật Âm";
            isActive = false;
        }
        else
        {
            SoundManager.Instance._audiosource.enabled = true;
            childtransform.GetComponent<TextMeshProUGUI>().text = "Tắt Âm";
            isActive = true;
        }    

    }
}
       
