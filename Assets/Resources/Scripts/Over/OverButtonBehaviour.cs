using UnityEngine;
using System.Collections;

public class OverButtonBehaviour : MonoBehaviour {
	
	public void OnClickOverButton() {
		Application.LoadLevel ("StartGame");
	}
	
}
