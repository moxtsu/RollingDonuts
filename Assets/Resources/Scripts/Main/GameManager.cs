using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class GameManager : MonoBehaviour {
	public float timeLimit = 10.0f;
	public Slider slider;
	
	// Use this for initialization
	void Start () {
		TimeSpan startTime = GetEpoch();
		
		this.gameObject.UpdateAsObservable()
			.Take(1)
			.Delay(TimeSpan.FromSeconds(timeLimit))
			.Subscribe(_ => {
				GameClear();
			});
			
		this.gameObject.UpdateAsObservable()
			.Select(_ => (GetEpoch() - startTime).TotalSeconds)
			.Where(passedTime => passedTime < timeLimit)
			.Select(passedTime => (float)(passedTime / timeLimit))
			.Subscribe(value => {
				slider.normalizedValue = value;
			});
	}
	
	public void GameOver() {
		Application.LoadLevel ("Over");
	}
	
	public void GameClear() {
		Application.LoadLevel ("Clear");
	}
	
	private TimeSpan GetEpoch() {
		return DateTime.UtcNow - new DateTime(1970, 1, 1);
	}
}
