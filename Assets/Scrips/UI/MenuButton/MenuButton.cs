using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButton : BaseButton
{
    protected override void OnClick()
    {
        Transform parentTransform = transform.parent; // Lấy parent của object hiện tại
        GameObject Item = GameObject.Find("Items");
        // Duyệt qua tất cả các sibling
        for (int i = 0; i < parentTransform.childCount; i++)
        {
            Transform siblingTransform = parentTransform.GetChild(i);
            if (siblingTransform.name == "Dialog")
            {
                // Tìm thấy sibling object, bật
                siblingTransform.gameObject.SetActive(true);
            }
        }

        foreach (Transform childItem in Item.transform)
        {
            Collider2D childColider = childItem.GetComponent<Collider2D>();
            childColider.enabled = false;
        }
    }
}
