using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class TimeController : MonoBehaviour {

	private System.DateTime startDate;
	public Text timeDisplay;

	void Awake () {
		DontDestroyOnLoad (gameObject);

	}

	// Use this for initialization
	void Start () {
		//string date = System.DateTime.Now.Date.ToString ();

		timeDisplay.text = System.DateTime.Now.ToShortTimeString();
		//GetComponent<Text> ().text = time;
		InvokeRepeating("TimeUpdate", 0, 1.0f);

	}

	// TimeUpdate is called every second
	void TimeUpdate(){ 
		if (System.DateTime.Now.Minute == 0) {
			if (System.DateTime.Now.Hour == 0){
				// it's midnight
				DailyUpdate();
			}
		}

		timeDisplay.text = System.DateTime.Now.ToString ();
	}

	void DailyUpdate(){
		// grow plants, update world...
		return;
	}

	// Update is called once per frame
	void Update () {

	}
}
