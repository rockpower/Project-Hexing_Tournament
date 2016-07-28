using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UI_controller : MonoBehaviour {









	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void toMainMenu()
	{	
		Debug.Log ("Main Menu Clicked. Returning...");
		Application.LoadLevel ("MainMenu");
	}


	public void toTutorial()
	{
		Debug.Log ("Tutorial Clicked. Loading...");
		Application.LoadLevel ("Apprentice");
	}

	public void toArena()
	{
		Debug.Log ("Arena Clicked. Loading...");
		Application.LoadLevel ("Arena");
	}

	public void exit()
	{	
		Debug.Log ("Exit Confirmed. Exiting...");
		Application.Quit();
	}
}
