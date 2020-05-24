using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Bullet : MonoBehaviour {

	public float damage = 10f;
	public float moveSpeed = 10f;

	private GameObject player;
	private SpriteRenderer bulletSR;
	private float bulletSpriteBound;
	private float playerSpriteBound;

	void Awake () {
		player = GameObject.FindGameObjectWithTag ("Player");

		if (player == null) {
			Debug.LogError ("No player found!");
		}

		bulletSR = GetComponent<SpriteRenderer> ();

		if (bulletSR == null) {
			Debug.LogError ("No SpriteRenderer attached to the bullet!");
		}
	}

	void Start () {
		bulletSpriteBound = transform.position.y;
		playerSpriteBound = player.transform.position.y;

		if (playerSpriteBound < bulletSpriteBound) {
			bulletSR.sortingOrder = -2;
		} else {
			bulletSR.sortingOrder = 1;
		}
	}

	void Update () {
		bulletSpriteBound = transform.position.y;
		//Debug.Log (bulletSpriteBound);
		playerSpriteBound = player.transform.position.y;
		//Debug.Log (playerSpriteBound);

		if (playerSpriteBound < bulletSpriteBound) {
			bulletSR.sortingOrder = -1;
		} else {
			bulletSR.sortingOrder = 1;
		}
	}

	void FixedUpdate () {
		transform.Translate (Vector3.right * Time.deltaTime * moveSpeed);
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.CompareTag ("Enemy")) {

		}

		//Destroy (this.gameObject);
	}
}
