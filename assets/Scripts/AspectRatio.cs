using UnityEngine;
using System.Collections;

public class AspectRatio : MonoBehaviour 
{
	private Camera camera;
	
	void Start () 
	{

		camera = gameObject.GetComponent<Camera>();



		if (camera.aspect >= 1.77f && camera.aspect <= 1.78f)
		{
			camera.orthographicSize = 5.0f;
		}
		else if (camera.aspect >= 1.59f && camera.aspect <= 1.61f)
		{
			camera.orthographicSize = 5.55f;
			GameObject.Find("sfondiInsieme").transform.localScale = new Vector3(0.38f, 0.38f, 1.0f);
		}
		else if (camera.aspect >= 1.49f && camera.aspect <= 1.51f)
		{
			camera.orthographicSize = 5.9f;
			GameObject.Find("sfondiInsieme").transform.localScale = new Vector3(0.4f, 0.4f, 1.0f);
		}
		else if (camera.aspect >= 1.33f && camera.aspect <= 1.34f)
		{
			camera.orthographicSize = 6.7f;
			GameObject.Find("sfondiInsieme").transform.localScale = new Vector3(0.46f, 0.46f, 1.0f);
		}
		else if (camera.aspect >= 1.24f && camera.aspect <= 1.26f)
		{
			camera.orthographicSize = 7.1f;
			GameObject.Find("sfondiInsieme").transform.localScale = new Vector3(0.48f, 0.48f, 1.0f);
		}




		//Debug.Log(gameObject.GetComponent<Camera>().aspect);
		//camera.fieldOfView = 20.0f;
	}

	void Update () 
	{
	
	}
}
