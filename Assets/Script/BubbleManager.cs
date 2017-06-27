using UnityEngine;
using System.Collections.Generic;

public class BubbleManager : MonoBehaviour {
    
    //Bacis property
    public Transform BubblePefab;    
    static List<Bubble> bubbles = new List<Bubble>();

    float bubbleZoneLeft = -1.5f;
    float bubbleZoneRight = 1.5f;
    float bubbleZoneLower = 1f;
    float bubbleZoneUpper = 3.5f;      

    //property for numbers input
    public float[] bubblePopInterval;  //超限必然会出BUG，需要处理
    public int bubbleInitNum;
    public int bubbleNumUpperLimit;

    //property as middle number
    float lastBubblePopTime = 0;


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

    }


    public  void BubblePop()
    {
        Vector3 bubblePosInit = new Vector3(UnityEngine.Random.Range(bubbleZoneLeft, bubbleZoneRight), UnityEngine.Random.Range(bubbleZoneLower, bubbleZoneUpper), 0);
        Transform b = (Transform)Instantiate(BubblePefab, bubblePosInit, Quaternion.identity);
        bubbles.Add(b.GetComponent<Bubble>());
    }

    public  void KillBubble(Bubble bubble)     //这里用的是教学视频的Singleton模式
    {
        Destroy(bubble.gameObject);
        bubbles.Remove(bubble);
    }

    public  void BubbleMultiPop()
    {
        for (int i = 0; i < bubbleInitNum; i++)
        {
            BubblePop();
        }
    }

    public  void BubbleMultiKill()
    {
        for (int i = 0; i < bubbles.Count; i++)
        {
            KillBubble(bubbles[i]);
        }
    }

}
