using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCell : MonoBehaviour {
    private SpriteRenderer render;
    private SpriteRenderer cardImg;
    // Use this for initialization
    void Start () {
        render = GetComponent<SpriteRenderer>();
        cardImg = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseUp()
    {
        //render.color = Color.red;
        Debug.Log("btn clicked!!!");

        StartCoroutine(PicLoader.Instance.Load(cardImg, string.Format("Image/Monsters/1.jpg")));
    }
}
