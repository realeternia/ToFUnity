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
        public NarlonLib.Math.Vector2 Pos;
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
    private int[,] cellMap = new int[GameConst.ColumnCount, GameConst.RowCount]; //位置映射

    public bool PlayerTurn = true;

    public void Init()
    {
        foreach (var tacticConfig in ConfigData.BattleTacticDict.Values)
        {
            var randMonId = ConfigDataManager.GetRandMonsterId(tacticConfig.Group);
            itemList.Add(new MatchCellInfo { Id = tacticConfig.CellId, Side = (byte)tacticConfig.Side, IsHide = true, MonsterId = randMonId });
        }
        ArraysUtils.RandomShuffle(itemList);

        for (int i = 0; i < GameConst.RowCount; i++)
        {
            itemList[i].Pos = new NarlonLib.Math.Vector2(i % GameConst.ColumnCount, i / GameConst.ColumnCount);
            cellMap[itemList[i].Pos.X, itemList[i].Pos.Y] = itemList[i].Id;
        }
    }

    public MatchCellInfo GetCellPos(NarlonLib.Math.Vector2 pos)
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

        cellMap[cellA.Pos.X, cellA.Pos.Y] = cellA.Id;
        cellMap[cellB.Pos.X, cellB.Pos.Y] = cellB.Id;
    }

    public List<MatchCellInfo> GetAll()
    {
        return itemList;
    }

    public List<MatchCellInfo> GetEnemys(int cellId)
    {
        var myCell = GetCell(cellId);
        var besideCells = GetBesides(cellId);

        besideCells.RemoveAll(neibour => neibour.IsHide || neibour.Side == 0 || neibour.Side == myCell.Side);
        return besideCells;
    }

    public List<MatchCellInfo> GetBesides(int cellId)
    {
        var myCell = GetCell(cellId);
        List<MatchCellInfo> besideCells = new List<MatchCellInfo>();
        if (myCell.Pos.X > 0)
            besideCells.Add(GetCell(cellMap[myCell.Pos.X - 1, myCell.Pos.Y]));
        if (myCell.Pos.X < GameConst.ColumnCount - 1)
            besideCells.Add(GetCell(cellMap[myCell.Pos.X + 1, myCell.Pos.Y]));
        if (myCell.Pos.Y > 0)
            besideCells.Add(GetCell(cellMap[myCell.Pos.X, myCell.Pos.Y - 1]));
        if (myCell.Pos.Y < GameConst.RowCount - 1)
            besideCells.Add(GetCell(cellMap[myCell.Pos.X, myCell.Pos.Y + 1]));
        return besideCells;
    }

    public void NextTurn()
    {
        PlayerTurn = !PlayerTurn;
    }
}
