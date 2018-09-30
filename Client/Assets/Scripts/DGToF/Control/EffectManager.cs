﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectManager : MonoBehaviour 
{
    private static EffectManager instance;
    public static EffectManager Instance { get { return instance; } }

    public GameObject EffBlood;
    public GameObject EffElect;
    public GameObject EffFlameFall;

    public GameObject FlyText;

    // Use this for initialization
    void Start ()
	{
	    instance = this;
        Debug.Log("EffectManager.init");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddEffect(GameObject eff, Vector3 pos)
    {
        Instantiate(eff, pos, transform.rotation, transform);
    }

    public void AddTextFly(Vector3 pos, string txt)
    {
        var obj = Instantiate(FlyText, pos, transform.rotation, transform);
        obj.GetComponent<TextFlyHpLoss>().Fly(txt);
    }
}
