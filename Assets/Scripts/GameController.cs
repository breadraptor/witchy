using UnityEngine.UI;
using UnityEngine;

using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;
using System.Xml;

public class GameController : MonoBehaviour {

	public static GameController control; // access with GameController.control
	public GameObject BeginGame;
	public PlayerData playerData;
	public Canvas myCanvas;
	public Camera myCamera;
	public GameObject itemPrefab;
	public GameObject potion;
	public Text timeDisplay;
	private XmlDocument items;

	void Awake () {
		// there can only be one time controller.
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if (control != this) {
			Destroy (gameObject);
		}

		//for testing saves:
		//File.Delete(Application.persistentDataPath + "/playerInfo.dat");

		NavigationScript nav = (NavigationScript)myCamera.GetComponent(typeof(NavigationScript));
		if (!Load ()){
			// new game	
			print ("**no save file**");
			nav.onBeginning("new");
		}
		else {
			nav.onBeginning ("main");
		}
	}

	// Use this for initialization
	void Start () {
		timeDisplay.text = System.DateTime.Now.ToShortTimeString();
		items = new XmlDocument();
		items.Load("assets/items.xml"); //TODO will this be fine on the phone??
		InvokeRepeating("TimeUpdate", 0, 1.0f);
	}

	// TimeUpdate is called every second
	void TimeUpdate(){ 
		if (System.DateTime.Now.Minute == 24 && System.DateTime.Now.Second == 15) {
			if (DateTime.Now.Hour == 13){
				// it's midnight
				DailyUpdate();
			}
		}
		timeDisplay.text = System.DateTime.Now.ToString ();
	}

	void DailyUpdate(){
        // grow plants, update world...
        spawnItem(1);
        spawnItem(2);
        spawnItem(2);
        spawnItem(9);
        spawnItem(11);
        spawnItem(20);
		return;
	}

	void spawnItem(int id){
		GameObject item = (GameObject)Instantiate(itemPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
		item.transform.SetParent (myCanvas.transform);
		SpawnedItem script = (SpawnedItem) item.GetComponent<SpawnedItem>();
		script.setID(id);
		item.GetComponent<Button>().onClick.AddListener(delegate {
			addToInventory(script.id);
			// item disappears when it's picked up
			Destroy (item);
		});
	}

	Item FindItemInXml(int id){
		Item itemFound = new Item();
		string findMe = "//item[@id='" + id + "']";
		XmlNodeList nodes = items.SelectNodes(findMe);
		if (nodes.Count == 1){ // it better be...
			XmlNode myNode = nodes[0];
			
			// copy the info from the full item file!
			itemFound.id = id;
			itemFound.name = myNode.ChildNodes[0].InnerText;
			itemFound.type = myNode.ChildNodes[1].InnerText;
			itemFound.description = myNode.ChildNodes[2].InnerText;
			itemFound.edible = Convert.ToBoolean(myNode.ChildNodes[3].InnerText);
			itemFound.white = myNode.ChildNodes[4].InnerText;
			itemFound.blue = myNode.ChildNodes[5].InnerText;
			itemFound.green = myNode.ChildNodes[6].InnerText;
			itemFound.yellow = myNode.ChildNodes[7].InnerText;
			itemFound.orange = myNode.ChildNodes[8].InnerText;
			itemFound.red = myNode.ChildNodes[9].InnerText;
			itemFound.violet = myNode.ChildNodes[10].InnerText;
			itemFound.black = myNode.ChildNodes[11].InnerText;
			itemFound.value = myNode.ChildNodes[12].InnerText;
			
			//print ("Item found: " + itemFound.ToString());
			
		}
		else {
			print ("item not found");
			itemFound = null;
		}
		return itemFound;
	}

	public void addToInventory(int id){
		if (playerData.InventoryData == null){
			// never had an item before, initialize the array!
			playerData.InventoryData = new ArrayList();
		}
		Item existingItem = FindInInventory(id);
		if (existingItem != null){
			// item already in inventory
			playerData.InventoryData.Remove(existingItem);
			// increase quantity and re-add
			existingItem.quantity += 1;
			playerData.InventoryData.Add(existingItem);
		}
		else{
			// get the item info, it's new
			Item item = FindItemInXml(id);
			item.quantity = 1;
			playerData.InventoryData.Add(item);
		}

		printInventory();
		//Save ();
	}

	Item FindInInventory(int id){
		// returns the item if it exists in the inventory
		print ("find item in inventory " + playerData.InventoryData);
		foreach(Item i in playerData.InventoryData){
			if (i.id == id){
				return i;
			}
		}
		return null;

	}

	public void printInventory(){
		if (playerData.InventoryData == null){
			print ("Inv is null");
			return;
		}
		print("ITEMS IN INVENTORY \n ------------------------- \n");
		foreach (Item i in playerData.InventoryData){
			print(i.ToString() + "\n");
		}

	}

	void loopAllXml(){
		foreach(XmlNode node in items.DocumentElement.ChildNodes){ // get all item nodes in items
			
			foreach(XmlNode tag in node.ChildNodes){ // get all tags within item
				print (tag.InnerText);
			}
		} 
	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
		PlayerData data = new PlayerData();
		// TODO: data filling goes here
		data.startDate = playerData.startDate;
		data.witchName = playerData.witchName;
		data.witchType = playerData.witchType;
		data.InventoryData = playerData.InventoryData;

		bf.Serialize(file, data);
		file.Close ();
		print ("game saved");
		//Load ();
	}

	public bool Load(){
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open (Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file); // creates an object
			file.Close();

			// TODO: set local data to the data we pulled from the file
			playerData.witchName = data.witchName;
			playerData.startDate = data.startDate;
			playerData.witchType = data.witchType;
			playerData.InventoryData = data.InventoryData;
			printInventory();

			print ("game loaded");
			return true;
		}
		else {
			return false;
		}
	}

	public void OnApplicationQuit() {
		// save before quitting.
		Save ();
	}
}

[Serializable]
public class PlayerData{
	// serialized class
	// public data....
	public System.DateTime startDate;
	// TODO
	public string witchName;
	public string witchType;
	public ArrayList InventoryData;

}

[Serializable]
public class Item{
	public int id;
	public int quantity;
	
	public string name;
	public string type;
	public string description;
	public bool edible;
	public string white;
	public string blue;
	public string green;
	public string yellow;
	public string orange;
	public string red;
	public string violet;
	public string black;
	public string value;
	
	public override string ToString ()
	{
		return "NAME: " + name + ", QUANTITY: " + quantity + ", ID: " + id;
	}
	
}