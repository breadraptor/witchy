using UnityEngine;
using System.Collections;

public class PermanentCanvasScript : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		DontDestroyOnLoad (gameObject);
	}

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
