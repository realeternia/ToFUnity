using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.DGToF.Control;
using ConfigDatas;
using NarlonLib.Tools;
using UnityEngine;

public class MatchManager {
    private static MatchManager instance;
    public static MatchManager Instance { get { return instance ?? (instance = new MatchManager()); } }

    public class MatchCellInfo
    {
        public int Id;
        public int Pos;
        public byte Side; //1 & 2
        public int MonsterId;

        public bool IsHide;

        private int str;
        public int Str
        {
            get { return str; }
            set { str = value; MessageCenter.Instance.Pubscribe("MatchCellInfo.Str", Id, str.ToString()); }
        }

        private int hpLeft;
        public int HpLeft
        {
            get { return hpLeft; }
            set { hpLeft = value; MessageCenter.Instance.Pubscribe("MatchCellInfo.HpLeft", Id, hpLeft.ToString()); }
        }
    }

    private List<MatchCellInfo> itemList = new List<MatchCellInfo>();

    public bool PlayerTurn = true;

    public void Init()
    {
        foreach (var tacticConfig in ConfigData.BattleTacticDict.Values)
        {
            var randMonId = ConfigDataManager.GetRandMonsterId(tacticConfig.Group);
            itemList.Add(new MatchCellInfo { Id = tacticConfig.CellId, Side = (byte)tacticConfig.Side, IsHide = true, MonsterId = randMonId });
        }
        ArraysUtils.RandomShuffle(itemList);

        for (int i = 0; i < 35; i++)
            itemList[i].Pos = i;
    }

    public MatchCellInfo GetCellPos(int pos)
    {
        return itemList.Find(p => p.Pos == pos);
    }
    public MatchCellInfo GetCell(int id)
    {
        return itemList.Find(p => p.Id == id);
    }

    public void ExchangePos(int cellAId, int cellBId)
    {
        var cellA = GetCell(cellAId);
        var cellB = GetCell(cellBId);
        var posA = cellA.Pos;
        cellA.Pos = cellB.Pos;
        cellB.Pos = posA;
    }

    public List<MatchCellInfo> GetAll()
    {
        return itemList;
    }

    public void NextTurn()
    {
        PlayerTurn = !PlayerTurn;
    }
}
