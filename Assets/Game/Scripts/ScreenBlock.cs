using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ScreenBlock : Singleton<ScreenBlock>, IPointerClickHandler
{
    private CanvasGroup canvasGroup;

    public System.Action<PointerEventData> actionBlockClicked;

    // Use this for initialization
    void Start()
    {
        canvasGroup = GetComponent<CanvasGroup>();

        GetComponent<Image>().enabled = true;
        EnableBlock(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void EnableBlock(bool enable)
    {
        canvasGroup.blocksRaycasts = enable;
        canvasGroup.alpha = enable ? 1.0f : 0.0f;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        actionBlockClicked.RaiseEvent(eventData);
    }
}
