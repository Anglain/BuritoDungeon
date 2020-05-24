using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

	public float fireRate = 15f;					//if 0 - the weapon is single-burst
	public LayerMask whatToHit;						//Layers which we can hit
	public float effectSpawnRate = 10f;				//How many times a sec (max) we can shoot

	public Transform bulletPrefab;

	private float timeToFire = 0f;
	private float timeToSpawnEffect = 0f;
	private Transform firePointLeft;
	private Transform firePointLeftToMiddle;
	private Transform firePoint;
	private Transform firePointRightToMiddle;
	private Transform firePointRight;

	void Awake () {
		firePointLeft = GameObject.Find ("FirePointLeft").transform;
		firePointLeftToMiddle = GameObject.Find ("FirePointLeftToMiddle").transform;
		firePoint = GameObject.Find ("FirePoint").transform;
		firePointRightToMiddle = GameObject.Find ("FirePointRightToMiddle").transform;
		firePointRight = GameObject.Find ("FirePointRight").transform;

		if (firePointLeft == null) {
			Debug.LogError ("No FirePointLeft found for the player!");
		}

		if (firePointLeftToMiddle == null) {
			Debug.LogError ("No FirePointLeftToMiddle found for the player!");
		}

		if (firePoint == null) {
			Debug.LogError ("No FirePoint found for the player!");
		}

		if (firePointRightToMiddle == null) {
			Debug.LogError ("No FirePointRightToMiddle found for the player!");
		}

		if (firePointRight == null) {
			Debug.LogError ("No FirePointRight found for the player!");
		}
	}

	void Start () {

	}

	void Update () {							//Update() - called every frame
		if (Input.GetButton ("Fire1") && Time.time > timeToFire) {
			timeToFire = Time.time + 1 / fireRate;
			Shoot ();
		}
	}

	void Shoot () {
		
		if (Time.time >= timeToSpawnEffect) {
			Effect ();
			timeToSpawnEffect = Time.time + 1f / effectSpawnRate;
		}
	}

	void Effect () {
		Instantiate (bulletPrefab, firePointLeft.position, firePointLeft.rotation);
		Instantiate (bulletPrefab, firePointLeftToMiddle.position, firePointLeftToMiddle.rotation);
		Instantiate (bulletPrefab, firePoint.position, firePoint.rotation);
		Instantiate (bulletPrefab, firePointRightToMiddle.position, firePointRightToMiddle.rotation);
		Instantiate (bulletPrefab, firePointRight.position, firePointRight.rotation);
	}
}
