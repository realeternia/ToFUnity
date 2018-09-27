﻿using System.Collections;
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
                var appleGO = Instantiate(cellType, transform);
                   appleGO.transform.localPosition = new Vector3(-1.46f + i * 0.7f, 2.45f - j * 0.7f, 0);
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

    public void Fight(BattleCell attacker, BattleCell defender)
    {
        var midPoint = attacker.transform.position*1/4 + defender.transform.position*3/4;
        Hashtable args = new Hashtable();

        //这里是设置类型，iTween的类型又很多种，在源码中的枚举EaseType中
        //例如移动的特效，先震动在移动、先后退在移动、先加速在变速、等等
        args.Add("easeType", iTween.EaseType.easeInOutExpo);

        //移动的速度，
      //  args.Add("speed", 0.3f);
        //移动的整体时间。如果与speed共存那么优先speed
        args.Add("time", 0.7f);
        //延迟执行时间
        args.Add("delay", 0.1f);
        //移动的过程中面朝一个点
     //   args.Add("looktarget", Vector3.zero);

        //三个循环类型 none loop pingPong (一般 循环 来回)	
        //args.Add("loopType", "none");
        //args.Add("loopType", "loop");	
      //  args.Add("loopType", "pingPong");

        //当然也可以写Vector3
       // args.Add("position", midPoint);

        //最终让改对象开始移动
        Vector3[] paths = new Vector3[] {midPoint, attacker.transform.position};
        args.Add("path", paths);
        iTween.MoveTo(attacker.gameObject, args);

        paths = new Vector3[] { midPoint, defender.transform.position };
        args["path"] = paths;
        iTween.MoveTo(defender.gameObject, args);

        attacker.LossHp(1);
        defender.LossHp(1);
    }
}
