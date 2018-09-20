using UnityEngine;
using System.Collections;
using Rewired;

public class Shoot : MonoBehaviour 
{

	public GameObject sheep;
	public GameObject blacksheep;
	public float blacksheep_rate;
	public float cooldown;
	public float spawnDistance;
	public float spawnDistance2;
	private float recharge;
	public string vertical;
	public string horizontal;
	public string shoot;

	private GameObject spawn;
	private Manager manager;

	private Player player; //puntatore al Player controll


	// false: destra ; true:sinistra
	private bool direction;

	void Start () 
	{
		manager = GameObject.Find("Manager").GetComponent<Manager>();

		if (gameObject.name == "Player1") player = ReInput.players.GetPlayer(0) as Player;
		else player = ReInput.players.GetPlayer(1) as Player;

		recharge = cooldown;


		direction = true;
		if (gameObject.name == "Player1")
			direction = true;
		else if (gameObject.name == "Player2")
			direction = false;
	}

	void Update () 
	{
		//player.GetButtonDown("Jump")
		if (player.GetAxis("Horizontal") > 0)
		{
			direction = false;
		}
		if (player.GetAxis("Horizontal") < 0)
		{
			direction = true;
		}

		recharge += Time.deltaTime;
		/*
		if (Input.GetButtonDown("Fire1"))
		{
			if (Input.GetAxis("Horizontal")>0)
			{

			}
		}
		*/
		if (player.GetButtonUp("Shoot") && recharge >= cooldown)
		{
			recharge = 0.0f;
			gameObject.GetComponent<AnimationCont>().shoot();

			if (!(manager.getBlackSheep()) && Random.value<blacksheep_rate)
			{
				manager.setBlackSheep(true);
				spawn = blacksheep;
			}
			else
			{
				spawn = sheep;
			}

			if (player.GetAxis("Horizontal") > 0 && player.GetAxis("Vertical") > 0) //alto a destra
			{
				GameObject pecora = Instantiate(spawn,new Vector3(transform.position.x+spawnDistance,transform.position.y+spawnDistance2,transform.position.z),Quaternion.Euler(0f,0f,0f)) as GameObject;
				float start = pecora.GetComponent<Sheep>().start_speed;
				pecora.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f,1.0f) * start;
			}
			else if (player.GetAxis("Horizontal") > 0 && player.GetAxis("Vertical") < 0) // basso a destra
			{
				GameObject pecora = Instantiate(spawn,new Vector3(transform.position.x+spawnDistance,transform.position.y-spawnDistance2,transform.position.z),Quaternion.Euler(0f,0f,0f)) as GameObject;
				float start = pecora.GetComponent<Sheep>().start_speed;
				pecora.GetComponent<Rigidbody2D>().velocity = new Vector2(1.0f,-1.0f) * start;
			}
			else if (player.GetAxis("Horizontal") < 0 && player.GetAxis("Vertical") < 0) // basso a sinistra
			{
				GameObject pecora = Instantiate(spawn,new Vector3(transform.position.x-spawnDistance,transform.position.y-spawnDistance2,transform.position.z),Quaternion.Euler(0f,0f,0f)) as GameObject;
				float start = pecora.GetComponent<Sheep>().start_speed;
				pecora.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.0f,-1.0f) * start;
			}
			else if (player.GetAxis("Horizontal") < 0 && player.GetAxis("Vertical") > 0) // alto a sinistra
			{
				GameObject pecora = Instantiate(spawn,new Vector3(transform.position.x-spawnDistance,transform.position.y+spawnDistance2,transform.position.z),Quaternion.Euler(0f,0f,0f)) as GameObject;
				float start = pecora.GetComponent<Sheep>().start_speed;
				pecora.GetComponent<Rigidbody2D>().velocity = new Vector2(-1.0f,+1.0f) * start;
			}
			else if (player.GetAxis("Horizontal") > 0) // destra
			{
				GameObject pecora = Instantiate(spawn,new Vector3(transform.position.x+spawnDistance,transform.position.y,transform.position.z),Quaternion.Euler(0f,0f,0f)) as GameObject;
				float start = pecora.GetComponent<Sheep>().start_speed;
				pecora.GetComponent<Rigidbody2D>().velocity = Vector2.right * start;
			}
			else if (player.GetAxis("Horizontal") < 0) // sinistra
			{
				GameObject pecora = Instantiate(spawn,new Vector3(transform.position.x-spawnDistance,transform.position.y,transform.position.z),Quaternion.Euler(0f,0f,0f)) as GameObject;
				float start = pecora.GetComponent<Sheep>().start_speed;
				pecora.GetComponent<Rigidbody2D>().velocity = -Vector2.right * start;
			}
			else if (player.GetAxis("Vertical") > 0) // alto
			{
				GameObject pecora = Instantiate(spawn,new Vector3(transform.position.x,transform.position.y+spawnDistance2,transform.position.z),Quaternion.Euler(0f,0f,0f)) as GameObject;
				float start = pecora.GetComponent<Sheep>().start_speed;
				pecora.GetComponent<Rigidbody2D>().velocity = Vector2.up * start;
			}
			else if (player.GetAxis("Vertical") < 0) // basso
			{
				GameObject pecora = Instantiate(spawn,new Vector3(transform.position.x,transform.position.y-spawnDistance2,transform.position.z),Quaternion.Euler(0f,0f,0f)) as GameObject;
				float start = pecora.GetComponent<Sheep>().start_speed;
				pecora.GetComponent<Rigidbody2D>().velocity = -Vector2.up * start;
			}
			else if (!direction) // destra
			{
				GameObject pecora = Instantiate(spawn,new Vector3(transform.position.x+spawnDistance,transform.position.y,transform.position.z),Quaternion.Euler(0f,0f,0f)) as GameObject;
				float start = pecora.GetComponent<Sheep>().start_speed;
				pecora.GetComponent<Rigidbody2D>().velocity = Vector2.right * start;
			}
			else if (direction) // sinistra
			{
				GameObject pecora = Instantiate(spawn,new Vector3(transform.position.x-spawnDistance,transform.position.y,transform.position.z),Quaternion.Euler(0f,0f,0f)) as GameObject;
				float start = pecora.GetComponent<Sheep>().start_speed;
				pecora.GetComponent<Rigidbody2D>().velocity = -Vector2.right * start;
			}
			else //default: right
			{
				GameObject pecora = Instantiate(spawn,new Vector3(transform.position.x+spawnDistance,transform.position.y,transform.position.z),Quaternion.Euler(0f,0f,0f)) as GameObject;
				float start = pecora.GetComponent<Sheep>().start_speed;
				pecora.GetComponent<Rigidbody2D>().velocity = Vector2.right * start;
			}
			
			//Instantiate(sheep,transform.position,Quaternion.Euler(0f,0f,0f));
		}

	}
}
