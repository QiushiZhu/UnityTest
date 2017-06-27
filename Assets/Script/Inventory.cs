using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {

    public Transform ItemCell;
    public List<Item> items;

    float itemCellWidth;
    float itemCellHeight;

    void AddItem(Item _item,int _num)
    {
        items.Add(_item);
        ItemCell.transform.GetChild(0).GetComponent<Image>().sprite = _item.icon;
        ItemCell.transform.GetChild(2).GetComponent<Text>().text = _num.ToString();
        Transform currentItem = (Transform)Instantiate(ItemCell);
        currentItem.SetParent(this.transform, false);

        //Set Layout
        int RowNum = Mathf.CeilToInt(items.Count / 4f);
        int ColumnNum = items.Count % 4;
        //currentItem.localPosition = new Vector3((ColumnNum - 1) * itemCellWidth, (RowNum - 1) * (-1) * itemCellHeight, 0);
        currentItem.GetComponent<RectTransform>().localPosition = new Vector3(0, 0, 0);
        
    }

	// Use this for initialization
	void Start () {
        itemCellHeight = 200f;
        itemCellWidth = 200f;
        items = new List<Item>();
        AddItem(ItemDataBase.items[2], 10);
        //AddItem(ItemDataBase.items[2], 20);
        //AddItem(ItemDataBase.items[2], 30);
        //AddItem(ItemDataBase.items[2], 40);
        //AddItem(ItemDataBase.items[2], 50);
        //AddItem(ItemDataBase.items[2], 60);
        //AddItem(ItemDataBase.items[2], 70);

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
