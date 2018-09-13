using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnClick()
    {
       
    }

    public Image GetImage()
    {
        return this.transform.GetComponentInChildren<Image>();
    }
    public void UpdateText(string txt)
    {
        var textC = this.transform.GetComponentInChildren<Text>();
        textC.text = txt;
    }
}
