using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NavigationScript : MonoBehaviour {

	public Canvas canvas;
    public Canvas permanentCanvas;
    public NavigationScript nav;
	public bool inventory = false;
	public bool potion = false;
	public bool main = false;
	public bool beginning = false;

	void Awake(){
        if (nav == null) {
            DontDestroyOnLoad(gameObject);
            nav = this;
        }
    }

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
		else if (potion){
			deactivateAll();
			canvas.transform.Find ("MakePotion").gameObject.SetActive(true);
		}
		else if (inventory){
			deactivateAll();
            GameObject inv = canvas.transform.Find("Inventory").gameObject;
			inv.SetActive(true);

			// this is to make sure the inventory is re-populated every single time it's opened.
			//InventoryScript script = (InventoryScript) inv.GetComponent<InventoryScript>();
            //script.OnGui();
		}
		else if (beginning){
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
        //TODO fix this... it needs to go back to wherever you were. (scene)
		main = true;
		potion = false;
		inventory = false;
		menuNavigation();
	}

	public void deactivateAll(){
        if (canvas != null) { 
		    foreach (Transform child in canvas.transform){
			    child.gameObject.SetActive(false);
		    }
        }
        
    }

}
