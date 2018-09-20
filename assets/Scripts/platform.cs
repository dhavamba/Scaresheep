using UnityEngine;
using System.Collections;

public class platform : MonoBehaviour 
{
	float cont;
	Animator animator;
	public float time_bounce;


	void Start () 
	{
		animator = gameObject.GetComponent<Animator>();
		cont = time_bounce;
		animator.SetBool("IsBounce",false);
	}
	

	void Update () 
	{
		cont += Time.deltaTime;
		if (cont>=time_bounce)
		{
			animator.SetBool("IsBounce",false);
		}
	}

	void OnCollisionEnter2D(Collision2D coll) 
	{
		cont = 0.0f;
		animator.SetBool("IsBounce",true);
	}
}
