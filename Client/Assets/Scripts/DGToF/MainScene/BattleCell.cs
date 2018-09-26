using System.Collections;
using System.Collections.Generic;
using ConfigDatas;
using UnityEngine;

public class BattleCell : MonoBehaviour {

    private SpriteRenderer render;
    private SpriteRenderer cardImg;
    private TextMesh hpText;
    private TextMesh strText;
    public int Id; //cellId
    public int MonsterId;
    // Use this for initialization
    void Start () {
        render = GetComponent<SpriteRenderer>();
        cardImg = transform.GetChild(0).GetComponent<SpriteRenderer>();
        hpText = transform.Find("Blood").GetChild(0).GetComponent<TextMesh>();
        strText = transform.Find("Str").GetChild(0).GetComponent<TextMesh>();
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
        transform.Find("Str").gameObject.SetActive(true);

        MonsterId = cell.MonsterId;
        MonsterConfig monsterConfig = ConfigData.GetMonsterConfig(MonsterId);
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

        MonsterConfig monsterConfig = ConfigData.GetMonsterConfig(MonsterId);
        UpdateStr(monsterConfig.Atk);
        UpdateHp(monsterConfig.Hp);
    }

    public void UpdateStr(int str)
    {
        strText.text = str.ToString();
    }
    public void UpdateHp(int hp)
    {
        hpText.text = hp.ToString();
    }
}
