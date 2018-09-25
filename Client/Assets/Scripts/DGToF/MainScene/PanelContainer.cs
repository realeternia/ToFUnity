using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelContainer : MonoBehaviour {

    public static PanelContainer Instance { get; private set; }

    // Use this for initialization
    void Start ()
    {
        Instance = this;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Add(GameObject panelType)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).name.Contains(panelType.name))
                return;
        }

        Instantiate(panelType, transform);
    }

    public void Remove(Transform trans)
    {
        for (int i = trans.childCount - 1; i >= 0; i--)
        {
            Destroy(trans.GetChild(i).gameObject);
        }
        trans.parent = null;
        Destroy(trans.gameObject);
    }
}
