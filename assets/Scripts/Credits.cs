using UnityEngine;
using System.Collections;

public class Credits : MonoBehaviour {

	// Update is called once per frame
	void Update () 
	{
		if (Input.GetMouseButton(0) || Input.GetKey("b"))
		{
			Application.LoadLevel("Menu");
		}
	
	}
}
