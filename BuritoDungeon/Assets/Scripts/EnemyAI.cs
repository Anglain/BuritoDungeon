using System.Collections;
using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Seeker))]
public class EnemyAI : MonoBehaviour {

	public Transform target;
	public float updateRate = 2f;						//Path update rate

	public Path path;									//Calculated path
	public float speed = 300f;							//AI speed per second
	//public ForceMode2D forceMode;						//Change betweed FORCE andd IMPULSE
	public float nextWaypointDistance = 0.5f;			//Max distance to the waypoint to continue to the next waypoint
	public float searchForPlayerRate = 3f;				//How much times a second we should search for the player

	[HideInInspector]
	public bool pathIsEnded = false;
	[HideInInspector]
	public Vector3 direction;

	private Seeker seeker;
	private Rigidbody2D rb2D;
	private SpriteRenderer sr;
	private int currentWaypoint = 0;					//The waypoint we are currently moving towards
	private bool searchingForPlayer = false;	

	void Awake () {
		seeker = GetComponent <Seeker> ();
		rb2D = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();

		if (sr == null) {
			Debug.LogError ("No SpriteRenderer object found on the enemy!");
		}
	}

	void Start () {
		
		if (target == null) {
			if (!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer ());
			}
			return;
		}

		seeker.StartPath (transform.position, target.position, OnPathCompete);			//Start a new path to the target position and return the result to the OnPathComplete() method

		StartCoroutine (UpdatePath ()); 
	}

	public void OnPathCompete (Path p) {
		Debug.Log ("We got a path. Did it have an error? " + p.error);

		if (!p.error) {
			path = p;
			currentWaypoint = 0;
		}
	}

	IEnumerator UpdatePath () {
		if (target == null) {
			if (!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer ());
			}
			yield return false;
		}

		seeker.StartPath (transform.position, target.position, OnPathCompete);			//Start a new path to the target position and return the result to the OnPathComplete() method

		yield return new WaitForSeconds (1f / updateRate);
		StartCoroutine (UpdatePath ());
	}

	void FixedUpdate () {
		if (target == null) {
			if (!searchingForPlayer) {
				searchingForPlayer = true;
				StartCoroutine (SearchForPlayer ());
			}
			return;
		}

		if (path == null) {
			return;
		}

		if (target.position.y < transform.position.y) {
			sr.sortingOrder = -1;
		} else {
			sr.sortingOrder = 2	;
		}
			
		//TODO: Always look at player (MISSILES (?) )

		if (currentWaypoint >= path.vectorPath.Count) {
			
			if (pathIsEnded) {
				return;
			} else {
				//Debug.Log ("The end of path is reached");
				pathIsEnded = true;
				return;
			}
		} else {
			pathIsEnded = false;
		}

		//Direction to the next waypoint
		direction = (path.vectorPath [currentWaypoint] - transform.position) * speed * Time.fixedDeltaTime;
		Debug.Log ("1 - " + (path.vectorPath [currentWaypoint] - transform.position));
		Debug.Log ("2 - " + ((path.vectorPath [currentWaypoint] - transform.position).normalized));
		Debug.Log ("3 - " + direction);

		//Move the AI
		rb2D.AddForce (direction);
		//transform.Translate (direction);

		float dist = Vector3.Distance (transform.position, path.vectorPath [currentWaypoint]);

		if (dist < nextWaypointDistance) {
			currentWaypoint++;
			return;
		}
	}

	IEnumerator SearchForPlayer () {
		GameObject searchResult = GameObject.FindGameObjectWithTag ("Player");

		if (searchResult == null) {
			yield return new WaitForSeconds (1f / searchForPlayerRate);
			StartCoroutine (SearchForPlayer ());
		} else {
			target = searchResult.transform;
			searchingForPlayer = false;
			StartCoroutine (UpdatePath ());
			yield return false;
		}
	}

}
