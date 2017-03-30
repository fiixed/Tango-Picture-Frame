using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPlacementController : MonoBehaviour {
	public GameObject m_selectedObject;
	public GameObject m_planeGO;
	public GameObject m_instPlaneGO = null;
	private BoxCollider m_planeCollider;
	private TangoPointCloud m_pointCloud;
	private bool weHaveGround = false;
	private bool isMovingObject = false;
	private GameObject m_instObject = null;
	private List<GameObject> list = new List<GameObject>();
	private GameObject m_touchedObject = null;

	void Start()
	{
		m_pointCloud = FindObjectOfType<TangoPointCloud>();
	}

	void Update ()
	{
		if (Input.touchCount == 1)
		{
			Touch t = Input.GetTouch(0);
			Ray ray = Camera.main.ScreenPointToRay(t.position);
			RaycastHit hit;


			if (Physics.Raycast (ray, out hit, 100f)) {
				if (hit.transform.gameObject.name != m_instPlaneGO.name) {
					isMovingObject = true;



					if (m_selectedObject.name == "Sphere" || m_selectedObject.name == "Cube") {
						isMovingObject = false;
					}
							
//					m_instObject = 
					m_touchedObject = hit.transform.gameObject;
					m_touchedObject.transform.GetChild(1).gameObject.SetActive(true);
//					GameObject BottomMarker = GameObject.Find ("Bottom Marker");
//					BottomMarker.SetActive (true);
				}
			} 

			if (isMovingObject) {
				if (t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary) {
					MoveObjectToPosition (t.position);
				}
				if (t.phase == TouchPhase.Ended) {
					isMovingObject = false;

					m_touchedObject.transform.GetChild(1).gameObject.SetActive(false);
//					GameObject BottomMarker = GameObject.Find ("Bottom Marker");
//					BottomMarker.SetActive (false);
				}

			} else {
				// Trigger place object function when single touch ended.
				if (t.phase == TouchPhase.Ended) {
					PlaceObject (t.position);
				}
			}
		}

		if (Input.touchCount == 2) {
			float rotX = Input.GetAxis("Mouse X") * 5 * Mathf.Deg2Rad;
			m_instObject.transform.RotateAround(Vector3.up, -rotX);
			m_instObject.transform.GetChild(1).gameObject.SetActive(true);

			if (Input.GetTouch (1).phase == TouchPhase.Ended) {
				m_instObject.transform.GetChild (1).gameObject.SetActive (false);
			}
		}
	}

	void PlaceObject(Vector2 touchPosition)
	{
		// Find the plane.
		Camera cam = Camera.main;
		Vector3 planeCenter;
		Plane plane;
		if (!m_pointCloud.FindPlane(cam, touchPosition, out planeCenter, out plane))
		{
			Debug.Log("cannot find plane.");
			return;
		}

		// Place object on the surface, and make it always face the camera.
		if (Vector3.Angle (plane.normal, Vector3.up) < 30.0f && m_instObject == null) {
			if (!weHaveGround) {
				weHaveGround = true;
				m_planeGO.transform.position = planeCenter;
				m_instPlaneGO = Instantiate (m_planeGO, planeCenter, Quaternion.identity);
				m_instPlaneGO.name = "MyPlane";
			}

			Vector3 up = plane.normal;
			Vector3 right = Vector3.Cross (plane.normal, cam.transform.forward).normalized;
			Vector3 forward = Vector3.Cross (right, plane.normal).normalized;
			Vector3 dropPos = new Vector3 (planeCenter.x, planeCenter.y + 2, planeCenter.z);
			m_instObject = Instantiate (m_selectedObject, dropPos, Quaternion.LookRotation (forward, up));
			if (m_selectedObject.name == "Sphere" || m_selectedObject.name == "Cube") {
				list.Add (m_instObject);
				m_instObject = null;
			}

		}
		else
		{
			Debug.Log("surface is too steep for object to stand on.");
		}
	}

	void MoveObjectToPosition(Vector2 touchPosition)
	{
		// Find the plane.
		Camera cam = Camera.main;
		Vector3 planeCenter;
		Plane plane;
		if (!m_pointCloud.FindPlane(cam, touchPosition, out planeCenter, out plane))
		{
			Debug.Log("cannot find plane.");
			return;
		}

		// Place object on the surface, and make it always face the camera.
		if (Vector3.Angle (plane.normal, Vector3.up) < 30.0f) {

			Vector3 up = plane.normal;
			Vector3 right = Vector3.Cross (plane.normal, cam.transform.forward).normalized;
			Vector3 forward = Vector3.Cross (right, plane.normal).normalized;
			Vector3 dropPos = new Vector3 (planeCenter.x, planeCenter.y, planeCenter.z);
			m_touchedObject.transform.position = dropPos;

		}
		else
		{
			Debug.Log("surface is too steep for object to stand on.");
		}
	}

	public void Reset(){
		if (m_selectedObject.name == "Sphere" || m_selectedObject.name == "Cube") {
			return;
		}
		m_touchedObject = null;
		isMovingObject = false;
		Destroy (m_instObject);
		if (list.Count > 0) {
			foreach (GameObject obj in list) {
				Destroy (obj);
			}
		}
	}
}