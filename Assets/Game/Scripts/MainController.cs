using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainController : Singleton<MainController>
{
    [SerializeField]
    private InventorySystem inventorySystem;

    [SerializeField]
    private GameObject sceneParent;

    [SerializeField]
    private GameObject menuPanel;

    [SerializeField]
    private GameSceneData[] sceneDatas;

    [SerializeField]
    private ItemData[] itemDatas;

    private GameSceneData m_CurrentSceneData;

    private bool m_HasPopupWindow = false;

    void Start()
    {
        GameButton[] buttons = menuPanel.GetComponentsInChildren<GameButton>();
        foreach (var button in buttons)
        {
            EventTriggerListener.Get(button.gameObject).onClick += OnButtonClicked;
        }

        EnterScene(GameSceneId.Scene1);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnButtonClicked(GameObject go)
    {
        GameButton button = go.GetComponent<GameButton>();
        switch (button.buttonId)
        {
            case GameButtonId.GotoScene1:
                EnterScene(GameSceneId.Scene1);
                break;

            case GameButtonId.GotoScene2:
                EnterScene(GameSceneId.Scene2);
                break;

            case GameButtonId.GotoScene3:
                EnterScene(GameSceneId.Scene3);
                break;
        }
    }

    public void EnterScene(GameSceneId sceneId)
    {
        if (m_CurrentSceneData != null)
        {
            if (m_CurrentSceneData.sceneId == sceneId)
            {
                return;
            }

            if (m_CurrentSceneData.sceneObject != null)
            {
                m_CurrentSceneData.sceneObject.ShowSceneObject(false);
            }
        }

        GameSceneData sceneData = FindSceneData(sceneId);
        if (sceneData == null)
        {
            Debug.LogErrorFormat("Didn't Find Scene:{0}", sceneId);
            return;
        }

        if (sceneData.sceneObject == null)
        {
            sceneData.sceneObject = GlobalTools.AddChild<GameScene>(sceneParent, sceneData.scenePrefab);
        }
        sceneData.sceneObject.ShowSceneObject(true);
        m_CurrentSceneData = sceneData;
    }

    private GameSceneData FindSceneData(GameSceneId sceneId)
    {
        GameSceneData sceneData = null;
        foreach (var data in sceneDatas)
        {
            if (data.sceneId == sceneId)
            {
                sceneData = data;
                break;
            }
        }

        return sceneData;
    }

    public ItemData GetItemData(ItemType itemType)
    {
        foreach (var data in itemDatas)
        {
            if (data.itemType == itemType)
            {
                return data;
            }
        }

        return null;
    }
    
    public static InventorySystem InventorySystem
    {
        get
        {
            return Instance.inventorySystem;
        }
    }

    public bool IsPopupWindowShowing
    {
        get
        {
            return m_HasPopupWindow;
        }
        set
        {
            m_HasPopupWindow = value;

            menuPanel.GetComponent<CanvasGroup>().blocksRaycasts = !m_HasPopupWindow;
        }
    }
}
