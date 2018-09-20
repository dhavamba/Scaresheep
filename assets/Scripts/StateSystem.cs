using UnityEngine;
using System.Collections.Generic;


/*
 * Questo script contiene le variabili globali utili per definire lo stato del sistema
 * */

public class StateSystem : MonoBehaviour 
{
	#region Var
	
	public bool windows{get; private set;} //variabile per capire se si usa un sistema windows o altro

	#region Singleton
	
	public static StateSystem controller; //un puntatore statico ad un oggetto della classe di stateSystem (per Singleton)

	/*
	 * Un singleton per la classe stateSystem visto che nel gioco sarà attivo solo e soltanto un oggetto di tale classe.
	 * */
	
	public static StateSystem istance
	{
		get 
		{
			if (controller == null) 
			{
				controller = FindObjectOfType(typeof(StateSystem)) as StateSystem;
			}
			return controller;
		}
	}
	
	#endregion

	#endregion

	#region Start

	void Awake()
	{
		DontDestroyOnLoad (gameObject); //non disrtugge tale classe al passaggio delle scene

		//controlla se sono in ambiente windows
		if (Application.platform == RuntimePlatform.WindowsEditor || Application.platform == RuntimePlatform.WindowsPlayer)
		{
			windows = true;
		}

	}

	void Start()
	{
		/*
		if (Camera.main.aspect < 1.7f) 
		{
			res = true;
			Camera.main.orthographicSize *= 1.08f;
		}
		*/
	}
	

	#endregion
	

}
