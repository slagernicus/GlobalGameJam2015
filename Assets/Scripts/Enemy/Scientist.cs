﻿using UnityEngine;
using System.Collections;

public class Scientist : EnemyController {

	

	public override string getProjectileString(){
		return "Prefabs/AcidBolt";
	}
	
	public override void setProjectile(string s){
		base.setProjectile(s);
	}
	
	public override void setStats(int x, int y, int z){
		base.setStats(1,8, 20);
	}
	
	public virtual string getSpriteString(){
		return "Sprites/Scientist/Scientist1";
	}
	
	public virtual void setSprite(string s){
		this.gameObject.GetComponent<SpriteRenderer>().sprite = (Sprite)(Resources.Load(s) as Sprite);
		this.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
	}
	

	
	
	void FixedUpdate(){
		//anim.SetFloat ("vSpeed", rigidbody2D.velocity.y);
		//anim.SetFloat("Speed", rigidbody2D.velocity.x);
	}
	
	public override void kill ()
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
			anim.SetBool("attacking", false);
			
			
		}
		attackpath = (pos - transform.position);
		}
	}


	
	public override IEnumerator Attack() {
		anim.SetBool("attacking",true);
		rigidbody2D.velocity = new Vector2(0,0);
		GameObject head = Instantiate(Resources.Load ("Prefabs/AcidBolt")as GameObject, transform.position + Vector3.right, transform.rotation) as GameObject;
		head.transform.parent = this.transform;
		head.AddComponent<BoxCollider2D>().isTrigger = true;

		yield return new WaitForSeconds(1);
		this.runningCoroutine = null;
		Destroy(head);
	}


}
