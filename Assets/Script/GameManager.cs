using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    public Transform BubblePefab;
    public int bubbleInitNum;
    //public float bubblePopInterval = 0.5f;
    public int bubbleNumUpperLimit;

    public Player player;
    public Enemy enemy;

    public float[] bubblePopInterval;  //超限必然会出BUG，需要处理

    float bubbleZoneLeft = -1.5f;
    float bubbleZoneRight = 1.5f;
    float bubbleZoneLower = 1f;
    float bubbleZoneUpper = 3.5f;

    float lastBubblePopTime = 0;

    static System.Collections.Generic.List<Bubble> bubbles = new System.Collections.Generic.List<Bubble>();

    public FloatText damageText;

    void Awake()
    {
        
    }

	// Use this for initialization
	void Start () {
        BubbleMultiPop();
        enemy.DeathEvent += EnemyDeath;
	}

    public static void KillBubble(Bubble bubble)     //这里用的是教学视频的Singleton模式
    {
        Destroy(bubble.gameObject);
        bubbles.Remove(bubble);
    }


    void EnemyDeath(object sender,EventArgs e)          //这里用的是.Net的委托-事件模式
    {

        for (int i = 0; i < bubbles.Count; i++)
        {
            KillBubble(bubbles[i]);
        }
        player.Gold = player.Gold + enemy.loot;
        BubbleMultiPop();
    }


    void NormalAttack(int _damage,Vector3 position)
    {       


        //player anim,junp
        //enemy anim, flank
        damageText.damage = _damage;
        Instantiate(damageText,position,Quaternion.identity);
        enemy.DamageEnemy(_damage);
    }

    void MissedAttack()
    {

    }

    void CritAttack()
    {
 
    }
	
	void Update () {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }


        //BubbleAutoPopFunction
        if ((Time.time - lastBubblePopTime) > bubblePopInterval[bubbles.Count]  &&   bubbles.Count<=bubbleNumUpperLimit)
        {
            BubblePop();
            lastBubblePopTime = Time.time;
        }

	}

    void Attack()
    {
        Collider2D[] col = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (col.Length > 0)
        {           
            foreach (Collider2D c in col)
            {
                if (c.tag == "Bubble")
                {                    
                    NormalAttack(player.damage,c.transform.position);
                    Bubble b = c.GetComponent<Bubble>();
                    KillBubble(b);
                    return;
                }
            }
        }
    }

    

    public void BubbleMultiPop()
    {
        for (int i = 0; i < bubbleInitNum; i++)
        {
            BubblePop();
        }
    }

    public void GeneralTest()
    {
        Debug.Log("_gm test.");
    }

    public void BubblePop()
    {
        Vector3 bubblePosInit = new Vector3(UnityEngine.Random.Range(bubbleZoneLeft, bubbleZoneRight), UnityEngine.Random.Range(bubbleZoneLower, bubbleZoneUpper), 0);
        Transform b = (Transform) Instantiate(BubblePefab, bubblePosInit, Quaternion.identity);
        bubbles.Add(b.GetComponent<Bubble>());
    }
}
