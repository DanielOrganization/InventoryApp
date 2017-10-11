using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Drag item function
/// </summary>
public class DragMe : MonoBehaviour
{
    private bool m_Dragging = false;

    [HideInInspector]
    public Transform draggingParent;

    private CanvasGroup canvasGroup;

    private Transform parentBeforeDrag;

    public System.Action<bool> actionDragFinished;

    // Use this for initialization
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnItemClicked);

        canvasGroup = transform.EnsureComponent<CanvasGroup>();

        draggingParent = ScreenBlock.Instance.transform;
    }

    // Update is called once per frame
    void Update()
    {
        // Convert the mouse position to the local position of dragging parent.
        if(m_Dragging)
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 localPos = draggingParent.InverseTransformPoint(pos);
            transform.localPosition = new Vector3(localPos.x, localPos.y, 0);
        }
    }
    
    private void OnItemClicked()
    {
        if (!m_Dragging)
        {
            parentBeforeDrag = transform.parent;
            transform.SetParent(draggingParent);

            StartDrag();
        }
    }

    /// <summary>
    /// Stop Drag when the screen block is clicked in dragging mode.
    /// </summary>
    /// <param name="eventData"></param>
    private void OnScreenBlockClicked(PointerEventData eventData)
    {
        StopDrag();

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        DropMe dropMe = null;
        foreach(var result in results)
        {
            dropMe = result.gameObject.GetComponent<DropMe>();
            if (dropMe != null)
            {
                break;
            }
        }

        if(dropMe != null && dropMe.AllowDropped(this))
        {
            // Allow to be dropped: place the item to the new parent
            transform.SetParent(dropMe.transform);

            actionDragFinished.RaiseEvent(true);
        }
        else
        {
            // Don't allow to be dropped: place the item to the parent before
            transform.SetParent(parentBeforeDrag);

            actionDragFinished.RaiseEvent(false);
        }
    }

    /// <summary>
    /// Enable Screen Block when the drag started.
    /// </summary>
    private void StartDrag()
    {
        m_Dragging = true;

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        ScreenBlock.Instance.EnableBlock(true);
        ScreenBlock.Instance.actionBlockClicked += OnScreenBlockClicked;
    }

    /// <summary>
    /// Disable Screen Block when the drag finished.
    /// </summary>
    private void StopDrag()
    {
        m_Dragging = false;

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        ScreenBlock.Instance.EnableBlock(false);
        ScreenBlock.Instance.actionBlockClicked -= OnScreenBlockClicked;
    }
}
