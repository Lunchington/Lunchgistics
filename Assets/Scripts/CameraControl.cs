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
			movement.x -= ScrollSpeed();
		}

		if((xpos <= Screen.width && xpos >= Screen.width - ResourceManager.ScrollWidth) || moveHoriz > 0) {
			movement.x += ScrollSpeed();
		}
		
		//vertical camera movement
		if((ypos >= 0 && ypos < ResourceManager.ScrollWidth) || moveVert < 0 ) {
			movement.y -= ScrollSpeed();

		} if((ypos <= Screen.height && ypos > Screen.height - ResourceManager.ScrollWidth)|| moveVert > 0 ) {
			movement.y += ScrollSpeed();
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


	private float ScrollSpeed() {
		return Camera.main.orthographicSize * ResourceManager.ScrollSpeed;
	}
	private void ScrollCamera() {

		if (Input.GetAxis("Mouse ScrollWheel") > 0 && camera.orthographicSize > ResourceManager.ZoomMin) // forward
		{
			ZoomOrthoCamera(Camera.main.ScreenToWorldPoint(Input.mousePosition), -0.5f);

		}
		if (Input.GetAxis("Mouse ScrollWheel") < 0  && camera.orthographicSize < ResourceManager.ZoomMax) // back
		{
			ZoomOrthoCamera(Camera.main.ScreenToWorldPoint(Input.mousePosition), 0.5f);
		}

		Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, ResourceManager.ZoomMin, ResourceManager.ZoomMax );
		ClampCam();
	}

	void ZoomOrthoCamera(Vector3 zoomTowards, float amount)
	{

		
		Camera.main.orthographicSize += amount;
		
	}

	private void ClampCam() {

		float minX = Extents().x + map.bounds.min.x ;
		float maxX = map.bounds.max.x -Extents().x ;

	
		float minY = Extents().y + map.bounds.min.y;
		float maxY = map.bounds.max.y - Extents().y;


		Vector3 camPosNew = Vector3.zero;

		camPosNew.x = Mathf.Clamp (Camera.main.transform.position.x, minX, maxX);
		camPosNew.y = Mathf.Clamp (Camera.main.transform.position.y, minY, maxY);
		camPosNew.z = ResourceManager.zLevelDefault;

		Camera.main.transform.position = camPosNew;

	}

	public static Vector2 Extents()
	{
			return new Vector2(Camera.main.orthographicSize * Screen.width/Screen.height, Camera.main.orthographicSize);

	}

}
