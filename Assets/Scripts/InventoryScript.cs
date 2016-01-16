using UnityEngine;
using System.Collections;

public class InventoryScript : MonoBehaviour {

	public GameController control;
	public ArrayList ItemPics;
	
	public void Start () {

	}

	public void populateInventory(){
		if (ItemPics == null){ 
			print ("itempics initialized");
			ItemPics = new ArrayList(); 
		}
		//TODO this loops through displayInventory to check for dups, and loops through InventoryData. efficiency?

		// loop through all the items
		foreach (Item i in control.playerData.InventoryData){
			if (!isDuplicate (i.id)){
				DisplayItem di = new DisplayItem();
				Sprite itemPic = Resources.Load (i.id.ToString(), typeof(Sprite)) as Sprite;
				di.id = i.id;
				di.pic = itemPic;
				//add new item to the array
				ItemPics.Add(di);
				print (di.id.ToString ());
			}
		}

		displayItems();
	}

	public bool isDuplicate(int i){

		foreach (DisplayItem di in ItemPics){
			if (di.id == i){
				// it's a duplicate
				return true;
			}
		}
		// not a duplicate
		return false;
	}

	public void displayItems(){
		//TODO take sprites and display them dynamically.

	}

}

public class DisplayItem{

	public int id;
	public Sprite pic;
}
