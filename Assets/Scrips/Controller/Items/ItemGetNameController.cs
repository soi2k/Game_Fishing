using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGetNameController : MonoBehaviour
{
    [SerializeField] private List<ItemName> lstitemname;
    public static string getName;
    private enum ItemName
    {
        Hat,
        Handbag,
        Watch,
        Sandals,
    }
    private void OnMouseDown()
    {
        if(lstitemname != null)
            getName = lstitemname[0].ToString();
    }
}
 