using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePanel : MonoBehaviour {

    public GameObject cellType;

    // Use this for initialization
    void Start () {

        MatchManager.Instance.Init();
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                var appleGO = Instantiate(cellType, new Vector3(-1.46f + i*0.7f, 2.45f - j*0.7f, 0), transform.localRotation);
                   appleGO.transform.SetParent(transform);
                appleGO.GetComponent<BattleCell>().Id = MatchManager.Instance.GetCellPos(i*7 + j).Id;
                //   appleGO.transform.localScale = new Vector3(-1.46f + i, 2.15f + j, 1.0f);
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
