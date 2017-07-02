using UnityEngine;
using System.Collections.Generic;

public class BubbleManager : MonoBehaviour
{

    //Bacis property
    public Transform BubblePrefab;
    public Transform RedBubblePrefab;
    static List<Bubble> bubbles = new List<Bubble>();
    RedBubble CurrentRedBubble;

    float bubbleZoneLeft = -1.5f;
    float bubbleZoneRight = 1.5f;
    float bubbleZoneLower = 1f;
    float bubbleZoneUpper = 3.5f;

    //property for numbers input
    public float[] bubblePopInterval;  //超限必然会出BUG，需要处理
    public int bubbleInitNum;
    public int bubbleNumUpperLimit;

    public float RedBubbleRate;
    public float RedBubbleDuration;


    //property as middle number
    float lastBubblePopTime = 0;
    float lastRedBubblePopTime = 0;
    public bool RedBubbleActive;

    // Use this for initialization
    void Start()
    {
        BubbleMultiPop();
    }


    void Update()
    {

        //BubbleAutoPopFunction, Core
        if ((Time.time - lastBubblePopTime) > bubblePopInterval[bubbles.Count] && bubbles.Count <= bubbleNumUpperLimit)
        {
            BubblePop();
            lastBubblePopTime = Time.time;
        }



        //RedBubblePopUpLogic
        if (RedBubbleActive && CurrentRedBubble == null && UnityEngine.Random.Range(0f, 1f) < RedBubbleRate)
        {
            RedBubblePop();
            lastRedBubblePopTime = Time.time;
        }


        //AutoRedBubbleDestroy
        if ((Time.time - lastRedBubblePopTime) > RedBubbleDuration && CurrentRedBubble != null)
        {
            Debug.Log("AutoKill");
            KillRedBubble(CurrentRedBubble);
        }
    }


    public void BubblePop()
    {
        Vector3 bubblePosInit = new Vector3(UnityEngine.Random.Range(bubbleZoneLeft, bubbleZoneRight), UnityEngine.Random.Range(bubbleZoneLower, bubbleZoneUpper), 0);
        Transform b = (Transform)Instantiate(BubblePrefab, bubblePosInit, Quaternion.identity);
        bubbles.Add(b.GetComponent<Bubble>());
    }

    public void RedBubblePop()
    {
        Vector3 bubblePosInit = new Vector3(UnityEngine.Random.Range(bubbleZoneLeft, bubbleZoneRight), UnityEngine.Random.Range(bubbleZoneLower, bubbleZoneUpper), 0);
        Transform b = (Transform)Instantiate(RedBubblePrefab, bubblePosInit, Quaternion.identity);
        CurrentRedBubble = b.GetComponent<RedBubble>();
    }

    public void KillBubble(Bubble bubble)     //这里用的是教学视频的Singleton模式
    {
        Destroy(bubble.gameObject);
        bubbles.Remove(bubble);
    }

    public void KillRedBubble(RedBubble bubble)
    {
        Destroy(bubble.gameObject);
        CurrentRedBubble = null;
    }

    public void BubbleMultiPop()
    {
        for (int i = 0; i < bubbleInitNum; i++)
        {
            BubblePop();
        }
    }

    public void BubbleMultiKill()
    {
        for (int i = 0; i < bubbles.Count; i++)
        {
            KillBubble(bubbles[i]);
        }
    }

}
