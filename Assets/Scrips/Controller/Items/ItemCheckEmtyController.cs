using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCheckEmtyController :Singleton<ItemCheckEmtyController>
{
    private int ischeckEmty;
    private void Update()
    {
        ischeckEmty = transform.childCount;
        Debug.Log(ischeckEmty);
    }

    public bool IsEmty()
    {
        if (ischeckEmty == 0)
            return true;
        else return false;
    }
}
 