using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.DGToF.Control
{
    public delegate void UpdateText(string txt);

    public class MessageCenter
    {
        private class MessageSaver
        {
            public UpdateText TextMethod;
            public GameObject GameObject;
        }

        private static MessageCenter instance;
        public static MessageCenter Instance { get { return instance ?? (instance = new MessageCenter()); } }
        
        private Dictionary<string, List<MessageSaver>> listenerDict = new Dictionary<string, List<MessageSaver>>();

        public void Pubscribe(string evtType, int evtKey, string val)
        {
            var keyStr = string.Format("{0}.{1}", evtType, evtKey);
            List<MessageSaver> saveList;
            Debug.Log(string.Format("{0}=>{1}", keyStr,val));
            if (listenerDict.TryGetValue(keyStr, out saveList))
            {
                foreach (var messageSaver in saveList)
                    messageSaver.TextMethod(val);
            }
        }

        //订阅
        public void Subscribe(string evtType, int evtKey, GameObject go, UpdateText md)
        {
            var keyStr = string.Format("{0}.{1}", evtType, evtKey);
            List<MessageSaver> saveList;

            if (listenerDict.TryGetValue(keyStr, out saveList))
            {
                if (saveList.Find(tar => tar.GameObject == go) != null) //重复订阅
                    return;
            }
            else
            {
                listenerDict[keyStr] = new List<MessageSaver>();
            }
            listenerDict[keyStr].Add(new MessageSaver { GameObject = go, TextMethod = md });
        }
        //订阅
        public void Unsubscribe(string evtType, int evtKey, GameObject go)
        {
            var keyStr = string.Format("{0}.{1}", evtType, evtKey);
            List<MessageSaver> saveList;

            if (listenerDict.TryGetValue(keyStr, out saveList))
                saveList.RemoveAll(tar => tar.GameObject == go);
        }
    }
}