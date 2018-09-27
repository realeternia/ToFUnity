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

    private BattlePanel panel;
    // Use this for initialization
    void Start () {
        render = GetComponent<SpriteRenderer>();
        cardImg = transform.GetChild(0).GetComponent<SpriteRenderer>();
        hpText = transform.Find("Blood").GetChild(0).GetComponent<TextMesh>();
        strText = transform.Find("Str").GetChild(0).GetComponent<TextMesh>();
        panel = transform.parent.GetComponent<BattlePanel>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnMouseUp()
    {
        var target = panel.GetShine();
        if (target != gameObject)
        {
            if (target == null)
            {
                panel.SetShine(Id); //先给选中框    
                return;
            }

            var targetCell = target.GetComponent<BattleCell>();
            var nowCellInfo = MatchManager.Instance.GetCell(Id);
            //    Debug.Log(string.Format("state id={0} side={1} hide={2}", nowCellInfo.Id, nowCellInfo.Side, nowCellInfo.IsHide));
            if (nowCellInfo.IsHide)
            {
                panel.SetShine(Id); //先给选中框    
            }
            else
            {
                if (nowCellInfo.Side == 1)
                    panel.SetShine(Id); //切换选中框    
                else
                    panel.Fight(targetCell, this);
            }
        }
        else
        {
            var cell = MatchManager.Instance.GetCell(Id);
            if (cell.IsHide) //翻开
            {
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
        }
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
