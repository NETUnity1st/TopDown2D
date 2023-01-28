using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TypeEffect : MonoBehaviour
{
    string TargetMsg;
    public GameObject EndCursor;
    public float CPS; //Character Per Seconds
    Text msgText;
    int index;
    float Temp;
    AudioSource audioSource;
    public bool isAnimation;
    void Awake()
    {
        msgText = GetComponent<Text>();
        audioSource = GetComponent<AudioSource>();
    }
    public void SetMsg(string msg)
    {
        if (isAnimation)
        {
            msgText.text = TargetMsg;
            CancelInvoke();
            EffectEnd();
        }
        else
        {
            TargetMsg = msg;
            EffectStart();
        }
    }
    void EffectStart()
    {
        EndCursor.SetActive(false);

        msgText.text = "";
        index = 0;
        Temp = 1.0f / CPS;
        Invoke("Effecting", Temp);
        isAnimation = true;
    }
    void Effecting()
    {
        if (msgText.text == TargetMsg)
        {
            EffectEnd();
            return;
        }
        msgText.text += TargetMsg[index];
        if (TargetMsg[index] != ' ' || TargetMsg[index] != '.' && index / 2 == 0)
            audioSource.Play();
        index++;
        Invoke("Effecting", 1 / CPS);
    }
    void EffectEnd()
    {
        EndCursor.SetActive(true);
        isAnimation = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
