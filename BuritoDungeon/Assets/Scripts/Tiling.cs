using UnityEngine;
using System.Collections;

public class Tiling : MonoBehaviour {

	public int offset = 2; 						//The offset
	public bool hasARightBuddy = false;			//Do we need to instantiate stuff
	public bool hasALeftBuddy = false;			//Do we need to instantiate stuff

	public bool reverseScale = false;			//Used if an object is no tilable

	private float spriteWidth = 0f;				//The width of one element
	private Camera cam;
	private Transform myTransform;

	void Awake () {
		cam = Camera.main;
		myTransform = transform;
	}

	// Use this for initialization
	void Start () {
		SpriteRenderer sRenderer = GetComponent<SpriteRenderer> ();
		spriteWidth = sRenderer.bounds.size.x;
	}
	
	// Update is called once per frame
	void Update () {
		//Does it still need buddies? IF not - do nothing
		if (!hasALeftBuddy || !hasARightBuddy) {
			//calculating camera's extend (half the width) of what the camera can see in world coordinates
			float camHorizontalExtend = cam.orthographicSize * Screen.width / Screen.height;

			//calculate the x position where the camera can see the edge of the sprite
			float edgeVisiblePositionRight = (myTransform.position.x + spriteWidth/2f) - camHorizontalExtend;
			float edgeVisiblePositionLeft = (myTransform.position.x - spriteWidth/2f) +	 camHorizontalExtend;

			//checking if we can see the edge of the element and the calling MakeNewBuddy if we can
			if (cam.transform.position.x >= edgeVisiblePositionRight - offset && !hasARightBuddy) {
				MakeNewBuddy (1);
				hasARightBuddy = true;
			} else if (cam.transform.position.x <= edgeVisiblePositionLeft - offset && !hasALeftBuddy) {
				MakeNewBuddy (-1);
				hasALeftBuddy = true;
			}
		}
	}

	//a method that creates a new buddy on the side required
	void MakeNewBuddy (int rightOrLeft) { 				//right = -1; left = 1
		//calculating new position for the new buddy
		Vector3 newPosition = new Vector3(myTransform.position.x + spriteWidth * rightOrLeft, myTransform.position.y, myTransform.position.z);

		//instantiating new buddy into the 'newBuddy' variable
		Transform newBuddy = Instantiate (myTransform, newPosition, myTransform.rotation) as Transform;

		//if object is not tilable we revert the localScale.x value
		if (reverseScale) {
			newBuddy.localScale = new Vector3 (newBuddy.localScale.x * (-1), newBuddy.localScale.y, newBuddy.localScale.z);
		}

		newBuddy.parent = myTransform.parent;
		if (rightOrLeft > 0) {
			newBuddy.GetComponent<Tiling> ().hasALeftBuddy = true;
		} else {
			newBuddy.GetComponent<Tiling> ().hasARightBuddy = true;
		}
	}
}
