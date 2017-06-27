using UnityEngine;
using System.Collections;
using System;

public class Enemy : MonoBehaviour
{

    public int level;
    private int stage;
    int sHealth;
    [HideInInspector]
    public int cHealth;
    public int levelInStage;
    [HideInInspector]
    public int loot;
    SpriteRenderer sr;
    [SerializeField]
    private EnemyStatsUI stats;
    public event EventHandler DeathEvent;
    public float TThealthDivider;
    Rect MonsterRect;
    

    void Awake()
    {
        sr = this.GetComponent<SpriteRenderer>();
    }

    // Use this for initialization
    void Start()
    {
        MonsterRect = sr.sprite.textureRect;
        MonsterInit();
    }

    // Update is called once per frame
    void Update()
    {


    }

    void MonsterInit()
    {
        stage = (int)Math.Ceiling(level / (double)levelInStage);

        sHealth = (int)(Math.Pow(29.045 / TThealthDivider, stage) * bossStageDeterminer(level));
        cHealth = sHealth;

        loot = Mathf.CeilToInt(TThealthDivider * sHealth * (0.02f + 0.00045f * stage));

        //string MonsterName = "Monster" + Convert0to00(level);
        //Texture2D monsterTexture = (Texture2D)Resources.Load(MonsterName);
        string MonsterName = level.ToString();
        Texture2D monsterTexture = (Texture2D)Resources.Load("Monsters/" + MonsterName);


        Sprite sp = Sprite.Create(monsterTexture, MonsterRect, new Vector2(0.5f, 0.5f));       //(0.5f,0.5f) means pivot at center
        sr.sprite = sp;

        stats.SetHealth(cHealth, sHealth);
        stats.SetStage(level);

    }

    int bossStageDeterminer(int level)
    {
        if (level % levelInStage != 0)
        {
            return 1;
        }
        else if (stage % 5 == 0)
        {
            return 10;
        }
        else if (stage % 4 == 0)
        {
            return 7;
        }
        else if (stage % 3 == 0)
        {
            return 6;
        }
        else if (stage % 2 == 0)
        {
            return 4;
        }
        else
        {
            return 2;
        }
    }

    string Convert0to00(int number)
    {
        if (number < 10 && number >= 0)
        {
            string a = number.ToString();
            string b = "0" + a;
            return b;
        }

        else if (number >= 10 && number < 100)
        {
            string a = number.ToString();
            return a;
        }

        else
        {
            return "FXXK!OVER 100!";
        }
    }



    void Death()
    {
        level++;
        if (DeathEvent != null)
            DeathEvent(this, EventArgs.Empty);
        MonsterInit();
    }

    public void DamageEnemy(int damage)
    {
        cHealth -= damage;
        if (cHealth <= 0)
        {
            Death();
        }

        if (stats)
        {
            stats.SetHealth(cHealth, sHealth);
        }
    }
}
