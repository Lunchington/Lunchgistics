using UnityEngine;
using System.Collections;

public class UserInput : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0))
			LeftClick ();
		if (Input.GetMouseButtonDown (1))
			RightClick ();
	}

	void LeftClick() {
		{
			RaycastHit2D hitInfo = Physics2D.Raycast (Camera.main.ScreenToWorldPoint (Input.mousePosition), Vector2.zero);
			if (hitInfo.collider != null) {
				Debug.Log ("Target Position: " + hitInfo.collider.gameObject.transform.position);						}
		}
	}

	void RightClick() {}
}
