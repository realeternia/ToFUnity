using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.DGToF.Control;
using Assets.Scripts.DGToF.Types;
using ConfigDatas;
using UnityEngine;

public class BattleCell : MonoBehaviour {

    private SpriteRenderer render;
    private SpriteRenderer cardImg;
    private SpriteRenderer raceImg;
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
        raceImg = transform.Find("Race").GetComponent<SpriteRenderer>();

        MessageCenter.Instance.Subscribe("MatchCellInfo.HpLeft", Id, hpText.gameObject, hp => hpText.text = hp.ToString());
        MessageCenter.Instance.Subscribe("MatchCellInfo.Str", Id, strText.gameObject, str => strText.text = str.ToString());
    }
	
	// Update is called once per frame
	void Update () {

    }

    void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(0))
        {
            panel.SetShine(Id); //先给选中框  
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (MatchManager.Instance.PlayerTurn)
            {
                var target = panel.GetShine();
                if (target == null) //无法空操作
                    return;

                if (target != gameObject)
                {
                    var targetCell = target.GetComponent<BattleCell>();
                    var nowCellInfo = MatchManager.Instance.GetCell(Id);
                    //   Debug.Log(string.Format("state id={0} side={1} hide={2}", nowCellInfo.Id, nowCellInfo.Side, nowCellInfo.IsHide));
                    if (!nowCellInfo.IsHide && nowCellInfo.Side == 2)
                    {
                        panel.Fight(targetCell, this);
                        MatchManager.Instance.NextTurn();
                    }
                    else //交换位置
                    {
                        panel.ExchangePos(this, targetCell);
                        MatchManager.Instance.NextTurn();
                    }
                }
                else
                {
                    Open();
                    MatchManager.Instance.NextTurn();
                }
            }
        }
    }

    public void Open()
    {
        var cell = MatchManager.Instance.GetCell(Id);
        if (cell.IsHide) //翻开
        {
            cell.IsHide = false;
            transform.Find("Blood").gameObject.SetActive(true);
            transform.Find("Str").gameObject.SetActive(true);

            MonsterId = cell.MonsterId;
            MonsterConfig monsterConfig = ConfigData.GetMonsterConfig(MonsterId);
            cell.Str = monsterConfig.Atk;
            cell.HpLeft = monsterConfig.Hp;
            //Debug.Log("aaaa" + Id + "aaa" + cell.MonsterId + monsterConfig.Name);

            transform.GetChild(0).GetComponent<AutoSpriteLoader>().Load(monsterConfig.Url + ".jpg");
            transform.Find("Race").GetComponent<AutoSpriteLoader>().Load(string.Format("chessg{0}.png", monsterConfig.Group));

            iTween.RotateBy(gameObject, new Vector3(0, 1f, 0), 1);
            StartCoroutine(LateColor());

            if (monsterConfig.Group == (int) MonsterGroupTypes.King)
                panel.ShakeAll(Id);

        }
    }

    IEnumerator LateColor()
    {
        yield return new WaitForSeconds(.2f);
        var cell = MatchManager.Instance.GetCell(Id);
        if (cell.Side == 1)
            render.color = Color.green;
        else
            render.color = Color.red;
    }

    public void MoveTo(int posId)
    {
        MoveTo(new Vector3(-1.53f + posId % 5 * 0.73f, 2.65f - posId / 5 * 0.73f, 0));
    }

    private void MoveTo(Vector3 targetPos)
    {
        Hashtable args = new Hashtable();

        //这里是设置类型，iTween的类型又很多种，在源码中的枚举EaseType中
        //例如移动的特效，先震动在移动、先后退在移动、先加速在变速、等等
        args.Add("easeType", iTween.EaseType.linear);

        //移动的速度，
        //  args.Add("speed", 0.3f);
        //移动的整体时间。如果与speed共存那么优先speed
        args.Add("time", 0.4f);
        //延迟执行时间
       // args.Add("delay", 0.1f);

        //当然也可以写Vector3
         args.Add("position", targetPos);

        iTween.MoveTo(gameObject, args);
    }

    public bool LossHp(int val)
    {
        var chessObj = MatchManager.Instance.GetCell(Id);
        chessObj.HpLeft -= val;

        EffectManager.Instance.AddEffect(EffectManager.Instance.EffBlood, new Vector3(transform.position.x, transform.position.y, 0));
        EffectManager.Instance.AddTextFly(new Vector3(transform.position.x, transform.position.y, -0.3f), string.Format("-{0}", val));
        if (chessObj.HpLeft <= 0)
        {
            OnDie(chessObj);
            return true;
        }

        return false;
    }

    private void OnDie(MatchManager.MatchCellInfo chessObj)
    {
        chessObj.Side = 0;
        render.color = Color.gray; //隐藏起来
        cardImg.sprite = null;
        transform.Find("Blood").gameObject.SetActive(false);
        transform.Find("Str").gameObject.SetActive(false);
        raceImg.gameObject.SetActive(false);

        MessageCenter.Instance.Unsubscribe("MatchCellInfo.HpLeft", Id, hpText.gameObject);
        MessageCenter.Instance.Unsubscribe("MatchCellInfo.Str", Id, strText.gameObject);
    }

    public void Shake()
    {
        iTween.ShakePosition(gameObject, new Vector3(0, 0.1f, 0), 1);
      //  EffectManager.Instance.AddEffect(EffectManager.Instance.EffFlameFall, new Vector3(transform.position.x, transform.position.y, 0));
        //obj.transform.SetParent(panel.transform.parent.GetChild(2));
    }
}
