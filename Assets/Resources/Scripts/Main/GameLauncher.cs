using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameLauncher : MonoBehaviour {
	public float timeLimit = 10.0f;
	public Slider slider;
	
	// Use this for initialization
	void Start () {
		GameManager.Instance.GameStart(this.gameObject, timeLimit, slider);
	}
}