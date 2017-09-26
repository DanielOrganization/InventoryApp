using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private RectTransform itemParent;

    [SerializeField]
    private RectTransform scrollView;
    [SerializeField]
    private RectTransform scrollViewContent;

    [SerializeField]
    private Button hintButton;

    [SerializeField]
    private GameObject hintAreaPrefab;

    [SerializeField]
    private Transform collectPoint;

    [SerializeField]
    private Button toogleButton;

    [SerializeField]
    private Button leftPageButton;

    [SerializeField]
    private Button rightPageButton;

    private List<InventoryItem> inventoryItems = new List<InventoryItem>();

    private bool needShow = false;

    private bool m_IsShowing = false;

    public System.Action actionHintClicked;

    private int m_PageIndex = 0;

    // Use this for initialization
    void Start()
    {
        ShowPanel(true, true);

        hintButton.onClick.AddListener(OnHintButtonClicked);

        toogleButton.onClick.AddListener(OnToggleButtonClicked);

        leftPageButton.onClick.AddListener(delegate ()
        {
            ScrollPage(-1);
        });

        rightPageButton.onClick.AddListener(delegate ()
        {
            ScrollPage(1);
        });
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddInventoryItem(InventoryItem item)
    {
        item.transform.localScale = Vector3.one;
        item.transform.SetParent(itemParent);
        item.ItemLocation = InventoryItem.Location.InventorySystem;
        inventoryItems.Add(item);
    }

    public void RemoveInventoryItem(InventoryItem item)
    {
        inventoryItems.Remove(item);
    }

    public void ShowPanel(bool show, bool force)
    {
        if (force)
            needShow = show;

        m_IsShowing = show;

        float endValue = show ? 0 : -80;
        gameObject.GetComponent<RectTransform>().DOAnchorPosY(endValue, 0.3f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //    ShowPanel(true, false);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //         if (needShow)
        //             return;
        // 
        //         ShowPanel(false, false);
    }

    public void OnHintButtonClicked()
    {
        actionHintClicked.RaiseEvent();
    }

    public void OnToggleButtonClicked()
    {
        ShowPanel(!m_IsShowing, false);
    }

    public void ShowHintAreaInScene(Transform hintAreaParent, Vector3 pos)
    {
        HintArea hintArea = GlobalTools.AddChild<HintArea>(hintAreaParent.gameObject, hintAreaPrefab);

        hintArea.transform.localPosition = hintAreaParent.InverseTransformPoint(pos);
    }

    private void ScrollPage(int offset)
    {
        int pageCount = Mathf.CeilToInt(scrollViewContent.sizeDelta.x / scrollView.sizeDelta.x);
        if (pageCount == 0)
            return;

        m_PageIndex = Mathf.Clamp(m_PageIndex + offset, 0, pageCount - 1);

        float posX = -scrollView.sizeDelta.x * m_PageIndex;
        scrollViewContent.SetRectTransformAnchoredPositionX(posX);
    }
}
