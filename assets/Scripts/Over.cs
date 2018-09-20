using UnityEngine;
using System.Collections;

public class Over : MonoBehaviour 
{

	public float timeInvicible;
	public float starting_life;
	private float life;
	public float tempoLampeggio;
	private float timeLamp;
	bool invincible;

	SpriteRenderer sprite;
	BoxCollider2D box;
    Transform lifes;

	// Use this for initialization
	void Start () 
	{
		life = starting_life;
		invincible = false;
		sprite = GetComponent<SpriteRenderer>();
		timeLamp = tempoLampeggio;
		sprite.enabled = true;
		Physics2D.IgnoreLayerCollision(10,8,false);
        if (gameObject.name == "Player1") lifes = GameObject.Find("LifePlayer1").transform;
        else lifes = GameObject.Find("LifePlayer2").transform;

    }

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.collider.tag == "Sheep")
		{
			Vector2 dir = other.collider.gameObject.GetComponent<Rigidbody2D>().velocity;
			dir.Normalize();
			GetComponent<Rigidbody2D>().AddForce(-dir * 1000);
			Leef();
		}
	}

	void Leef()
	{
		if (!invincible)
		{
			life--;
			less();
		}
	}

	public void halfLeff()
	{
		if (!invincible)
		{
			life -= 1.0f; //questo perchè l'esplosione prende due volte il player per via dei due collider
			less();
		}
	}

	void less()
	{
        if (lifes.childCount != 0) GameObject.Destroy(lifes.GetChild(0).gameObject);
		if (life == 0)
		{
			//Debug.Log("gameover");
			GameObject.Find("Manager").GetComponent<Manager>().setFineGioco(gameObject.name);
			Destroy(gameObject);
		}
		else
		{
			invincible = true;
			Physics2D.IgnoreLayerCollision(10,8,true);
			Invoke("notInvicible", timeInvicible);
		}
	}
	
	void notInvicible()
	{
		Physics2D.IgnoreLayerCollision(10,8, false);
		sprite.enabled = true;
		invincible = false;
	}

	void Update()
	{
		timeLamp += Time.deltaTime;
		if (invincible && timeLamp>=tempoLampeggio)
		{
			if (sprite.enabled) 
			{
				sprite.enabled = false;
			}
			else 
			{
				sprite.enabled = true;
			}
			timeLamp = 0.0f;
		}
	}
}
