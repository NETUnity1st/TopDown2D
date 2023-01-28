using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public int CurQuestId;
    Dictionary<int, QuestData> QuestList;
    public int QuestActionIndex;
    public GameObject[] QuestObject;
    // Start is called before the first frame update
    void Awake()
    {
        QuestList = new Dictionary<int, QuestData>();
        GenerateData();
    }

    // Update is called once per frame
    void GenerateData()
    {
        QuestList.Add(10, new QuestData("마을사람들과대화하기", new int[] { 1000, 2000 }));
        QuestList.Add(20, new QuestData("루도의동전찾아주기", new int[] { 5000, 2000 }));
        QuestList.Add(0, new QuestData("퀘스트 전부 클리어!", new int[] { 0 }));

    }

    public int GetQuestTalkIndex(int npcid)
    {
        return CurQuestId + QuestActionIndex;
    }

    public string CheckQuest(int npcid)
    {

        if (npcid == QuestList[CurQuestId].NpcId[QuestActionIndex])
        {
            QuestActionIndex++;
            ControlObject();
        }

        if (QuestActionIndex == QuestList[CurQuestId].NpcId.Length)
        {
            NextQuest();
            ControlObject();
        }
        return QuestList[CurQuestId].QuestName;
    }
    public string CheckQuest()
    {
        return QuestList[CurQuestId].QuestName;
    }

    void NextQuest()
    {
        CurQuestId += 10;
        if (CurQuestId == QuestList.Count * 10)
            CurQuestId = 0;
        QuestActionIndex = 0;
    }

    public void ControlObject()
    {
        switch (CurQuestId)
        {
            case 10:
                // if(QuestActionIndex == 2)
                //     QuestObject[0].SetActive(true);
                break;
            case 20:
                if (QuestActionIndex == 0)
                    QuestObject[0].SetActive(true);
                else if (QuestActionIndex == 1)
                    QuestObject[0].SetActive(false);
                break;
            default:
                break;
        }
    }
}
