using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
		main = false;
	}

	public void onBeginning(string opt){
		if (opt.Equals("main")){
			main = true;
		}
		else {
			// begin game script
			beginning = true;
		}
		menuNavigation ();
	}

	public void menuNavigation(){
		if (main){
			deactivateAll();
			canvas.transform.Find ("InventoryButton").gameObject.SetActive(true);
			canvas.transform.Find ("Cauldron").gameObject.SetActive(true);
			canvas.transform.Find ("ToBeachButton").gameObject.SetActive(true);
			canvas.transform.Find ("main room").gameObject.SetActive(true);
		}
		if (potion){
			deactivateAll();
			canvas.transform.Find ("MakePotion").gameObject.SetActive(true);
		}
		if (inventory){
			deactivateAll();
            GameObject inv = canvas.transform.Find("Inventory").gameObject;
			inv.SetActive(true);

			// this is to make sure the inventory is re-populated every single time it's opened.
			//InventoryScript script = (InventoryScript) inv.GetComponent<InventoryScript>();
            //script.OnGui();
		}
		if (beginning){
			deactivateAll ();
			canvas.transform.FindChild ("NameUI").gameObject.SetActive(true);
			canvas.transform.FindChild ("BeginGame").gameObject.SetActive(true);
		}

	}

	public void onToBeachButtonClick(){
		SceneManager.LoadScene("beach");
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
