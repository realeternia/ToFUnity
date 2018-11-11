using System.Collections;
using System.Collections.Generic;
using NarlonLib.Math;
using NarlonLib.Tools;
using UnityEngine;

public class AIController : MonoBehaviour {
    public BattlePanel Panel;

    private TimeCounter tc = new TimeCounter(0.4f);

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

        var hiddenCells = MatchManager.Instance.GetAll().FindAll(cell => cell.IsHide && cell.Side > 0);
        if (hiddenCells.Count > 10)//todo temp code
        {
            var openTarget = hiddenCells[MathTool.GetRandom(hiddenCells.Count)];
            Panel.Open(openTarget.Id);
            MatchManager.Instance.NextTurn();
            return;
        }
    }
}
