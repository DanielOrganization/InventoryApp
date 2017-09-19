using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HotspotFind : MonoBehaviour
{
    public FindWindow findWindow;
        
    // Use this for initialization
    void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(OnItemClicked);

        findWindow.ShowWindow(false, false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnItemClicked()
    {
        findWindow.ShowWindow(true, true);
    }
}
