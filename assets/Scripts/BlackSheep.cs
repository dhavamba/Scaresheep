using UnityEngine;
using System.Collections;

public class BlackSheep : MonoBehaviour 
{
	public int limitEat;
	public float incrementEat;
	public AudioSource biting;

	private int sheepEated;
	private Rigidbody2D body;

	bool death;
	Vector3 scale;
	
	void Start () 
	{
		death = false;
		sheepEated = 0;
		body = gameObject.GetComponent<Rigidbody2D>();
		scale = transform.localScale;
	}
	

	void Update () 
	{
	
	}
	
	void OnCollisionEnter2D(Collision2D coll) 
	{
		if ( (coll.collider.tag == "Sheep" && !death) || (coll.collider.tag == "Player") )
		{
			if (!coll.gameObject.GetComponent<BlackSheep>())
			{
				if (coll.collider.tag == "Sheep")
					Destroy(coll.gameObject);
				else if (coll.collider.tag == "Player")
					coll.gameObject.GetComponent<Over>().halfLeff();
				sheepEated++;
				biting.Play();
				if (sheepEated >= limitEat)
				{
					GameObject.Find("Manager").GetComponent<Manager>().setBlackSheep(false);
					death = true;
					GetComponent<Sheep>().die();
				}
				else
				{
					body.velocity = new Vector2(body.velocity.x*0.7f,body.velocity.y*0.7f);
					scale += new Vector3(incrementEat, incrementEat, 0);
					transform.localScale = scale;
				}
			}
		}

		
	}

}
