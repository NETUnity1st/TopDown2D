using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestData
{
    public string QuestName;
    public int[] NpcId;

    public QuestData(string name, int[] npcids)
    {
        QuestName = name;
        NpcId = npcids;
    }
}
