﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Prevent users from clicking the button behind the window
/// </summary>
public class ScreenBlock : Singleton<ScreenBlock>, IPointerClickHandler
{
    public System.Action<PointerEventData> actionBlockClicked;

    private Transform m_OriginParent;

    // Use this for initialization
    void Start()
    {
        GetComponent<Image>().enabled = true;
        EnableBlock(false);

        m_OriginParent = transform.parent;
    }

    // Update is called once per frame
    void Update()
    {

    }

    /// <summary>
    /// Enable or disable the block
    /// </summary>
    /// <param name="enable"></param>
    public void EnableBlock(bool enable)
    {
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();

        canvasGroup.blocksRaycasts = enable;
        canvasGroup.alpha = enable ? 1.0f : 0.0f;
    }

    public void RestoreParent()
    {
        transform.SetParent(m_OriginParent);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        actionBlockClicked.RaiseEvent(eventData);
    }
}
