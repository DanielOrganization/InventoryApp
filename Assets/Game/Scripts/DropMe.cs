using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Drop Area Controller: detect whether the area is allowed to be placed.
/// </summary>
public class DropMe : MonoBehaviour
{
    public ItemType allowDropType;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool AllowDropped(DragMe dragMe)
    {
        InventoryItem item = dragMe.GetComponent<InventoryItem>();
        if(item != null)
        {
            if (item.ItemType == allowDropType)
                return true;
        }

        return false;
    }
}
