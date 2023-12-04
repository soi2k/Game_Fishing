using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCheckClickController : Singleton<ItemCheckClickController>
{
    public Collider2D hitCollider;
    // Update is called once per frame
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            hitCollider = Physics2D.OverlapPoint(mousePosition);
        }
    }

    public bool isClickItem()
    {
        if (hitCollider != null) return true;
        else return false;
    }
}
