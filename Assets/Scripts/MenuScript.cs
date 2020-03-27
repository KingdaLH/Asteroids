using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MenuScript : MonoBehaviour
{

	public GameController instance;
	[SerializeField] Transform menuPanel;
	Event keyEvent;
	Text buttonText;
	KeyCode newKey;

	bool waitingForKey;


	void Start ()
	{
		//Controleren dat panel niet actief is bij starten
		menuPanel.gameObject.SetActive(false);
		waitingForKey = false;

		/*Controleert van elke child van de panel
		 * de namen. De gekoppelde keycode wordt
		 * vervolgens opgeslagen.
		 */
		for(int i = 0; i < menuPanel.childCount; i++)
		{
			if(menuPanel.GetChild(i).name == "ForwardKey")
				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameController.instance.forward.ToString();
			else if(menuPanel.GetChild(i).name == "BackwardKey")
				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameController.instance.backward.ToString();
			else if(menuPanel.GetChild(i).name == "LeftKey")
				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameController.instance.left.ToString();
			else if(menuPanel.GetChild(i).name == "RightKey")
				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameController.instance.right.ToString();
			else if(menuPanel.GetChild(i).name == "JumpKey")
				menuPanel.GetChild(i).GetComponentInChildren<Text>().text = GameController.instance.jump.ToString();
		}
	}

	void Update ()
	{
		//Escape knop opent en sluit het menu
		if(Input.GetKeyDown(KeyCode.Escape) && !menuPanel.gameObject.activeSelf)
			menuPanel.gameObject.SetActive(true);
		else if(Input.GetKeyDown(KeyCode.Escape) && menuPanel.gameObject.activeSelf)
			menuPanel.gameObject.SetActive(false);
	}

	void OnGUI()
	{
		/*keyEvent controleert of de speler
		 * op een knop drukt.
		 */
		keyEvent = Event.current;
		
		if(keyEvent.isKey && waitingForKey)
		{
			newKey = keyEvent.keyCode; //koppelt de nieuwe knop die de speler  indrukt
			waitingForKey = false;
		}
	}

	//Starts co-routine
	public void StartAssignment(string keyName)
	{
		if(!waitingForKey)
			StartCoroutine(AssignKey(keyName));
	}

	//Tekst koppelen aan de knop
	public void SendText(Text text)
	{
		buttonText = text;
	}

	//Afwachten van een ingedrukte toets
	IEnumerator WaitForKey()
	{
		while(!keyEvent.isKey)
			yield return null;
	}

	/*AssignKey neemt een keyName als een parameter. De
	 * keyName wordt gecontroleerd met een switch statement. 
	 */
	public IEnumerator AssignKey(string keyName)
	{
		waitingForKey = true;

		yield return WaitForKey(); //Wordt continu uitgevoerd als de speler niks indrukt

		switch(keyName)
		{
		case "forward":
			GameController.instance.forward = newKey; //Zet forward op de nieuwe keycode
			buttonText.text = GameController.instance.forward.ToString(); //Tekst van de knop aanpassen
			PlayerPrefs.SetString("forwardKey", GameController.instance.forward.ToString()); //save new key to PlayerPrefs
			break;
		case "backward":
			GameController.instance.backward = newKey; //Zet backwards op de  nieuwe keycode
			buttonText.text = GameController.instance.backward.ToString(); //Tekst van de knop aanpassen
			PlayerPrefs.SetString("backwardKey", GameController.instance.backward.ToString()); //Opslaan in PlayerPrefs
			break;
		case "left":
			GameController.instance.left = newKey; //Zet left op de  nieuwe keycode
			buttonText.text = GameController.instance.left.ToString(); //Tekst van de knop aanpassen
			PlayerPrefs.SetString("leftKey", GameController.instance.left.ToString()); //Opslaan in PlayerPrefs
			break;
		case "right":
			GameController.instance.right = newKey; //Zet right op de  nieuwe keycode
			buttonText.text = GameController.instance.right.ToString(); //Tekst van de knop aanpassen
			PlayerPrefs.SetString("rightKey", GameController.instance.right.ToString()); //Opslaan in PlayerPrefs
			break;
		case "jump":
			GameController.instance.jump = newKey; //Zet jump op de  nieuwe keycode
			buttonText.text = GameController.instance.jump.ToString(); //Tekst van de knop aanpassen
			PlayerPrefs.SetString("jumpKey", GameController.instance.jump.ToString()); //Opslaan in PlayerPrefs
			break;
		}

		yield return null;
	}
}
