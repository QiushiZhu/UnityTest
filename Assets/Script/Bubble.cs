using UnityEngine;
using System.Collections;


public class Bubble : MonoBehaviour {

    SpriteRenderer sr;
    float colorAlpha;

    public float duration;
    float liveTime;
    GameManager _gm;

    Transform InnerCircle;
    Transform OuterCicle;

    public float rotateSpeed;

    void Awake()
    {
        InnerCircle = this.transform.GetChild(1);
        OuterCicle = this.transform.GetChild(0);
    }

	// Use this for initialization
	void Start () {
        sr = this.GetComponent<SpriteRenderer>();
        colorAlpha = 1;
        sr.color = new Color(1, 0, 0, colorAlpha);
        liveTime = Time.time;
        
	}
	
	// Update is called once per frame
	void Update () {
        InnerCircle.Rotate(0, 0, rotateSpeed);
        OuterCicle.Rotate(0, 0, -rotateSpeed);
        //colorAlpha = colorAlpha - 1 / duration / 100;
        //sr.color = new Color(1, 0, 0, colorAlpha);
        if ((Time.time - liveTime) > duration)
        {
            //GameManager.KillBubble(this);
        }
	}
}
