using UnityEngine.UI;
using UnityEngine;
using System.Collections;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameController : MonoBehaviour {

	public static GameController control; // access with GameController.control
	public GameObject BeginGame;
	public PlayerData playerData;

	public GameObject potion;

	public Text timeDisplay;

	void Awake () {
		// there can only be one time controller.
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if (control != this) {
			Destroy (gameObject);
		}

		if (!Load ()){
			// new game	
			BeginGame.SetActive (true);

		}
	}

	// Use this for initialization
	void Start () {
		//for testing saves:
		//File.Delete(Application.persistentDataPath + "/playerInfo.dat");

		timeDisplay.text = System.DateTime.Now.ToShortTimeString();
		InvokeRepeating("TimeUpdate", 0, 1.0f);
	}

	// TimeUpdate is called every second
	void TimeUpdate(){ 
		if (System.DateTime.Now.Minute == 0 && System.DateTime.Now.Second == 0) {
			if (System.DateTime.Now.Hour == 0){
				// it's midnight
			
				DailyUpdate();
			}
		}

		//print (witchName.ToString());
		timeDisplay.text = System.DateTime.Now.ToString ();
	}

	void DailyUpdate(){
		// grow plants, update world...
		//Vector3 location = new Vector3()
		GameObject drop = (GameObject)Instantiate(potion, new Vector3(0,1,2), Quaternion.identity);
		drop.SetActive(true);
		return;
	}

	// Update is called once per frame
	void Update () {

	}

	public void Save(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create (Application.persistentDataPath + "/playerInfo.dat");
		PlayerData data = new PlayerData();
		// TODO: data filling goes here
		data.startDate = playerData.startDate;
		data.witchName = playerData.witchName;
		data.witchType = playerData.witchType;

		bf.Serialize(file, data);
		file.Close ();
		print ("game saved");
		Load ();
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

			print ("game loaded");
			return true;
		}
		else {
			return false;
		}
	}

	public void OnApplicationQuit() {
		// save before quitting.
		//Save ();
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

}