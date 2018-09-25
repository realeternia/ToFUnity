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
        PanelContainer.Instance.Add(bagPanelType);
    }
}
