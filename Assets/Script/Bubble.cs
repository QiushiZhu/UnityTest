using UnityEngine;
using System.Collections;


public class Bubble : MonoBehaviour {
   

    Transform InnerCircle;
    Transform OuterCicle;

    [SerializeField]
    float rotateSpeed;

    void Awake()
    {
        InnerCircle = this.transform.GetChild(1);
        OuterCicle = this.transform.GetChild(0);
    }

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        InnerCircle.Rotate(0, 0, rotateSpeed);
        OuterCicle.Rotate(0, 0, -rotateSpeed);
        
	}
}
