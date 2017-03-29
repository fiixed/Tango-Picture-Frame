using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PictureFrameUIController : MonoBehaviour {

	public GameObject m_pictureFrame;
	private TangoPointCloud m_pointCloud;

	void Start() {
		m_pointCloud = FindObjectOfType<TangoPointCloud>();
	}


	void Update() {
		if (Input.touchCount == 1) {
			// Trigger placepictureframe function when single touch ended.
			Touch t = Input.GetTouch(0);
			if (t.phase == TouchPhase.Ended) {
				PlacePictureFrame(t.position);
			}
		}
	}

	void PlacePictureFrame(Vector2 touchPosition) {
		// Find the plane.
		Camera cam = Camera.main;
		Vector3 planeCenter;
		Plane plane;
		if (!m_pointCloud.FindPlane(cam, touchPosition, out planeCenter, out plane)) {
			Debug.Log("cannot find plane.");
			return;
		}

		// Place picture frame on the surface, and make it always face the camera.
		if (Vector3.Angle(plane.normal, Vector3.up) > 60.0f) {
			Vector3 up = plane.normal;
			Vector3 right = Vector3.Cross(plane.normal, cam.transform.forward).normalized;
			Vector3 forward = Vector3.Cross(right, plane.normal).normalized;
			Instantiate(m_pictureFrame, planeCenter, Quaternion.LookRotation(forward, up));
		} else {
			Debug.Log("surface is not steep enough for picture frame to be placed on.");
		}
	}
}
