using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public abstract class BaseButton : GameMonoBehaviour
{
    [Header("Button")]
    [SerializeField] protected Button button;

    protected override void Start()
    {
        this.AddOnClickEvent();
    }

    // Update is called once per frame
    protected override void LoadComponentBase()
    {
        LoadButton();
    }

    protected virtual void LoadButton()
    {
        if (button != null) return;
        this.button = GetComponent<Button>();
    }    
    protected virtual void AddOnClickEvent()
    {
        button.onClick.AddListener(this.OnClick);
    }

    protected abstract void OnClick();
}
