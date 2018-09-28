using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSpriteLoader : MonoBehaviour
{
    public string Type;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Load(string name1)
    {
        var render = transform.GetComponent<SpriteRenderer>();
        StartCoroutine(PicLoader.Instance.Load(render, string.Format("Image/{0}/{1}", Type, name1)));
    }
}
