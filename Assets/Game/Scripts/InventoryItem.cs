using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Inventory item: it can be clicked, dragged and dropped.
/// </summary>
public class InventoryItem : MonoBehaviour
{
    [SerializeField]
    private Image targetImage;

    [SerializeField]
    private ItemType itemType;

    [SerializeField]
    private Text itemState;

    private Location location = Location.FindWindow;

    private DragMe dragMe;

    public enum Location
    {
        FindWindow,
        InventorySystem,
        Used,
    }

    // Use this for initialization
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnItemClicked);

        ItemData data = MainController.Instance.GetItemData(itemType);
        targetImage.sprite = data.sprite;
        targetImage.SetNativeSize();

        dragMe = transform.EnsureComponent<DragMe>();
        dragMe.actionDragFinished += OnDropFinished;
        dragMe.enabled = false;

        this.ItemLocation = Location.FindWindow;
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public ItemType ItemType
    {
        set
        {
            itemType = value;

            ItemData data = MainController.Instance.GetItemData(itemType);
            targetImage.sprite = data.sprite;
            targetImage.SetNativeSize();
        }
        get
        {
            return itemType;
        }
    }
    
    private void OnItemClicked()
    {
        if(this.ItemLocation == Location.FindWindow)
        {
            MainController.InventorySystem.AddInventoryItem(this);
        }
    }

    public Location ItemLocation
    {
        get
        {
            return location;
        }
        set
        {
            location = value;

            if (location == Location.InventorySystem)
            {
                dragMe.enabled = true;
            }
            else
            {
                dragMe.enabled = false;
            }

            string state = "";
            switch(location)
            {
                case Location.FindWindow:
                    state = "Unfound";
                    break;

                case Location.InventorySystem:
                    state = "Found&Unused";
                    break;

                case Location.Used:
                    state = "Used";
                    break;
            }
            itemState.text = state;
        }
    }

    private void OnDropFinished(bool success)
    {
        if(success)
        {
            MainController.InventorySystem.RemoveInventoryItem(this);

            this.ItemLocation = InventoryItem.Location.Used;
        }
    }
}

