using System.Collections;
using System.Collections.Generic;
using NarlonLib.Math;
using NarlonLib.Tools;
using UnityEngine;

public class AIController : MonoBehaviour {
    public BattlePanel Panel;

    private TimeCounter tc = new TimeCounter(0.4f);

    class AIAttackCheckData
    {
        public MatchManager.MatchCellInfo Attacker;
        public MatchManager.MatchCellInfo Defender;
        public int Score;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	    if (tc.OnTick())
	    {
	        AiThink();
	    }
	}

    private void AiThink()
    {
        if (MatchManager.Instance.PlayerTurn)
        {
            return;
        }

        var attackPair = GetAttackPair();
        if (attackPair != null)
        {
            Panel.Fight(attackPair.Attacker.Id, attackPair.Defender.Id);
            MatchManager.Instance.NextTurn();
            return;
        }

        var hiddenCells = MatchManager.Instance.GetAll().FindAll(cell => cell.IsHide && cell.Side > 0);
        if (hiddenCells.Count > 10)//todo temp code
        {
            var openTarget = hiddenCells[MathTool.GetRandom(hiddenCells.Count)];
            Panel.Open(openTarget.Id);
            MatchManager.Instance.NextTurn();
            return;
        }
    }

    private static AIAttackCheckData GetAttackPair()
    {
        var myCells = MatchManager.Instance.GetAll().FindAll(cell => cell.Side == 2 && !cell.IsHide); //找到自己的棋子
        List<AIAttackCheckData> scoreList = new List<AIAttackCheckData>();
        foreach (var pickCell in myCells)
        {
            var targets = MatchManager.Instance.GetEnemys(pickCell.Id);
            foreach (var defender in targets)
            {
                var checkData = new AIAttackCheckData
                    {Attacker = pickCell, Defender = defender, Score = Mathf.Min(pickCell.Str, defender.HpLeft)};
                if (defender.HpLeft < pickCell.Str)
                    checkData.Score += 10; //击杀+10

                scoreList.Add(checkData);
            }
        }

        if (scoreList.Count > 1)
        {
            scoreList.Sort((a, b) => -a.Score + b.Score); //降序
        }

        if (scoreList.Count <= 0)
            return null;
        return scoreList[0];
    }
}
