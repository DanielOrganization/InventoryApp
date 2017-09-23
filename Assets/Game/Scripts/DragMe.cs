using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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
    }

    // Update is called once per frame
    void Update()
    {
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

    private void OnScreenBlockClicked(PointerEventData eventData)
    {
        StopDrag();

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);

        DropMe dropMe = null;
        foreach(var result in results)
        {
            if(result.gameObject.GetComponent<DropMe>() != null)
            {
                dropMe = result.gameObject.GetComponent<DropMe>();
                break;
            }
        }

        if(dropMe == null)
        {
            transform.SetParent(parentBeforeDrag);

            actionDragFinished.RaiseEvent(false);
        }
        else
        {
            transform.SetParent(dropMe.transform);

            actionDragFinished.RaiseEvent(true);
        }
    }

    private void StartDrag()
    {
        m_Dragging = true;

        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        ScreenBlock.Instance.EnableBlock(true);
        ScreenBlock.Instance.actionBlockClicked += OnScreenBlockClicked;
    }

    private void StopDrag()
    {
        m_Dragging = false;

        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        ScreenBlock.Instance.EnableBlock(false);
        ScreenBlock.Instance.actionBlockClicked -= OnScreenBlockClicked;
    }
}
