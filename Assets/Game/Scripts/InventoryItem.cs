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

    public System.Action<InventoryItem> actionItemClicked;

    public Location location = Location.FindWindow;

    public enum Location
    {
        FindWindow,
        InventorySystem,
        UseWindow,
    }

    // Use this for initialization
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnItemClicked);

        ItemData data = MainController.Instance.GetItemData(itemType);
        targetImage.sprite = data.sprite;
        targetImage.SetNativeSize();
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
        actionItemClicked.RaiseEvent(this);
    }

}

