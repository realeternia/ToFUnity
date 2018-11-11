using System.Collections;
using System.Collections.Generic;
using ConfigDatas;
using UnityEngine;

public class BattlePanel : MonoBehaviour {

    public GameObject cellType;
    public GameObject glowObj;

    public GameObject glowObjIns;

    private AIController aiThink;

    // Use this for initialization
    void Start () {

        MatchManager.Instance.Init();
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 7; j++)
            {
                var appleGO = Instantiate(cellType, transform);
                var cell = appleGO.GetComponent<BattleCell>();
                cell.Id = MatchManager.Instance.GetCellPos(i * 7 + j).Id;
                cell.UpdatePos(i * 7 + j);
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
            glowObjIns.transform.localPosition = new Vector3(0, 0, -.3f);
        }
        else
        {
            glowObjIns.transform.SetParent(selectCell.transform);
            glowObjIns.transform.localPosition = new Vector3(0, 0, -.3f);
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

    public void Fight(int attackerId, int defenderId)
    {
        var cellA = FindTarget(attackerId);
        var cellB = FindTarget(defenderId);
        Fight(cellA.GetComponent<BattleCell>(), cellB.GetComponent<BattleCell>());
    }

    public void Fight(BattleCell attacker, BattleCell defender)
    {
        var midPoint = attacker.transform.position*1/4 + defender.transform.position*3/4;
        Hashtable args = new Hashtable();

        args.Add("easeType", iTween.EaseType.easeInOutExpo);

        //移动的整体时间。如果与speed共存那么优先speed
        args.Add("time", 0.7f);
        args.Add("delay", 0.1f);

        //最终让改对象开始移动
        Vector3[] paths = new Vector3[] {midPoint, attacker.transform.position};
        args.Add("path", paths);
        iTween.MoveTo(attacker.gameObject, args);

        paths = new Vector3[] { midPoint, defender.transform.position };
        args["path"] = paths;
        iTween.MoveTo(defender.gameObject, args);

        StartCoroutine(LossHp(attacker, defender));
    }

    private IEnumerator LossHp(BattleCell attacker, BattleCell defender)
    {
        yield return new WaitForSeconds(0.4f);

        var damage = ConfigData.GetMonsterConfig(attacker.MonsterId).Atk;
        if (defender.LossHp(damage))
            ExchangePos(attacker, defender); //死亡直接交换位置
    }

    public void ExchangePos(int cellAId, int cellBId)
    {
        var cellA = FindTarget(cellAId);
        var cellB = FindTarget(cellBId);
        ExchangePos(cellA.GetComponent<BattleCell>(), cellB.GetComponent<BattleCell>());
    }

    public void ExchangePos(BattleCell cellA, BattleCell cellB)
    {
        var myPos = MatchManager.Instance.GetCell(cellA.Id).Pos;
        var tarPos = MatchManager.Instance.GetCell(cellB.Id).Pos;
        cellA.MoveTo(tarPos);
        cellB.MoveTo(myPos);
    }

    public void Open(int cellId)
    {
        var cellA = FindTarget(cellId);
        if (cellA != null)
            cellA.GetComponent<BattleCell>().Open();
    }

    public void ShakeAll(int exceptId)
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            var battleCell = child.GetComponent<BattleCell>();
            if (battleCell.Id != exceptId)
                battleCell.Shake();
        }
    }
}
