using UnityEngine;
using System.Collections;

public class NavigationScript : MonoBehaviour {

	public Canvas canvas;
	public bool inventory = false;
	public bool potion = false;
	public bool main = false;
	public bool beginning = false;

	
	void Start () {
		potion = false;
		inventory = false;
		beginning = false;
		main = true;
		menuNavigation();
	}

	public void menuNavigation(){
		if (main){
			deactivateAll();
			canvas.transform.Find ("InventoryButton").gameObject.SetActive(true);
			canvas.transform.Find ("Cauldron").gameObject.SetActive(true);
			canvas.transform.Find ("ToBeachButton").gameObject.SetActive(true);
			//canvas.transform.Find ("TestItem").gameObject.SetActive(true);
		}
		if (potion){
			deactivateAll();
			canvas.transform.Find ("MakePotion").gameObject.SetActive(true);
		}
		if (inventory){
			deactivateAll();
			canvas.transform.Find("Inventory").gameObject.SetActive(true);
		}

	}

	public void onToBeachButtonClick(){
		Application.LoadLevel ("beach");
	}

	public void onInventoryButtonClick(){
		main = false;
		inventory = true;
		menuNavigation();
	}
	public void onCauldronClick(){
		main = false;
		potion = true;
		menuNavigation();
	}

	public void onBackClick(){
		main = true;
		potion = false;
		inventory = false;
		menuNavigation();
	}

	public void deactivateAll(){
		foreach (Transform child in canvas.transform){
			child.gameObject.SetActive(false);
		}
	}

}
