using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SpawnedItem : MonoBehaviour {

	public int id;
	public SpawnedItem sp;
	public Sprite itemPic;

	// Use this for initialization
	void Start () {
		sp = this;
		setPosition();
		this.gameObject.SetActive(true);
		itemPic = Resources.Load (id.ToString(), typeof(Sprite)) as Sprite;
		this.GetComponent<Image>().sprite = itemPic;

	}
	
	public void setID(int i){
		this.id = i;
	}

	public int getID(){
		return id;
	}

	public void setPosition(){
		//TODO this just spawns everything on the floor.
		this.GetComponent<RectTransform>().localPosition = new Vector3(UnityEngine.Random.Range(-300.0F, 300.0F),UnityEngine.Random.Range(-200.0F, -120.0F),0);
	}
}
