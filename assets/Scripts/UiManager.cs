using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UiManager : MonoBehaviour 
{

	public Text vittoriaP1;
	public Text vittoriaP2;

	void Start () 
	{
		vittoriaP1.gameObject.SetActive(false);
		vittoriaP2.gameObject.SetActive(false);
	}

	void Update()
	{
		if (Input.GetKey(KeyCode.Escape))
		{
			Application.Quit();
		}
	}

	public void winP1() 
	{
		vittoriaP1.gameObject.SetActive(true);
	}

	public void winP2() 
	{
		//Debug.Log("COME GET SOME");
		vittoriaP2.gameObject.SetActive(true);
	}

}
