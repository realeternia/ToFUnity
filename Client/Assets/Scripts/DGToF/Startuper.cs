using System.Collections;
using System.Collections.Generic;
using ConfigDatas;
using UnityEngine;

public class Startuper : MonoBehaviour {

    void Awake()
    {
        ConfigData.LoadData();
        Debug.Log("config loaded");
    }
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
