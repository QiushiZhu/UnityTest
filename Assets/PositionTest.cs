using UnityEngine;
using System.Collections;

public class PositionTest : MonoBehaviour
{
    
    //public Transform IconPrefab;

    // Use this for initialization
    void Start()
    {
        GameObject test = new GameObject();
        SpriteRenderer sr = test.AddComponent<SpriteRenderer>();
        Sprite sp = Resources.Load<Sprite>(@"itemIcon\other\bluePotion01");
        sr.sprite = sp;
        test.transform.position = new Vector3(1,1,1);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
