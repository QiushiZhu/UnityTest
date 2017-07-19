using UnityEngine;
using System.Collections;

public class FloatText : MonoBehaviour {

    public Sprite[] numbers;
    private GameObject[] Nums;
    private GameObject anchor;
    public float MoveUpSpeed;   //上漂距离
    public float FadeOutTime;   //消失时间，没打算渐隐，直接消失

    private float InitTime;
    public float stayTime;

    public int damage;

    public void show(int _num)
    {
        int length = _num.ToString().Length;        
        GameObject[] Nums = new GameObject[length];
        for (int i = 0; i < length; i++)
        {
            Nums[i] = new GameObject("char"+ i.ToString());     //这句是最蠢的
            Nums[i].transform.parent = anchor.transform;
            SpriteRenderer sr = Nums[i].AddComponent<SpriteRenderer>();
            int value = int.Parse(_num.ToString()[i].ToString());       //这句也很蠢...用于获取数字的第i位的值，比如1745的第0位为1，第一位为7.如果用取余会优雅一点。
            sr.sprite = numbers[value];
        }

        CenteringGameObjectsInX(Nums, anchor, 0.3f);        //控制间距为0.3

    }

    void MoveUp()
    {
        Vector3 currentPosition = this.transform.position;
        Vector3 newPosition = new Vector3(currentPosition.x, currentPosition.y+=MoveUpSpeed , currentPosition.z);
        this.transform.position = newPosition;
    }

    //按X轴居中
    static void CenteringGameObjectsInX(GameObject[] childs, GameObject parent, float interval)
    {
        float startingPoint = parent.transform.position.x - 0.5f * interval * childs.Length;
        for (int i = 0; i < childs.Length; i++)
        {
            childs[i].transform.position = new Vector3(startingPoint + i * interval, parent.transform.position.y, parent.transform.position.z);
        }
    }


    void Awake()
    {
        anchor = this.gameObject;
    }


	// Use this for initialization
	void Start () {
        Destroy(this.gameObject, FadeOutTime);
        InitTime = Time.time;
        show(damage);
	}
	
	// Update is called once per frame
	void Update () {
        if (Time.time - InitTime > stayTime)
        {
            MoveUp();
        }
	}
}
