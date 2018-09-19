using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolbarPanel : MonoBehaviour {
    public GameObject bagPanelType;
    private Button btn1;
    // Use this for initialization
    void Start () {
        btn1 = transform.Find("Button1").GetComponent<Button>();
        btn1.onClick.AddListener(OnBtn1Click);
    }
    
    // Update is called once per frame
	void Update () {

    }

    private void OnBtn1Click()
    {
        var panelContainer = transform.parent.Find("PanelContainer");
        var appleGO = Instantiate(bagPanelType, new Vector3(0, 0, 0), transform.localRotation);
        appleGO.transform.SetParent(panelContainer);
        appleGO.transform.localScale = Vector3.one;
    }
}
