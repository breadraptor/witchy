using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BeginGameScript : MonoBehaviour {

	public System.DateTime startDate;
	public string witchType;
	public string witchName;

	public GameObject NameUI;
	public GameObject canvas;
	public GameObject QuizPrefab;
	
	Dictionary<string, int> answers;
	int quizClicked = 1;

	// Use this for initialization
	void Start () {
		startDate = System.DateTime.Now.Date; // this is the day the game started
		// find children: GameObject blah = UIElements.transform.Find ("Name/NameBox").gameObject;
		answers = new Dictionary<string, int>();
		answers.Add("Lunar", 0);
		answers.Add ("Faerie", 0);
		answers.Add("Fire", 0);
		answers.Add("Garden", 0);
		answers.Add ("Sea", 0);
		answers.Add("Earth", 0);
		answers.Add("Animal", 0);
		answers.Add ("Divinity", 0);
		NameUI.gameObject.SetActive (true);

	}

	// called when SubmitName button is clicked
	public void SubmitNameClick(){
		string name = NameUI.transform.FindChild("NameBox").FindChild ("Text").gameObject.GetComponent<Text>().text;
		if (name.Equals("")){
			print ("no name");
		}
		else {
			witchName = name;
			//GameController.control.Save();
			NameUI.SetActive(false);

		}
		StartQuiz(1);
	}

	public void StartQuiz(int number){
		// spawn the question
		if (number < 5){
			GameObject question1 = (GameObject)Instantiate(QuizPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
			question1.transform.SetParent (canvas.transform);
			question1.GetComponent<RectTransform>().localPosition = new Vector3(0,1,0);

			GameObject a1 = question1.transform.FindChild("Answer1").gameObject;
			a1.GetComponent<Button>().onClick.AddListener(delegate {
				QuizChoiceClick(a1.transform.FindChild("Text").gameObject.GetComponent<Text>().text);
			});

			GameObject a2 = question1.transform.FindChild("Answer2").gameObject;
			a2.GetComponent<Button>().onClick.AddListener(delegate {
				QuizChoiceClick(a2.transform.FindChild("Text").gameObject.GetComponent<Text>().text);
			});

			GameObject a3 = question1.transform.FindChild("Answer3").gameObject;
			a3.GetComponent<Button>().onClick.AddListener(delegate {
				QuizChoiceClick(a3.transform.FindChild("Text").gameObject.GetComponent<Text>().text);
			});

			if (number == 1){
				question1.transform.FindChild ("Question1").gameObject.GetComponent<Text>().text = "What type of person are you?";
				a1.transform.FindChild("Text").gameObject.GetComponent<Text>().text = "Night owl";
				a2.transform.FindChild("Text").gameObject.GetComponent<Text>().text = "Morning person";
				a3.transform.FindChild("Text").gameObject.GetComponent<Text>().text = "Midday is best";
			}
			else if (number == 2){
				question1.transform.FindChild ("Question1").gameObject.GetComponent<Text>().text = "What's your ideal adventure?";
				a1.transform.FindChild("Text").gameObject.GetComponent<Text>().text = "A long hike";
				a2.transform.FindChild("Text").gameObject.GetComponent<Text>().text = "Travel to a new city";
				a3.transform.FindChild("Text").gameObject.GetComponent<Text>().text = "Adopt a pet";
			}
			else if (number == 3){
				question1.transform.FindChild ("Question1").gameObject.GetComponent<Text>().text = "What trait would you say describes you most?";
				a1.transform.FindChild("Text").gameObject.GetComponent<Text>().text = "Tenacious";
				a2.transform.FindChild("Text").gameObject.GetComponent<Text>().text = "Honest";
				a3.transform.FindChild("Text").gameObject.GetComponent<Text>().text = "Kind";
			}
			else if (number ==4){
				question1.transform.FindChild ("Question1").gameObject.GetComponent<Text>().text = "What's your favorite?";
				a1.transform.FindChild("Text").gameObject.GetComponent<Text>().text = "Cake";
				a2.transform.FindChild("Text").gameObject.GetComponent<Text>().text = "Tea";
				a3.transform.FindChild("Text").gameObject.GetComponent<Text>().text = "Stew";
			}
			question1.SetActive(true);
		}
		else {
			// quiz is over
			findWitchType ();
			return;
		}
	}

	public void QuizChoiceClick(string answer){
		ProcessAnswer(answer);
		// hide all previous questions
		foreach (GameObject test in GameObject.FindGameObjectsWithTag("quiz")){
			test.SetActive(false);
		}
		StartQuiz (++quizClicked);

	}

	public void FinalChoiceClick(string answer){
		witchType = answer;
		print ("witchtype is " + witchType);
		foreach (GameObject test in GameObject.FindGameObjectsWithTag("quiz")){
			test.SetActive(false);
		}
		completeBeginning();
	}

	public void ProcessAnswer(string answer){
		switch(answer){
		case "Night owl":
			answers["Lunar"]++;
			answers["Divinity"]++;
			break;
		case "Morning person":
			answers["Faerie"]++;
			answers["Sea"]++;
			answers["Earth"]++;;
			break;
		case "Midday is best":
			answers["Fire"]++;
			answers["Garden"]++;
			answers["Animal"]++;
			break;
		case "A long hike":
			answers["Lunar"]++;
			answers["Garden"]++;
			answers["Earth"]++;;
			break;
		case "Travel to a new city":
			answers["Fire"]++;
			answers["Sea"]++;
			answers["Divinity"]++;
			break;
		case "Adopt a pet":
			answers["Faerie"]++;
			answers["Animal"]++;
			break;
		case "Tenacious":
			answers["Fire"]++;
			answers["Earth"]++;;
			answers["Divinity"]++;
			break;
		case "Honest":
			answers["Lunar"]++;
			answers["Garden"]++;
			answers["Sea"]++;
			break;
		case "Kind":
			answers["Faerie"]++;
			answers["Animal"]++;
			break;
		case "Cake":
			answers["Faerie"]++;
			answers["Animal"]++;
			break;
		case "Tea":
			answers["Lunar"]++;
			answers["Garden"]++;
			answers["Divinity"]++;
			break;
		case "Stew":
			answers["Fire"]++;
			answers["Sea"]++;
			answers["Earth"]++;;
			break;
		default:
			print ("default case");
			break;
		}
	}

	private void findWitchType(){
		int maxNum = 0;
		string maxName = "test";
		ArrayList tie = new ArrayList();
		foreach (KeyValuePair<string, int> entry in answers){
			if (entry.Value > maxNum) {
				maxName = entry.Key;
				maxNum = entry.Value;
			}
		}
		tie.Add(maxName);
		foreach (KeyValuePair<string, int> entry in answers){
			// look for ties
			if (entry.Value == maxNum && entry.Key != maxName){
				tie.Add(entry.Key);
				//print (tie[tie.Count-1]);
			}
		}
		if (tie.Count > 1){
			// we got a tie. Let the user pick one
			GameObject finalQuestion = (GameObject)Instantiate(QuizPrefab, new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity);
			finalQuestion.transform.SetParent (canvas.transform);
			finalQuestion.GetComponent<RectTransform>().localPosition = new Vector3(0,1,0);
			finalQuestion.transform.FindChild ("Question1").gameObject.GetComponent<Text>().text = "What type of witch do you feel you are?";

			for(int i = 0; i < tie.Count; i++){
				string buttonNum = "Answer" + (i + 1).ToString();
				GameObject a1 = finalQuestion.transform.FindChild(buttonNum).gameObject;
				a1.GetComponent<Button>().onClick.AddListener(delegate {
					FinalChoiceClick(a1.transform.FindChild("Text").gameObject.GetComponent<Text>().text);
				});
				a1.transform.FindChild("Text").gameObject.GetComponent<Text>().text = (string)tie[i];


			}
			finalQuestion.SetActive(true);
			/* this would be the random way of solving a tie.
			int rand = Random.Range (0, tie.Count);
			foreach (string entry in tie){
				if (tie.IndexOf(entry) == rand){
					maxName = entry;
					break;
				}
			} */
		}
		else {
			FinalChoiceClick((string)tie[0]);
		}
	}

	public void completeBeginning(){
		// save the data we just collected
		GameController.control.playerData.witchName = witchName;
		GameController.control.playerData.witchType = witchType;
		GameController.control.playerData.startDate = startDate;
		GameController.control.Save (); 

	}
}



