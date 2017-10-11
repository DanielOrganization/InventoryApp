using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// Find Inventory Window.
/// </summary>
public class FindWindow : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]
    private Button closeButton;

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

    /// <summary>
    /// play the window animation when show or hide, Enable the screen block when show and disable when hide.
    /// </summary>
    /// <param name="show"></param>
    /// <param name="animate"></param>
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
            ScreenBlock.Instance.EnableBlock(true);
            ScreenBlock.Instance.transform.SetParent(transform.parent);
            ScreenBlock.Instance.transform.SetAsFirstSibling();

            MainController.InventorySystem.ShowPanel(show, true);
        }
        else
        {
            ScreenBlock.Instance.EnableBlock(false);
            ScreenBlock.Instance.RestoreParent();
        }

        MainController.Instance.IsPopupWindowShowing = show;
    }

    /// <summary>
    /// Hide FindWindow when clicked
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        ShowWindow(false, true);
    }
}
