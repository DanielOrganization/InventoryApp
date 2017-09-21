using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventorySystem : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Transform itemParent;

    [SerializeField]
    private Button hintButton;

    [SerializeField]
    private GameObject hintAreaPrefab;

    private List<InventoryItem> inventoryItems = new List<InventoryItem>();

    private bool needShow = false;

    public System.Action actionHintClicked;

    // Use this for initialization
    void Start()
    {
        ShowPanel(false, true);

        hintButton.onClick.AddListener(OnHintButtonClicked);
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
        // show Inventory Panel always
//         if(force)
//             needShow = show;
// 
//         float endValue = show ? 0 : -80;
//         gameObject.GetComponent<RectTransform>().DOAnchorPosY(endValue, 0.3f);
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

    public void OnHintButtonClicked()
    {
        actionHintClicked.RaiseEvent();
    }

    public void ShowHintAreaInScene(Transform hintAreaParent, Vector3 pos)
    {
        HintArea hintArea = GlobalTools.AddChild<HintArea>(hintAreaParent.gameObject, hintAreaPrefab);
        hintArea.transform.localPosition = pos;
    }
}
