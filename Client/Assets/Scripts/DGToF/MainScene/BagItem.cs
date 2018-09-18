using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.GetComponent<Button>().onClick.AddListener(OnClick);
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnClick()
    {
        UpdateImage("anshuijing");
        UpdateText("大还丹");
    }

    public Image GetImage()
    {
        return this.transform.Find("CellImg").GetComponentInChildren<Image>();
    }
    public void UpdateText(string txt)
    {
        var textC = this.transform.GetComponentInChildren<Text>();
        textC.text = txt;
    }
    public void UpdateImage(string txt)
    {
        StartCoroutine(PicLoader.Instance.Load(GetImage(), string.Format("Image/Item/{0}.jpg", txt)));
    }
}
