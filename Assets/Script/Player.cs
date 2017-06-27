using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Player : MonoBehaviour {

    public int level;
    [HideInInspector]public int damage;    

    public event EventHandler GoldChanged;
    private int levelCost;
    private int _gold;
    public int Gold
    {
        get { return _gold; }
        set
        {
            _gold = value;
            if (GoldChanged != null)
            {
                GoldChanged(value, EventArgs.Empty);
            }
        }
    }

    [SerializeField]
    Button levelUpButton;
    [SerializeField]
    Text goldText;

	// Use this for initialization
	void Start () {
        GoldChanged += GoldChangedEventHandler;
        Gold = 1;
        levelCost = (int)(Mathf.Pow(1.06f, level) * (level + 4));
        damage = (int)Mathf.RoundToInt(Mathf.Pow(1.05f, level) * level);

        LevelUpButtonUpdate();
	}
	
	// Update is called once per frame
	void Update () {
	  
	}

    public void levelUp()
    {
        level++;
        Gold = Gold - levelCost;
        levelCost =(int) (Mathf.Pow(1.06f, level) * (level+4));
        damage = (int)Mathf.RoundToInt( Mathf.Pow(1.05f, level) * level);

        LevelUpButtonUpdate();
    }

    void GoldChangedEventHandler(object sender, EventArgs e)
    {        
        goldText.text = Gold.ToString();


        LevelUpButtonUpdate();

    }

    void LevelUpButtonUpdate()
    {
        Text levelUpCostText = levelUpButton.GetComponentInChildren<Text>();
        levelUpCostText.text = levelCost.ToString();


        if (Gold >= levelCost)
        {
            levelUpButton.interactable = true;
        }
        else
        {
            levelUpButton.interactable = false;
        }
    }

    
}
