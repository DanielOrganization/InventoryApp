﻿using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UseWindow : MonoBehaviour, IPointerClickHandler
{
    public Button closeButton;

    public RectTransform itemsGroup;
    
    public List<InventoryItem> inventoryItems = new List<InventoryItem>();

    // Use this for initialization
    void Start()
    {
        closeButton.onClick.AddListener(OnCloseButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCloseButtonClicked()
    {
        ShowWindow(false, true);
    }
    
    public void ShowWindow(bool show, bool animate)
    {
        gameObject.SetActive(true);

        float endValue = show ? 1.0f : 0.0f;
        if (animate)
        {
            transform.DOScale(endValue, 0.3f).SetEase(Ease.InQuad);
        }
        else
        {
            transform.localScale = Vector3.one * endValue;
        }

        if (show)
        {
            MainController.InventorySystem.ShowPanel(show, true);
        }

        MainController.Instance.IsPopupWindowShowing = show;
    }
    
    public void OnPointerClick(PointerEventData eventData)
    {
        ShowWindow(false, true);
    }
}
