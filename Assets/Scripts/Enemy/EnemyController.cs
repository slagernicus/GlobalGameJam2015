﻿using UnityEngine;
using System.Collections;

public class EnemyController : MonoBehaviour {

	public int ATK;
	public int HP;
	public int SPD;
	public int RNG;
	public Vector3 attackpath;
	GameObject p;
	OGChickenController _player;
	Animator anim;
	GameObject head;

	private bool attacking;
	private Coroutine runningCoroutine;


	// Use this for initialization
	void Start () {
		//this.attacking = false;
		anim = GetComponent<Animator>();

		p = GameObject.FindGameObjectWithTag("Player");
        _player = p.GetComponent<OGChickenController>();
        
		this.SPD = 1;
		this.RNG = 5;
		this.HP = 10;
		this.head = Resources.Load ("Prefabs/Head1") as GameObject;
	}
	
	// Update is called once per frame
	void Update () {


		this.kill();
	}



	void OnCollisionEnter2D(Collision2D c)
	{
		if(c.gameObject.tag == "Player")
		{
			_player.currentHP -= 10f;
		}

		if(c.gameObject.tag == "beak")
		{
			this.HP -= 10;
		}	
	}



	void kill()
	{
		if(p!= null){
			Vector3 pos = p.transform.position;
			rigidbody2D.AddForce((pos - transform.position)*SPD);
			if(pos.magnitude < RNG ) // player in range
			{	//this.attacking = true;

				if(this.runningCoroutine==null){
					this.runningCoroutine = StartCoroutine(this.Attack());
				}


			}
			else{
				anim.SetBool("attack", false);


			}
			attackpath = (pos - transform.position);
		}
	}

	IEnumerator Attack() {
		anim.SetBool("attack",true);
		//attack();
		this.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
		rigidbody2D.velocity = new Vector2(0,0);
		
		
		//(pos - transform.position)
		
		GameObject head = Instantiate(this.head, transform.position + new Vector3(1,-2, 0), transform.rotation) as GameObject;
		head.transform.parent = this.transform;
		head.AddComponent<BoxCollider2D>().isTrigger = true;

		Debug.Log(Time.time);
		yield return new WaitForSeconds(2);
		Debug.Log(Time.time);
		this.runningCoroutine = null;
		Destroy(head);
	}


}
