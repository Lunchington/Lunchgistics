using UnityEngine;
using System.Collections;
using RTS;

public class CameraControl : MonoBehaviour {

	private MeshRenderer map;
	private Transform mapPos;


	// Use this for initialization
	void Start () {
		GameObject m = GameObject.FindGameObjectWithTag ("Map");

		map = m.GetComponentInChildren<MeshRenderer>();
		mapPos = m.transform;
	}
	
	// Update is called once per frame
	void Update () {
		MoveCamera();
		ScrollCamera();

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




		if(destination != origin) {
			Camera.mainCamera.transform.position = Vector3.MoveTowards(origin, destination, Time.deltaTime * ResourceManager.ScrollSpeed);
		}

		ClampCam();

	}

	private void ScrollCamera() {

		if (Input.GetAxis("Mouse ScrollWheel") > 0) // forward
		{
			Camera.main.orthographicSize--;
		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0) // back
		{
			Camera.main.orthographicSize++;
		}

		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, ResourceManager.ZoomMin, ResourceManager.ZoomMax );
		ClampCam();
	}

	private void ClampCam() {

		float minX = Extents().x + map.bounds.min.x ;
		float maxX = map.bounds.max.x -Extents().x ;

	
		float minY = Extents().y + map.bounds.min.y;
		float maxY = map.bounds.max.y - Extents().y;


		Vector3 camPosNew = Vector3.zero;

		camPosNew.x = Mathf.Clamp (Camera.main.transform.position.x, minX, maxX);
		camPosNew.y = Mathf.Clamp (Camera.main.transform.position.y, minY, maxY);
		camPosNew.z = -10f;

		Camera.main.transform.position = camPosNew;
		Debug.Log (Extents().x + " " + maxX + " " + map.bounds.max.x + " " + camPosNew);

	}

	public static Vector2 Extents()
	{
			return new Vector2(Camera.main.orthographicSize * Screen.width/Screen.height, Camera.main.orthographicSize);

	}

}
