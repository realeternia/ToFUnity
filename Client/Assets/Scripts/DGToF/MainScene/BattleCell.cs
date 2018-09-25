using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCell : MonoBehaviour {
    private SpriteRenderer render;
    private SpriteRenderer cardImg;
    public int Id;
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
        var cell = MatchManager.Instance.GetCell(Id);
        if (!cell.IsHide)
        {
            return;
        }

        cell.IsHide = false;
        if (cell.Side == 1)
            render.color = Color.red;
        else
            render.color = Color.blue;

        StartCoroutine(PicLoader.Instance.Load(cardImg, string.Format("Image/Monsters/1.jpg")));
    }
}
