using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattlePanel : MonoBehaviour {

    public GameObject cellType;
    public GameObject glowObj;

    public GameObject glowObjIns;

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

    public void SetShine(int id)
    {
        var selectCell = FindTarget(id);
        if (selectCell == null)
        {
            return;
        }

        if (glowObjIns == null)
        {
            glowObjIns = Instantiate(glowObj, selectCell.transform);
            glowObjIns.transform.localPosition = new Vector3(0, 0, -.1f);
        }
        else
        {
            glowObjIns.transform.SetParent(selectCell.transform);
            glowObjIns.transform.localPosition = new Vector3(0, 0, -.1f);
        }
    }

    public GameObject GetShine()
    {
        if (glowObjIns == null)
            return null;

        return glowObjIns.transform.parent.gameObject;
    }

    private GameObject FindTarget(int id)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            if (child.GetComponent<BattleCell>().Id == id)
                return child.gameObject;
        }
        return null;
    }
}
