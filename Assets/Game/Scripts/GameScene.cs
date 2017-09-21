using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    public SceneConfig m_SceneConfig;
    
    // Use this for initialization
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void Init()
    {
        InventoryItem[] items = GetComponentsInChildren<InventoryItem>(true);
        foreach (var item in items)
        {
            item.actionItemClicked += OnInventoryItemClicked;
        }
    }

    private void OnInventoryItemClicked(InventoryItem item)
    {
        switch (item.location)
        {
            case InventoryItem.Location.FindWindow:
                MainController.InventorySystem.AddInventoryItem(item);
                break;

            case InventoryItem.Location.InventorySystem:
                UseWindow useWindow = GetShowingUseWindow();
                if (useWindow != null)
                {
                    MainController.InventorySystem.RemoveInventoryItem(item);
                    useWindow.PlaceInventoryItem(item);
                }
                break;

            case InventoryItem.Location.UseWindow:
                break;
        }
    }

    private UseWindow GetShowingUseWindow()
    {
        UseWindow[] useWindows = GetComponentsInChildren<UseWindow>(true);
        foreach (var window in useWindows)
        {
            if (window.IsShowing)
            {
                return window;
            }
        }

        return null;
    }

    public void ShowSceneObject(bool show)
    {


        if(show)
        {
            MainController.InventorySystem.actionHintClicked += OnInventoryHintClicked;
        }
        else
        {
            // Destory hit area in this scene first
            HintArea hintArea = GetComponentInChildren<HintArea>();
            if(hintArea != null)
            {
                Destroy(hintArea.gameObject);
            }

            MainController.InventorySystem.actionHintClicked -= OnInventoryHintClicked;
        }

        gameObject.SetActive(show);
    }

    private void OnInventoryHintClicked()
    {
        print("OnInventoryHintClicked");

        HotspotFind[] hotspotFinds = GetComponentsInChildren<HotspotFind>();
        if(hotspotFinds.Length > 0)
        {
            int index = Random.Range(0, hotspotFinds.Length);
            
            Vector3 pos = transform.InverseTransformPoint(hotspotFinds[index].transform.position);

            MainController.InventorySystem.ShowHintAreaInScene(transform, pos);
        }
    }
}
