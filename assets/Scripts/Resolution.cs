using UnityEngine;
using System.Collections;

/*
 * Classe che si occupa della gestione della risoluzione della camera in base all'aspect ratio.
 * Per raggiungere tale scopo, la classe crea delle letterboxes (bande nere).
 * */

public class Resolution : MonoBehaviour 
{

    public Vector2 nativeResolution;

	private float windowAspect; //valore dell'aspect ratio corrente
	private float nativeAspect; //valore dell'aspect ratio nativo

	
	/*
	 * Metodo Start
	 * */
	void Start () 
	{
		nativeAspect = nativeResolution.x / nativeResolution.y; 
		windowAspect = (float)Screen.width / (float)Screen.height;

		scaleCamera(); //scalo la camera in base all'aspect ratio corrente
	}

	/*
	 * Se si gioca in window mode, l'aspect ratio può variare dinamicamente, quindi si deve controllare in real time.
	 * */
	void FixedUpdate()
	{
		if (!Screen.fullScreen) controlScale(); //se non sono a schermo intero controlo periodicamente il mio aspect ratio
	}

	/*
	 * Funzione che mi permette di scalare la camera in funzione del mio aspectRatio corrente
	 * */
	void scaleCamera()
	{
		
		float scaleHeight = windowAspect / nativeAspect; // valore che serve per sistemare il view port della camera
		Camera camera = GetComponent<Camera>(); // ottengo puntatore alla camera
		Rect rect = camera.rect; //salvo la view port della camera
		
		// Se il mio aspect ratio è minore del mio aspect ratio nativo, attivo le bande nere verticale (se è uguale non cambio nulla)
		if (windowAspect < nativeAspect)
		{  	
			//sistemo la view tramite dei valori trovati via Internet
			rect.width = 1.0f;
			rect.height = scaleHeight;
			rect.x = 0;
			rect.y = (1.0f - scaleHeight) / 2.0f;
		}
		// Se il mio aspect ratio è maggiore del mio aspect ratio nativo, attivo le bande nere orizzontali
		else if (windowAspect > nativeAspect)
		{
			//sistemo la view tramite dei valori trovati via Internet
			float scaleWidth = 1.0f / scaleHeight; // valore che serve per sistemare il view port della camera
			
			rect.width = scaleWidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scaleWidth) / 2.0f;
			rect.y = 0;
		}
		
		camera.rect = rect; //aggiorno la view port
	}

	/*
	 * Controllo periodicamente se l'aspect ratio attuale è diverso da quello che ho registrato, se si, scalo la camera
	 * */
	void controlScale()
	{
		float windowAux = (float)Screen.width / (float)Screen.height; //la window attuale
		
		//se la window corrente è diversa dalla window precedente salvata, vuol dire che si deve riscalare la camera
		if (windowAux != windowAspect) 
		{
			windowAspect = windowAux;
			scaleCamera();
		}
	}

}
