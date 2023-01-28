using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TalkManager : MonoBehaviour
{
    Dictionary<int, string[]> TalkData;
    Dictionary<string, int> ObjId;
    Dictionary<string, int> QstId;
    Dictionary<int, Sprite> PortraitData;
    public Sprite[] PortraitArr;
    // Start is called before the first frame update
    void Awake()
    {
        ObjId = new Dictionary<string, int>(){
            {"Desk",10000},{"Box",11000},{"Tree",12000},
            {"Rock",13000},{"NPC Luna",1000},{"NPC Ludo",2000},
            {"Plants",14000},{"Ludo'sCoin",5000}
        };
        QstId = new Dictionary<string, int>()
        {
            {"마을사람들과대화하기",10},{"루도의동전찾아주기",20}
        };
        TalkData = new Dictionary<int, string[]>();
        PortraitData = new Dictionary<int, Sprite>();
        GenerateData();
    }

    // Update is called once per frame
    void GenerateData()
    {
        TalkData.Add(ObjId["NPC Luna"], new string[] { "안녕?:0",
                                                    "이 곳에 처음 왔구나?:1",
                                                    "만나서 반가워!:2" });

        TalkData.Add(ObjId["NPC Ludo"], new string[] { "A와는 대화 해봤니?:0",
                                                    "여긴 내 집이야.:1" });

        TalkData.Add(ObjId["Box"], new string[] { "평범한 박스다.",
                                                    "안에 수상한 것이 들어있다." });

        TalkData.Add(ObjId["Desk"], new string[] { "책상이다.",
                                                    "읽을 수 없는 언어로 된 글자들이 늘여져있다." });

        TalkData.Add(ObjId["Rock"], new string[] { "크고 우람한 돌이다." });

        TalkData.Add(ObjId["Tree"], new string[] { "큰 나무다.",
                                                    "맺힌 열매는 없다." });

        TalkData.Add(ObjId["Plants"], new string[] { "평범한 길가에 있는 식물이다." });


        TalkData.Add(ObjId["NPC Luna"] + QstId["마을사람들과대화하기"], new string[]
         { "어서 와!:0" ,
            "이 마을에 놀라운 전설이 있다는데:1",
            "오른쪽 호수 쪽에 Ludo라는 애가 알려줄거야.:2"
         });
        TalkData.Add(ObjId["NPC Ludo"] + QstId["마을사람들과대화하기"] + 1, new string[]
         {  "여어.:0" ,
            "이 호수의 전설을 들으러 온거야?:1",
            "근데 지금 내가 동전을 잃어버렸어.:3",
            "혹시 찾아봐주지 않을래? 이야기는 그 후에 하자.:1",
            "정말 고마워!!:2"
         });
        TalkData.Add(ObjId["NPC Luna"] + QstId["루도의동전찾아주기"], new string[]
        { "루도의 동전?:1",
          "내가 그럴줄 알았어!:3",
          "나중에 루도에게 한 마디 해야겠어:3"
        });
        TalkData.Add(ObjId["NPC Ludo"] + QstId["루도의동전찾아주기"], new string[]
        { "찾으면 꼭 가져다 줘\n부탁할게:1"
        });
        TalkData.Add(ObjId["Ludo'sCoin"] + QstId["루도의동전찾아주기"], new string[]
        { "근처에서 동전을 찾았다."
        });
        TalkData.Add(ObjId["NPC Ludo"] + QstId["루도의동전찾아주기"] + 1, new string[]
        { "엇, 찾아줘서 고마워.:2"
        });

        PortraitData.Add(ObjId["NPC Luna"] + 0, PortraitArr[0]);

        PortraitData.Add(ObjId["NPC Luna"] + 1, PortraitArr[1]);

        PortraitData.Add(ObjId["NPC Luna"] + 2, PortraitArr[2]);

        PortraitData.Add(ObjId["NPC Luna"] + 3, PortraitArr[3]);

        PortraitData.Add(ObjId["NPC Ludo"] + 0, PortraitArr[4]);

        PortraitData.Add(ObjId["NPC Ludo"] + 1, PortraitArr[5]);

        PortraitData.Add(ObjId["NPC Ludo"] + 2, PortraitArr[6]);

        PortraitData.Add(ObjId["NPC Ludo"] + 3, PortraitArr[7]);
    }

    public string GetTalk(int id, int talkindex)
    {
        Debug.Log(id);
        if (!TalkData.ContainsKey(id))
        {
            if (TalkData.ContainsKey(id - id % 10))
                return GetTalk(id - id % 10, talkindex);
            else
                return GetTalk(id - id % 100, talkindex);
        }
        if (talkindex == TalkData[id].Length)
            return null;
        else
            return TalkData[id][talkindex];
    }

    public Sprite GetPortrait(int id, int portraitindex)
    {
        return PortraitData[id + portraitindex];
    }
}
