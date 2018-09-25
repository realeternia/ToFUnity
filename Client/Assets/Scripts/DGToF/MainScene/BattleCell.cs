using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleCell : MonoBehaviour {
    private SpriteRenderer render;
    // Use this for initialization
    void Start () {
        render = GetComponent<SpriteRenderer>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseUp()
    {
        render.color = Color.red;
        Debug.Log("btn clicked!!!");
    }
}
