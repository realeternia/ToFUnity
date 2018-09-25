using System.Collections;
using System.Collections.Generic;
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
    }

    private List<MatchCellInfo> itemList = new List<MatchCellInfo>();

    public void Init()
    {
        for (int i = 0; i < 17; i++)
            itemList.Add(new MatchCellInfo { Id = i+100, Side = 1, IsHide = true, MonsterId = 41000001});
        for (int i = 0; i < 18; i++)
            itemList.Add(new MatchCellInfo { Id=i+200, Side = 2, IsHide = true, MonsterId = 41000001 });
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
}
