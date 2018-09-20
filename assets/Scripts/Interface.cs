using UnityEngine;
using System.Collections;

public class Interface : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
	
	}
	
	public void StartGame()
	{
		Application.LoadLevel("Game");
	}

	public void Credits()
	{
		Invoke ("_Credits", 0.5f);
	}

	void _Credits()
	{
		Application.LoadLevel("Credits");
	}

	public void Instructions()
	{
		Invoke ("_Instruction", 0.5f);
	}

	void _Instruction()
	{
		Application.LoadLevel("Instructions");
	}

	public void Quit()
	{
		Invoke ("_Quit", 0.5f);
	}

	void _Quit()
	{
		Application.Quit();
	}
}
