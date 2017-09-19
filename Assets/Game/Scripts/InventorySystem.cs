using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using System;

public class InventorySystem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Transform itemParent;

    private List<InventoryItem> inventoryItems = new List<InventoryItem>();

    private bool needShow = false;

    // Use this for initialization
    void Start()
    {
        ShowPanel(false, true);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddInventoryItem(InventoryItem item)
    {
        item.transform.SetParent(itemParent);

        item.location = InventoryItem.Location.InventorySystem;

        inventoryItems.Add(item);
    }

    public void RemoveInventoryItem(InventoryItem item)
    {
        inventoryItems.Remove(item);
    }

    public void ShowPanel(bool show, bool force)
    {
        if(force)
            needShow = show;

        float endValue = show ? 0 : -80;
        gameObject.GetComponent<RectTransform>().DOAnchorPosY(endValue, 0.3f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowPanel(true, false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (needShow)
            return;

        ShowPanel(false, false);
    }
}
