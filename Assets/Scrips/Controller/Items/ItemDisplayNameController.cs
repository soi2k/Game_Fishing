using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDisplayNameController : Singleton<ItemDisplayNameController>
{
    public void DisplayItemName(string textItemName)
    {
        StartCoroutine(Display(textItemName));
    }
    protected IEnumerator Display(string textItemName)
    {
        for(int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            Transform childItem = this.gameObject.transform.GetChild(i);
            if(childItem.name  == "text" + textItemName)
            {
                childItem.gameObject.SetActive(true);
                yield return new WaitForSeconds(1);
                childItem.gameObject.SetActive(false);
                yield break;
            }
        }
    }
}
