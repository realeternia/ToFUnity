using System.Collections.Generic;
using ConfigDatas;
using NarlonLib.Math;

namespace Assets.Scripts.DGToF.Control
{
    public class ConfigDataManager
    {
        private static Dictionary<int, List<int>> randomMonsterGroupDict;

        public static int GetRandMonsterId(int groupId)
        {
            if (randomMonsterGroupDict == null)
            {
                randomMonsterGroupDict = new Dictionary<int, List<int>>();
                for (int i = 0; i < 7; i++)
                    randomMonsterGroupDict[i] = new List<int>();
                foreach (MonsterConfig monsterConfig in ConfigData.MonsterDict.Values)
                {
                    randomMonsterGroupDict[monsterConfig.Group].Add(monsterConfig.Id);
                }
            }
            var targetList = randomMonsterGroupDict[groupId];
            return targetList[MathTool.GetRandom(targetList.Count)];
        }
    }
}