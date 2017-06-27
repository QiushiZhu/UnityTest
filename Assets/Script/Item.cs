using UnityEngine;
using System.Collections;

public class Item 
{
    int index;
    string name;
    int value;
    int rare;
    string name_CN;
    public Sprite icon;

    public Item(int _index,string _name,int _value,int _rare,string _name_CN)
    {
        index = _index;
        name = _name;
        value = _value;
        rare = _rare;
        name_CN = _name_CN;

        //Texture2D IconTexture = (Texture2D)Resources.Load("itemIcon/" + name);


        //Sprite sp = Sprite.Create(IconTexture, new Rect(0f,0f,64f,64f), new Vector2(0.5f, 0.5f));       //(0.5f,0.5f) means pivot at center
        icon = Resources.Load<Sprite>("itemIcon/" + name);

    }

}
