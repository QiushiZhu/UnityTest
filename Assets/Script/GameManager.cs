using UnityEngine;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {


    public Player player;
    public Enemy enemy;
    public BubbleManager bm;

    public FloatText damageText;    
    

    void Awake()
    {
        
    }

	// Use this for initialization
	void Start () {
        enemy.DeathEvent += EnemyDeath;
	}



    void EnemyDeath(object sender,EventArgs e)          //这里用的是.Net的委托-事件模式
    {

        bm.BubbleMultiKill();
        player.Gold = player.Gold + enemy.loot;
        bm.BubbleMultiPop();

        if (enemy.level > 20)
        {
            bm.RedBubbleActive = true;
        }
    }


    void NormalAttack(int _damage,Vector3 position)
    {   
        //player anim,junp
        //enemy anim, flank
        damageText.damage = _damage;
        Instantiate(damageText);
        enemy.DamageEnemy(_damage);
    }

    void ExecuteAttack(int _damage, Vector3 position)
    {
        damageText.damage = _damage;
        Instantiate(damageText);
        enemy.DamageEnemy(_damage);
    }

    void MissedAttack()
    {

    }

    void CritAttack()
    {
 
    }
	
	void Update () 
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Attack();
        }

	}

    void Attack()
    {
        Collider2D[] col = Physics2D.OverlapPointAll(Camera.main.ScreenToWorldPoint(Input.mousePosition));

        if (col.Length > 0)
        {           
            foreach (Collider2D c in col)
            {
                if(c.tag =="RedBubble")
                {
                    ExecuteAttack(player.damage * player.RedBubbleDamageParam, c.transform.position);
                    RedBubble b = c.GetComponent<RedBubble>();
                    bm.KillRedBubble(b);
                    return;
                }

                else if (c.tag == "Bubble")
                {                    
                    NormalAttack(player.damage,c.transform.position);
                    Bubble b = c.GetComponent<Bubble>();
                    bm.KillBubble(b);
                    return;
                }
            }
        }
    }

    
}
