using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SideBarController : MonoBehaviour {

	public Image m_selectionIndicator;
	public Button m_selectedButton = null;
	public GameObject m_frameObject;
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
		if (selectedButton.name == "Button (5)") {
			pictureFrameUIController.m_pictureFrame = m_frameObject;
		}else if (selectedButton.name == "Button (4)") {
			pictureFrameUIController.m_pictureFrame = m_frameObject;
		} else {
			pictureFrameUIController.m_pictureFrame = m_frameObject;
		}
	}

	void Update(){
		if(m_selectedButton != null){ 
			m_selectionIndicator.transform.position = new Vector3(m_selectionIndicator.transform.position.x, m_selectedButton.transform.position.y, 0);
		}
	}


}
