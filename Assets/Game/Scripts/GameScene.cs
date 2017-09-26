using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameScene : MonoBehaviour
{
    [SerializeField]
    private SceneConfig m_SceneConfig;
    
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

    }
    
    public void ShowSceneObject(bool show)
    {
        if(show)
        {
            MainController.InventorySystem.actionHintClicked += OnInventoryHintClicked;
        }
        else
        {
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
        HotspotFind[] hotspotFinds = GetComponentsInChildren<HotspotFind>();
        if(hotspotFinds.Length > 0)
        {
            foreach(var hotspotFind in hotspotFinds)
            {
                if(hotspotFind.findWindow.GetComponentsInChildren<InventoryItem>().Length > 0)
                {
                    MainController.InventorySystem.ShowHintAreaInScene(transform, hotspotFind.transform.position);
                    break;
                }
            }
        }
    }
}
