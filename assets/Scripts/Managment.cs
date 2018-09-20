using UnityEngine;
using System.Collections;

public class Managment : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		DontDestroyOnLoad(this);
		Application.LoadLevel("Menu");
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
