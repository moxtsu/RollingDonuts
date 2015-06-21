using UnityEngine;
using System;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class GameManager : MonoBehaviour {
	public float timeLimit = 10.0f;
	
	// Use this for initialization
	void Start () {
		this.gameObject.UpdateAsObservable()
			.Take(1)
			.Delay(TimeSpan.FromSeconds(timeLimit))
			.Subscribe(_ => {
				GameClear();
			});
	}
	
	public void GameOver() {
		Application.LoadLevel ("Over");
	}
	
	public void GameClear() {
		Application.LoadLevel ("Clear");
	}
}
