﻿using UnityEngine;
using System.Collections;
using Rewired;

/*
 * Il movimento 
 * */

public class Control : MonoBehaviour 
{

	#region Variables

	#region FSM

	//I possibili stati del player
	public enum FSMState
	{
		Walk,
		OffGround
	}
	
	public FSMState curState; //lo stato attuale del player

	#endregion

	#region Grounded

	public LayerMask whatIsGround;  //indica cosa è terreno
	private bool isGround; //booleano che indica se il player è nel terreno (true) o in volo (false)
	private bool isBlock; //booleano che indica che Laika è bloccata da un muro.

	#endregion

	#region Movement

	[Range(0, 10f)]
	public float speed; //la velocità del player

	public bool facingRight{get;set;} //indica se il player è girato a destra (true) o a sinistra (false)
	private Player player; //puntatore al Player controll
	private float input; //float che conserva il valore dell'input

	#endregion

	#region Pointers

	public static Control controller; //un puntatore statico ad un oggetto della classe AControl (per Singleton)

	private Rigidbody2D body; //un puntatore al rigidbody
	private BoxCollider2D box; //un puntatore al boxCollider
	private Transform transf; //un puntatore a transform

	#endregion

	#region aux

	Vector2 _aux; //vettore ausiliario 1
	Vector2 _aux2; //vettore ausiliario 2 
	RaycastHit2D _hit; //un oggetto RaycastHit2D

	#endregion

	#region Jump

	[Range(0, 20f)]
	public float jumpForce; //la potenza del salto

	private float limitFallVelocity; //limita la velocità di caduta

	#endregion

	#endregion

	#region Start

	#region Singleton
	
	/*
	 * Metodo per la classe che serve per il Singleton, ritorna un puntatore statico (o lo crea se non esiste).
	 * Questo perchè nel gioco sarà attivo solo e soltanto un oggetto di tale classe.
	 * */
	public static Control istance
	{
		get 
		{
			if (controller == null) 
			{
				controller = FindObjectOfType(typeof(Control)) as Control;
			}
			return controller;
		}
	}

	#endregion

	// Use this for initialization
	void Start () 
	{
		transf = GetComponent<Transform>() as Transform;
		body = GetComponent<Rigidbody2D>() as Rigidbody2D;
		box = GetComponent<BoxCollider2D>() as BoxCollider2D;

		if (gameObject.name == "Player1") player = ReInput.players.GetPlayer(0) as Player;
		else player = ReInput.players.GetPlayer(1) as Player;

		facingRight = Utily.FloatToBoolean(transf.localScale.x); //metto la direzione in base alla direzione del pg.
		limitFallVelocity = 9.81f; //il massimo della velocità di caduta
	}

	#endregion

	#region Update
	
	/*
	 * Nell'update controllo in che stato sono e poi attivo l'update di quello stato.
	 * */
	void Update ()
	{
        isGround = Grounded(); //vede se il player è a terra o è in volo
		isBlock = blockHorizontal(); //vede se il player è bloccato da qualcosa
		
		if (!isGround)
		{
			curState = FSMState.OffGround;
		}
		else
		{
			curState = FSMState.Walk;
		}

		//FSM: I casi sono i nodi, ogni update controlla in che stato il player è ed esegue il l'update relativo.
		switch (curState)
		{
			case FSMState.Walk: UpdateWalkState(); break;
			case FSMState.OffGround: UpdateOffGround(); break;
		}
	}

	/*
	 * Controlli
	 * */
	void FixedUpdate()
	{
		if (body.velocity.y < 0)
		{
			controlVelocity(); //limita la velocità di caduta
		}
	}

	void UpdateWalkState()
	{
		Move ();
		if (player.GetButtonDown("Jump"))
		{
			Jump();
		}
	}

	void UpdateOffGround()
	{
		Move ();
	}

	#endregion

	void Jump()
	{
		body.AddForce(new Vector2(0, jumpForce * 100));

	}

	void Move()
	{	
		input = player.GetAxis("Horizontal");

		if (!isBlock) 
		{
			body.velocity = new Vector2(input * (speed * 100) * Time.deltaTime, body.velocity.y);
		}
		
		if ( (input < 0 && facingRight) || (input > 0 && !facingRight) ) 
		{
			Flip();
		} 
	}

	public void Flip() 
	{
		facingRight = !facingRight;
		
		Vector3 theScale = transf.localScale;
		theScale.x *= -1;
		transf.localScale = theScale;
		theScale.x *= -1;
	}

	bool Grounded()
	{
		_aux = new Vector2(box.bounds.min.x, box.bounds.min.y - 0.2f);
		_aux2 = new Vector2(box.bounds.max.x, box.bounds.min.y - 0.2f);
		_hit = Physics2D.Linecast(_aux, _aux2 , whatIsGround );
		return Utily.ObjectToBoolean(_hit.collider);
	}

	bool blockHorizontal()
	{
		if (facingRight)
		{
			_aux = new Vector2(box.bounds.max.x + 0.2f, box.bounds.min.y);
			_aux2 = new Vector2(box.bounds.max.x + 0.2f, box.bounds.max.y);
		}
		else
		{
			_aux = new Vector2(box.bounds.min.x - 0.2f, box.bounds.min.y);
			_aux2 = new Vector2(box.bounds.min.x - 0.2f, box.bounds.max.y);
		}
		_hit = Physics2D.Linecast(_aux, _aux2 , whatIsGround );
		return Utily.ObjectToBoolean(_hit.collider);
	}

	/*
	 * Questa funziona controlla la velocità di caduta, la limita se diventa troppo grande.
	 * */
	void controlVelocity()
	{
		// Accellera l'aumento di velocità, questo serve per dare l'illusione al giocatore che la velocità è costante
		if (body.velocity.y > -limitFallVelocity) 
		{
			body.velocity = new Vector2(body.velocity.x, body.velocity.y - 0.2f);
		}
		else
		{
			body.velocity = new Vector2(body.velocity.x, -limitFallVelocity); //setta la velocità a fallVelocity
		}
	}

}
