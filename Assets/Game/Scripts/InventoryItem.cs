using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItem : MonoBehaviour
{
    [SerializeField]
    private Image targetImage;

    [SerializeField]
    private ItemType itemType;
    
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

