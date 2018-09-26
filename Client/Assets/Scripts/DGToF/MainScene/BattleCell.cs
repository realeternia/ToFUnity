﻿using System.Collections;
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

        StartCoroutine(PicLoader.Instance.Load(cardImg, string.Format("Image/Monsters/1.jpg")));

        // iTween.ShakePosition(gameObject, new Vector3(0, 0.1f, 0), 1);
        iTween.RotateBy(gameObject, new Vector3(0, 1f, 0), 1);
        StartCoroutine(LateColor());
    }

    IEnumerator LateColor()
    {
        yield return new WaitForSeconds(.8f);
        var cell = MatchManager.Instance.GetCell(Id);
        if (cell.Side == 1)
            render.color = Color.red;
        else
            render.color = Color.blue;
    }
}
