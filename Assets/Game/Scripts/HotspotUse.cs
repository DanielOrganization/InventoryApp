using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotspotUse : MonoBehaviour
{
    public UseWindow useWindow;
    
    // Use this for initialization
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnItemClicked);
        
        useWindow.ShowWindow(false, false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnItemClicked()
    {
        useWindow.ShowWindow(true, true);
    }
}
