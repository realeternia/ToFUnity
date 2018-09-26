using System.Collections;
using System.Collections.Generic;
using ConfigDatas;
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
        transform.Find("Blood").gameObject.SetActive(true);
        
        MonsterConfig monsterConfig = ConfigData.GetMonsterConfig(cell.MonsterId);
        //Debug.Log("aaaa" + Id + "aaa" + cell.MonsterId + monsterConfig.Name);
        StartCoroutine(PicLoader.Instance.Load(cardImg, string.Format("Image/Monsters/{0}.jpg", monsterConfig.Url)));

        // iTween.ShakePosition(gameObject, new Vector3(0, 0.1f, 0), 1);
        iTween.RotateBy(gameObject, new Vector3(0, 1f, 0), 1);
        StartCoroutine(LateColor());
    }

    IEnumerator LateColor()
    {
        yield return new WaitForSeconds(.6f);
        var cell = MatchManager.Instance.GetCell(Id);
        if (cell.Side == 1)
            render.color = Color.red;
        else
            render.color = Color.blue;
    }
}
