using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CanvasBehavior : MonoBehaviour, IHasChanged {
	public Transform slots;
	public Text inventoryText;
	private ArrayList items;
	
	// Use this for initialization
	void Start () {
		HasChanged ();
	}

	public void onBackpackClick(){
		this.transform.FindChild("Inventory").gameObject.SetActive(true);
		print ("backpackclicked");
	}

	public void onCauldronClick(){
		this.transform.FindChild("MakePotion").gameObject.SetActive(true);
		print ("cauldronclicked");
	}

	public void onBackClick(){
		this.transform.FindChild ("MakePotion").gameObject.SetActive(false);
	}
	
	public void onBrewClick(){
		// combine the three ingredients
		
		if (items.Count < 3){
			print ("less than three items");
		}
		else {
			// TODO brew the items!
		}
	}
	
	#region IHasChanged implementation
	
	public void HasChanged ()
	{
		items = new ArrayList();
		foreach (Transform slotTransform in slots){
			GameObject item = slotTransform.GetComponent<Slot>().item;
			if (item){
				items.Add(item.name);
			}
		}
		string itemOutput = " ";
		foreach (string i in items){
			itemOutput = itemOutput + " " + i;
		}
		inventoryText.text = itemOutput;
	}
	
	#endregion
}

namespace UnityEngine.EventSystems {
	public interface IHasChanged : IEventSystemHandler {
		void HasChanged();
	}



}
