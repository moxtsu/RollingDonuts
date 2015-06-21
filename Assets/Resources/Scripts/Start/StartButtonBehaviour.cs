using UnityEngine;
using System.Collections;

public class StartButtonBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void OnClickStartButton() {
		Application.LoadLevel("Main");
	}
}
