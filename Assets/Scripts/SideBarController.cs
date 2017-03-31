using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideBarController : MonoBehaviour {

	public Image m_selectionIndicator;
	public Button m_selectedButton = null;
	public GameObject m_frameObject1;
	public GameObject m_frameObject2;
	public GameObject m_frameObject3;
	public GameObject m_frameObject4;
	public GameObject m_frameObject5;
	public GameObject m_frameObject6;
	//	public GameObject m_cubeObject;
	//	public GameObject m_chairObject;
	//	public bool passTheBallObject;

	public void SelectObject (Button selectedButton) {
		m_selectedButton = selectedButton;
		m_selectionIndicator.transform.position = new Vector3(m_selectionIndicator.transform.position.x, selectedButton.transform.position.y, 0);
		m_selectionIndicator.enabled = true;

		// this is going to be A
		PictureFrameUIController pictureFrameUIController = GetComponent<PictureFrameUIController>();

//		pictureFrameUIController.Reset ();

		Debug.Log("Button Name: "+selectedButton.name);
		if (selectedButton.name == "Button1") {
			pictureFrameUIController.m_pictureFrame = m_frameObject1;
		} else if (selectedButton.name == "Button2") {
			pictureFrameUIController.m_pictureFrame = m_frameObject2;
		} else if (selectedButton.name == "Button3") {
			pictureFrameUIController.m_pictureFrame = m_frameObject3;
		} else if (selectedButton.name == "Button4") {
			pictureFrameUIController.m_pictureFrame = m_frameObject4;
		} else if (selectedButton.name == "Button5") {
			pictureFrameUIController.m_pictureFrame = m_frameObject5;
		} else {
			pictureFrameUIController.m_pictureFrame = m_frameObject6;
		}
	}

	void Update(){
		if(m_selectedButton != null){ 
			m_selectionIndicator.transform.position = new Vector3(m_selectionIndicator.transform.position.x, m_selectedButton.transform.position.y, 0);
		}
	}


}
