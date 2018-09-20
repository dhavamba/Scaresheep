using UnityEngine;
using System.Collections;

public class Sheep : MonoBehaviour 
{
	public float start_speed;
	public float collision_speed;
	public float max_speed;
	public GameObject explosion;
	public float explosion_radius;
	public float increment_sheep;
	public float time_explosion;
	public AudioSource biting;

	private Rigidbody2D body;
	Collider2D[] hits;
		
	bool death;
	CircleCollider2D circle;
	Vector3 scale;

	void Start () 
	{
		death = false;
		body = gameObject.GetComponent<Rigidbody2D>();
		circle = GetComponent<CircleCollider2D>();
		scale = transform.localScale;
		//body.velocity = Vector2.right * start_speed; //da cambiare right con direzione scelta
	}

	void Update () 
	{
		if (!death)
		{
			if (body.velocity.x>=max_speed)
			{
				body.velocity = new Vector2(max_speed,body.velocity.y);
			}
			if (body.velocity.y>=max_speed)
			{
				body.velocity = new Vector2(body.velocity.x,max_speed);
			}
		}
		else
		{
			scale = transform.localScale;
			scale += new Vector3(increment_sheep, increment_sheep, 0);
			transform.localScale = scale;
		}
	}

	void Over()
	{
		hits = Physics2D.OverlapCircleAll(transform.position, explosion_radius); 
		for (int i = 0; i < hits.Length; i++)
		{
			if (hits[i].tag == "Player")
			{
				hits[i].GetComponent<Over>().halfLeff();
			}
		}
		Instantiate(explosion,transform.position,Quaternion.Euler(0f,0f,0f));
		Destroy(this.gameObject);

	}


	void OnCollisionEnter2D(Collision2D coll) 
	{
		if (coll.collider.tag == "Player" && !gameObject.GetComponent<BlackSheep>())
		{
				biting.Play();
				coll.gameObject.GetComponent<Over>().halfLeff();
				die();
		}
		else if (!death)
		{
			float angle = Vector2.Angle(transform.position,coll.gameObject.transform.position);
			//Debug.Log(transform.position-coll.gameObject.transform.position);
			//Debug.Log(angle);
			if (angle<=10.0f)
			{
				body.AddForce(((transform.position-coll.gameObject.transform.position)+new Vector3(Random.Range(-2.0f,2.0f),Random.Range(-2.0f,2.0f),0))*collision_speed);
			}
			else
			{
				body.AddForce((transform.position-coll.gameObject.transform.position)*collision_speed);
			}
		}

	}

	public void die ()
	{
		circle.sharedMaterial = null;
		body.drag = 4f;
		body.gravityScale = 1;
		body.velocity = Vector2.zero;
		body.AddForce(-Vector2.up * 0.01f);
		death = true;
		Invoke("Over", time_explosion);
	}


}
