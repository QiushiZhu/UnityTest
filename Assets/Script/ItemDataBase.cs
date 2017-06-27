using UnityEngine;
using System.Collections;

public class ItemDataBase : MonoBehaviour {

    public static Item[] items;

	// Use this for initialization
	void Start () {


        TextAsset ItemText = Resources.Load("ItemDataBase", typeof(TextAsset)) as TextAsset;
        string[] itemEntryText = ItemText.text.Split("\r"[0]);

        int ItemCount = itemEntryText.Length-1;

        items = new Item[ItemCount];

        for (int i = 1; i < ItemCount; i++)
        {
           // Debug.Log(itemEntryText[i]);
            items[i] = new Item(int.Parse(itemEntryText[i].Split(',')[0]), itemEntryText[i].Split(',')[1], int.Parse(itemEntryText[i].Split(',')[2]), int.Parse(itemEntryText[i].Split(',')[3]), itemEntryText[i].Split(',')[4]);

        }
	}

    // Update is called once per frame
    void Update()
    {
	
	}
}
