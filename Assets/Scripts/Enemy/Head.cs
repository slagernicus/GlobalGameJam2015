﻿using UnityEngine;
using System.Collections;

public class Head : MonoBehaviour {
	private GameObject player;
	// Use this for initialization
	void Start () {
		player = GameManager.player;
	}
	
	// Update is called once per frame
	void Update () {
		if(gameObject.transform.parent!=null && player!=null){
			Vector3 p = gameObject.transform.parent.transform.position;
			Vector3 player2 = player.transform.position;
			Vector3 u = (player2 - p).normalized;
			this.rigidbody2D.AddForce(new Vector2(u.x, u.y)*50f);
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.tag == "Player"){
			GameManager._player.currentHP -= 15;
		}

		else{
			if(other.gameObject.tag!="Enemy"){
				Destroy(gameObject);
			}
		}
	}
}
