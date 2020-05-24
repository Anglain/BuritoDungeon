using UnityEngine;
using System.Collections;

public class TopDownShooterCamera : MonoBehaviour {
	
	public float speed = 10f;

	private Transform target;
	private Vector3 mousePos;

	void Awake () {
		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Start () {
		mousePos = GameObject.FindObjectOfType<Camera> ().ScreenToWorldPoint(Input.mousePosition);
	}
		
	void LateUpdate () {
		mousePos = GameObject.FindObjectOfType<Camera> ().ScreenToWorldPoint(Input.mousePosition);
		mousePos.z = -10;
		this.transform.position = Vector3.MoveTowards (this.transform.position, (target.position*2.5f + mousePos) /7*2 , speed * Time.deltaTime);
	}
}
