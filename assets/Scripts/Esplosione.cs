using UnityEngine;
using System.Collections;

public class Esplosione : MonoBehaviour 
{
	private float cont;
	
	void Start () 
	{
		cont = 0.0f;
	}

	void Update () 
	{
		cont += Time.deltaTime;
		if (cont>=3.0f)
		{
			Destroy(gameObject);
		}
	}
}
