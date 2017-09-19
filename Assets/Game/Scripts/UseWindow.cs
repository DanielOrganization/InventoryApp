using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UseWindow : MonoBehaviour, IPointerClickHandler
{
    public Button closeButton;
    public Button okButton;

    public RectTransform itemsGroup;

    public System.Action actionWindowClicked;
    public System.Action actionEnterNextScreen;

    public List<InventoryItem> inventoryItems = new List<InventoryItem>();

    // Use this for initialization
    void Start()
    {
        closeButton.onClick.AddListener(OnCloseButtonClicked);
        okButton.onClick.AddListener(OnOKButtonClicked);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCloseButtonClicked()
    {
        ShowWindow(false, true);
    }

    private void OnOKButtonClicked()
    {
        actionEnterNextScreen.RaiseEvent();
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

        MainController.InventorySystem.ShowPanel(show, true);

        MainController.Instance.IsPopupWindowShowing = show;

        IsShowing = show;
    }

    public bool IsShowing { get; set; }

    public void OnPointerClick(PointerEventData eventData)
    {
        actionWindowClicked.RaiseEvent();
    }

    public void PlaceInventoryItem(InventoryItem item)
    {
        item.transform.SetParent(itemsGroup);

        item.location = InventoryItem.Location.UseWindow;
    }
}
