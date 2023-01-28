using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public Animator PortraitAnim;
    public QuestManager QuestManager;
    public TalkManager TalkManager;
    public Animator talkPanel;
    public Image PortraitImg;
    public Sprite prevPortraitspr;
    public TypeEffect talk;
    public Text nameText;
    public GameObject ScanObject;
    public bool IsAction;
    public int TalkIndex;
    public GameObject MenuSet;
    public Text QuestText;
    public GameObject Player;
    void Start()
    {
        GameLoad();
        talk.SetMsg("");
        QuestText.text = QuestManager.CheckQuest();
    }
    void Awake()
    {
        IsAction = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(MenuSet.activeSelf){
            Time.timeScale = 0;    
        }else{
            Time.timeScale = 1;
        }
        if (Input.GetButtonDown("Cancel"))
        {
            if (MenuSet.activeSelf)
                MenuSet.SetActive(false);
            else
                MenuSet.SetActive(true);
        }
        
    }

    public void Action(GameObject obj)
    {
        ScanObject = obj;
        ObjData objdata = ScanObject.GetComponent<ObjData>();
        if (objdata == null)
        {
            IsAction = false;
            return;
        }
        else
            nameText.text = ScanObject.name;
        Talk(objdata.Id, objdata.IsNpc);


        talkPanel.SetBool("IsShow", IsAction);
    }
    void Talk(int id, bool isnpc)
    {

        if (talk.isAnimation)
        {
            talk.SetMsg("");
            return;
        }

        int QuestTalkIndex = QuestManager.GetQuestTalkIndex(id);
        string talkdata = TalkManager.GetTalk(id + QuestTalkIndex, TalkIndex);


        if (talkdata == null)
        {
            QuestText.text = QuestManager.CheckQuest(id);
            IsAction = false;
            TalkIndex = 0;
            return;
        }
        if (isnpc)
        {
            PortraitImg.color = new Color(1, 1, 1, 1);
            talk.SetMsg(talkdata.Split(':')[0]);
            PortraitImg.sprite = TalkManager.GetPortrait(id, int.Parse(talkdata.Split(':')[1]));
            if (prevPortraitspr != PortraitImg.sprite)
            {
                PortraitAnim.SetTrigger("DoEffect");
                prevPortraitspr = PortraitImg.sprite;
            }
        }
        else
        {
            PortraitImg.color = new Color(1, 1, 1, 0);
            talk.SetMsg(talkdata);
        }
        IsAction = true;
        TalkIndex++;
    }
    public void GameExit()
    {
        Application.Quit();
    }
    public void GameSave()
    {
        PlayerPrefs.SetFloat("PlayerX", Player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY",Player.transform.position.y);
        PlayerPrefs.SetInt("QuestId", QuestManager.CurQuestId);
        PlayerPrefs.SetInt("QuestActionIndex",QuestManager.QuestActionIndex);
        PlayerPrefs.Save();

        MenuSet.SetActive(false);
    }
    
    public void GameLoad()
    {
        if(!PlayerPrefs.HasKey("PlayerX"))
            return;

        float x =PlayerPrefs.GetFloat("PlayerX");
        float y= PlayerPrefs.GetFloat("PlayerY");
        int QID = PlayerPrefs.GetInt("QuestId");
        int QCI = PlayerPrefs.GetInt("QuestActionIndex");
        
        Player.transform.position = new Vector3(x,y,0);
        QuestManager.CurQuestId = QID;
        QuestManager.QuestActionIndex = QCI;
        QuestManager.ControlObject();
    }
}
