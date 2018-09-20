using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour 
{
	public float tempo_restart;

	private bool fine_gioco;
	private float cont;
	private AudioSource source;
	private bool fine;
	private string nome;

	private bool blacksheep;
	
	void Start () 
	{
		blacksheep = false;
		fine_gioco = false;
		cont = 0.0f;
		source = gameObject.GetComponent<AudioSource>();
		fine = true;
	}

	void Update () 
	{

		if (fine_gioco)
		{
			if (fine)
			{
				if (nome == "Player1")
				{
					GameObject.Find("UiObj").GetComponent<UiManager>().winP2();
				}
				else if (nome == "Player2")
				{
					//Debug.Log("COME GET SOME");
					GameObject.Find("UiObj").GetComponent<UiManager>().winP1();
				}

				source.Play();
				fine = false;
				//Debug.Log("FINEEEEEEE");
			}

			cont += Time.deltaTime;
			if (cont>= tempo_restart)
			{
				Application.LoadLevel(Application.loadedLevel);
			}
		}
	}

	public void setFineGioco (string n)
	{
		//Debug.Log(n);
		nome = n;
		fine_gioco = true;
	}

	public void setBlackSheep (bool b)
	{
		blacksheep = b;
	}

	public bool getBlackSheep ()
	{
		return blacksheep;
	}

	
}
