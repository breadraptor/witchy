using UnityEngine;
using System.Collections;

public class InventoryScript : MonoBehaviour
{

    public GameController control;
    public ArrayList ItemPics;

    private int inventoryWidth = 5;
    private int iconWidthHeight = 70; //icon is 150x150
    private int spacing = 40;
    private Vector2 offset = new Vector2(60, 40); //start position

    //description window
    private Vector2 descOffset = new Vector2(Screen.width - 500, Screen.height - 100);

    public void OnGUI()
    {
        populateInventory();
    }

    public void populateInventory()
    {
        if (ItemPics == null)
        {
            print("itempics initialized");
            ItemPics = new ArrayList();
        }
        // this loops through displayInventory to check for dups, and loops through InventoryData. efficiency?
        // loop through all the items
        foreach (Item i in control.playerData.InventoryData)
        {
            if (!isDuplicate(i.id))
            {
                DisplayItem di = new DisplayItem();
                Texture itemPic = Resources.Load(i.id.ToString(), typeof(Texture)) as Texture;
                di.id = i.id;
                di.pic = itemPic;
                di.name = i.name;
                di.amount = i.quantity;
                di.desc = i.description;
                //add new item to the array
                ItemPics.Add(di);
            }
        }

        displayItems();
    }

    public bool isDuplicate(int i)
    {
        foreach (DisplayItem di in ItemPics)
        {
            if (di.id == i)
            {
                // it's a duplicate
                return true;
            }
        }
        // not a duplicate
        return false;
    }

    public void displayItems()
    {
        //Take sprites and display them dynamically.
        int j;
        int k;
        DisplayItem currentInventoryItem;
        Rect currentRect;
        for (int i = 0; i < ItemPics.Count; i++)
        {
            j = i / inventoryWidth; // divide to get rows
            k = i % inventoryWidth; // mod to get columns
            currentInventoryItem = (DisplayItem)ItemPics[i];
            currentRect = (new Rect(offset.x + k * (iconWidthHeight + spacing), offset.y + j * (iconWidthHeight + spacing), iconWidthHeight, iconWidthHeight));
            GUI.DrawTexture(currentRect, currentInventoryItem.pic); //display picture
            GUI.Label(currentRect, currentInventoryItem.amount.ToString()); //display quantity

            
            if (GUI.Button(currentRect, "", new GUIStyle("label"))) // create invisible button on each item
            {
                displayDescription(currentInventoryItem);
                // display description when clicked
                //print(currentInventoryItem.desc);
            }
            
            
        }

    }

    private void displayDescription(DisplayItem di)
    {
        GUI.Box(new Rect(descOffset.x, descOffset.y, 400, 100), di.desc);
        print("fkdsl");
    }
}

    public class DisplayItem {

        public int id;
        public string name;
        public Texture pic;
        public int amount;
        public string desc;
    }
