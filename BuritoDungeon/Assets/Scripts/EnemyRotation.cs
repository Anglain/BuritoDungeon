using UnityEngine;
using System.Collections;

public class EnemyRotation : MonoBehaviour {

	/// <summary>
	/// The object will change sprites in 8 directions according to the mouse position
	/// Attach to the object, which is the child of the required object
	/// </summary>

	public Sprite up;
	public Sprite right;
	public Sprite down;
	public Sprite left;
	public Sprite upLeft;
	public Sprite upRight;
	public Sprite downRight;
	public Sprite downLeft;

	private SpriteRenderer sr;
	private EnemyAI enemy;

	void Awake () {
		sr = GetComponent<SpriteRenderer> ();

		if (sr == null) {
			Debug.LogError ("No SpriteRenderer component found on the Enemy!");
		}

		enemy = GetComponent<EnemyAI> ();

		if (enemy == null) {
			Debug.LogError ("No EnemyAI component found!");
		}
	}

	void Start () {
		StartCoroutine (ChangeSprite ());
	}

	IEnumerator ChangeSprite () {
		yield return new WaitForSeconds (0.1f);

		float rotationZ = Mathf.Atan2 (enemy.direction.y, enemy.direction.x) * Mathf.Rad2Deg; //find the angle in degrees

		if (rotationZ < 22.5f && rotationZ >= -22.5f) {
			sr.sprite = right;
		} else if (rotationZ < -22.5f && rotationZ >= -67.5f) {
			sr.sprite = downRight;
		} else if (rotationZ < -67.5f && rotationZ >= -112.5f) {
			sr.sprite = down;
		} else if (rotationZ < -112.5f && rotationZ >= -157.5f) {
			sr.sprite = downLeft;
		} else if (rotationZ < -157.5f || rotationZ >= 157.5f) {
			sr.sprite = left;
		} else if (rotationZ < 157.5f && rotationZ >= 112.5f) {
			sr.sprite = upLeft;
		} else if (rotationZ < 112.5f && rotationZ >= 67.5f) {
			sr.sprite = up;
		} else if (rotationZ < 67.5f && rotationZ >= 22.5f) {
			sr.sprite = upRight;
		}

		StartCoroutine (ChangeSprite ());
	}
}
