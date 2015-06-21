using UnityEngine;
using System.Collections;

public class ClearButtonBehaviour : MonoBehaviour {

	public void OnClickClearButton() {
		Application.LoadLevel ("StartGame");
	}

}
