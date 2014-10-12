using UnityEngine;
using System.Collections;
using RTS;

public class CameraControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		MoveCamera();

	}



	private void MoveCamera() {
		float xpos = Input.mousePosition.x;
		float ypos = Input.mousePosition.y;

		//Keyboard Scroll
		float moveHoriz = Input.GetAxis ("Horizontal");
		float moveVert = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (0, 0, 0);

		if((xpos >= 0 && xpos <= ResourceManager.ScrollWidth) || moveHoriz < 0){
			movement.x -= ResourceManager.ScrollSpeed;
		}

		if((xpos <= Screen.width && xpos >= Screen.width - ResourceManager.ScrollWidth) || moveHoriz > 0) {
			movement.x += ResourceManager.ScrollSpeed;
		}
		
		//vertical camera movement
		if((ypos >= 0 && ypos < ResourceManager.ScrollWidth) || moveVert < 0 ) {
			movement.y -= ResourceManager.ScrollSpeed;
		} if((ypos <= Screen.height && ypos > Screen.height - ResourceManager.ScrollWidth)|| moveVert > 0 ) {
			movement.y += ResourceManager.ScrollSpeed;
		}

		movement = Camera.mainCamera.transform.TransformDirection(movement);
		movement.z = -10;

		Vector3 origin = Camera.mainCamera.transform.position;
		Vector3 destination = origin;
		destination.x += movement.x;
		destination.y += movement.y;
		destination.z = -10f;


		if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
		{
			Camera.main.orthographicSize--;
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
		{
			Camera.main.orthographicSize++;
		}
		
		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, ResourceManager.ZoomMin, ResourceManager.ZoomMax );


		if(destination != origin) {
			Camera.mainCamera.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.ScrollSpeed);
		}
	}
	

}
