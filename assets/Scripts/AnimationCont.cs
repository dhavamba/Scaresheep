using UnityEngine;
using System.Collections;

public class AnimationCont : MonoBehaviour {

	float cont;
	Animator animator;
	public float time_shoot;
	
	
	void Start () 
	{
		animator = gameObject.GetComponent<Animator>();
		cont = time_shoot;
		animator.SetBool("IsShoot",false);
	}
	
	
	void Update () 
	{
		cont += Time.deltaTime;
		if (cont>=time_shoot)
		{
			animator.SetBool("IsShoot",false);
		}
	}
	
	public void shoot ()
	{
		cont = 0.0f;
		animator.SetBool("IsShoot",true);
	}
}
