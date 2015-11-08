using UnityEngine;
using System.Collections;

public class SpawnedItem : MonoBehaviour {

	public int id;
	public SpawnedItem sp;

	// Use this for initialization
	void Start () {
		sp = this;
		setPosition();
		this.gameObject.SetActive(true);
	}
	
	public void setID(int i){
		this.id = i;
	}

	public int getID(){
		return id;
	}

	public void setPosition(){
		//TODO maybe pick a few positions where we're okay with items spawning, then randomize between those.
		this.GetComponent<RectTransform>().localPosition = new Vector3(UnityEngine.Random.Range(-200.0F, 200.0F),UnityEngine.Random.Range(-200.0F, 0.0F),0);
	}
}
