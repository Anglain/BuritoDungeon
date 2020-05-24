using UnityEngine;
using System.Collections;

public class PlayerRotation : MonoBehaviour {
	
	/// <summary>
	/// The object will change sprites in 8 directions according to the mouse position
	/// Attach to the object, which is the child of the player
	/// Otherwise write on line 22 "GetComponent" instead of "GetComponentInParent"
	/// </summary>

	public Sprite up;
	public Sprite right;
	public Sprite down;
	public Sprite left;
	public Sprite upLeft;
	public Sprite upRight;
	public Sprite downRight;
	public Sprite downLeft;
	public float diff = 10f;

	private SpriteRenderer sr;
	private Transform firePointLeft;
	private Transform firePointLeftToMiddle;
	private Transform firePoint;
	private Transform firePointRightToMiddle;
	private Transform firePointRight;

	void Awake () {
		sr = GetComponentInParent<SpriteRenderer> ();
		firePointLeft = GameObject.Find ("FirePointLeft").transform;
		firePointLeftToMiddle = GameObject.Find ("FirePointLeftToMiddle").transform;
		firePoint = GameObject.Find ("FirePoint").transform;
		firePointRightToMiddle = GameObject.Find ("FirePointRightToMiddle").transform;
		firePointRight = GameObject.Find ("FirePointRight").transform;

		if (sr == null) {
			Debug.LogError ("No SpriteRenderer component found on the Player!");
		}

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

	// Update is called once per frame
	void Update () {
		//subtracting the position  of the player from the mouse position
		Vector3 difference = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position;
		difference.Normalize ();	//normalizing the vector

		float rotationZ = Mathf.Atan2 (difference.y,difference.x) * Mathf.Rad2Deg; //find the angle in degrees
		transform.rotation = Quaternion.Euler (0f, 0f, rotationZ);
		//Debug.Log (rotationZ);

		if (rotationZ < 22.5f && rotationZ >= -22.5f) {																		//Change sprites in the direction where the mouse is
			sr.sprite = right;
			firePoint.position = Vector3.right * 0.16f + transform.position;
			firePoint.rotation = Quaternion.Euler (0f, 0f, 0f);
			firePointLeft.position = firePoint.position;
			firePointLeft.rotation = Quaternion.Euler (0f, 0f, diff);
			firePointRight.position = firePoint.position;
			firePointRight.rotation = Quaternion.Euler (0f, 0f, -diff);
			firePointLeftToMiddle.position = firePoint.position;
			firePointLeftToMiddle.rotation = Quaternion.Euler (0f, 0f, diff/2f);
			firePointRightToMiddle.position = firePoint.position;
			firePointRightToMiddle.rotation = Quaternion.Euler (0f, 0f, -diff/2f);
		} else if (rotationZ < -22.5f && rotationZ >= -67.5f) {
			sr.sprite = downRight;
			firePoint.position = (Vector3.right + Vector3.down) * 0.16f + transform.position;
			firePoint.rotation = Quaternion.Euler (0f, 0f, -45f);
			firePointLeft.position = firePoint.position;
			firePointLeft.rotation = Quaternion.Euler (0f, 0f, -45f + diff);
			firePointRight.position = firePoint.position;
			firePointRight.rotation = Quaternion.Euler (0f, 0f, -45f - diff);
			firePointLeftToMiddle.position = firePoint.position;
			firePointLeftToMiddle.rotation = Quaternion.Euler (0f, 0f, -45f + diff/2f);
			firePointRightToMiddle.position = firePoint.position;
			firePointRightToMiddle.rotation = Quaternion.Euler (0f, 0f, -45f - diff/2f);
		} else if (rotationZ < -67.5f && rotationZ >= -112.5f) {
			sr.sprite = down;
			firePoint.position = Vector3.down * 0.16f + transform.position;
			firePoint.rotation = Quaternion.Euler (0f, 0f, -90f);
			firePointLeft.position = firePoint.position;
			firePointLeft.rotation = Quaternion.Euler (0f, 0f, -90f + diff);
			firePointRight.position = firePoint.position;
			firePointRight.rotation = Quaternion.Euler (0f, 0f, -90f - diff);
			firePointLeftToMiddle.position = firePoint.position;
			firePointLeftToMiddle.rotation = Quaternion.Euler (0f, 0f, -90f + diff/2f);
			firePointRightToMiddle.position = firePoint.position;
			firePointRightToMiddle.rotation = Quaternion.Euler (0f, 0f, -90f - diff/2f);
		} else if (rotationZ < -112.5f && rotationZ >= -157.5f) {
			sr.sprite = downLeft;
			firePoint.position = (Vector3.down + Vector3.left) * 0.16f + transform.position;
			firePoint.rotation = Quaternion.Euler (0f, 0f, -135f);
			firePointLeft.position = firePoint.position;
			firePointLeft.rotation = Quaternion.Euler (0f, 0f, -135f + diff);
			firePointRight.position = firePoint.position;
			firePointRight.rotation = Quaternion.Euler (0f, 0f, -135f - diff);
			firePointLeftToMiddle.position = firePoint.position;
			firePointLeftToMiddle.rotation = Quaternion.Euler (0f, 0f, -135f + diff/2f);
			firePointRightToMiddle.position = firePoint.position;
			firePointRightToMiddle.rotation = Quaternion.Euler (0f, 0f, -135f - diff/2f);
		} else if (rotationZ < -157.5f || rotationZ >= 157.5f) {
			sr.sprite = left;
			firePoint.position = Vector3.left * 0.16f + transform.position;
			firePoint.rotation = Quaternion.Euler (0f, 0f, 180f);
			firePointLeft.position = firePoint.position;
			firePointLeft.rotation = Quaternion.Euler (0f, 0f, -180f + diff);
			firePointRight.position = firePoint.position;
			firePointRight.rotation = Quaternion.Euler (0f, 0f, 180f - diff);
			firePointLeftToMiddle.position = firePoint.position;
			firePointLeftToMiddle.rotation = Quaternion.Euler (0f, 0f, -180f + diff/2f);
			firePointRightToMiddle.position = firePoint.position;
			firePointRightToMiddle.rotation = Quaternion.Euler (0f, 0f, 180f - diff/2f);
		} else if (rotationZ < 157.5f && rotationZ >= 112.5f) {
			sr.sprite = upLeft;
			firePoint.position = (Vector3.left + Vector3.up) * 0.16f + transform.position;
			firePoint.rotation = Quaternion.Euler (0f, 0f, 135f);
			firePointLeft.position = firePoint.position;
			firePointLeft.rotation = Quaternion.Euler (0f, 0f, 135f + diff);
			firePointRight.position = firePoint.position;
			firePointRight.rotation = Quaternion.Euler (0f, 0f, 135f - diff);
			firePointLeftToMiddle.position = firePoint.position;
			firePointLeftToMiddle.rotation = Quaternion.Euler (0f, 0f, 135f + diff/2f);
			firePointRightToMiddle.position = firePoint.position;
			firePointRightToMiddle.rotation = Quaternion.Euler (0f, 0f, 135f - diff/2f);
		} else if (rotationZ < 112.5f && rotationZ >= 67.5f) {
			sr.sprite = up;
			firePoint.position = Vector3.up * 0.16f + transform.position;
			firePoint.rotation = Quaternion.Euler (0f, 0f, 90f);
			firePointLeft.position = firePoint.position;
			firePointLeft.rotation = Quaternion.Euler (0f, 0f, 90f + diff);
			firePointRight.position = firePoint.position;
			firePointRight.rotation = Quaternion.Euler (0f, 0f, 90f - diff);
			firePointLeftToMiddle.position = firePoint.position;
			firePointLeftToMiddle.rotation = Quaternion.Euler (0f, 0f, 90f + diff/2f);
			firePointRightToMiddle.position = firePoint.position;
			firePointRightToMiddle.rotation = Quaternion.Euler (0f, 0f, 90f - diff/2f);
		} else if (rotationZ < 67.5f && rotationZ >= 22.5f) {
			sr.sprite = upRight;
			firePoint.position = (Vector3.up + Vector3.right) * 0.16f + transform.position;
			firePoint.rotation = Quaternion.Euler (0f, 0f, 45f);
			firePointLeft.position = firePoint.position;
			firePointLeft.rotation = Quaternion.Euler (0f, 0f, 45f + diff);
			firePointRight.position = firePoint.position;
			firePointRight.rotation = Quaternion.Euler (0f, 0f, 45f - diff);
			firePointLeftToMiddle.position = firePoint.position;
			firePointLeftToMiddle.rotation = Quaternion.Euler (0f, 0f, 45f + diff/2f);
			firePointRightToMiddle.position = firePoint.position;
			firePointRightToMiddle.rotation = Quaternion.Euler (0f, 0f, 45f - diff/2f);
		}

		Debug.DrawLine (new Vector3(firePoint.position.x + 0.5f, firePoint.position.y), new Vector3(firePoint.position.x - 0.5f, firePoint.position.y));
		Debug.DrawLine (new Vector3(firePoint.position.x, firePoint.position.y + 0.5f), new Vector3(firePoint.position.x, firePoint.position.y - 0.5f));
	}
}
