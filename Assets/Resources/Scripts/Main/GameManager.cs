using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public enum GameScene {
	Start,
	Playing,
	Clear,
	Over,
}

public class GameManager : SingletonMonoBehaviourFast<GameManager> {
	private GameScene scene = GameScene.Start;
	
	public void GameStart(GameObject gameObject, float timeLimit, Slider slider) {
		scene = GameScene.Playing;
		TimeSpan startTime = GetEpoch();
		
		gameObject.UpdateAsObservable()
			.Take(1)
			.Delay(TimeSpan.FromSeconds(timeLimit))
			.Subscribe(_ => {
				if (scene == GameScene.Playing) { GameClear(); }
			});
			
		gameObject.UpdateAsObservable()
			.Select(_ => (GetEpoch() - startTime).TotalSeconds)
			.Where(passedTime => passedTime < timeLimit)
			.Select(passedTime => (float)(passedTime / timeLimit))
			.Subscribe(value => {
				slider.normalizedValue = value;
			});
	}
	
	public void GameOver() {
		if (scene != GameScene.Playing) { return; }
		
		scene = GameScene.Over;
		
		// ちょっと間を置く
		this.gameObject.UpdateAsObservable()
			.Take(1).Delay(TimeSpan.FromSeconds(.5f))
			.Subscribe(_ => {
				Application.LoadLevel ("Over");
			});
	}
	
	public void GameClear() {
		if (scene != GameScene.Playing) { return; }
		
		scene = GameScene.Clear;
		
		// ちょっと間を置く
		this.gameObject.UpdateAsObservable()
			.Take(1).Delay(TimeSpan.FromSeconds(.5f))
			.Subscribe(_ => {
				Application.LoadLevel ("Clear");
			});
	}
	
	private TimeSpan GetEpoch() {
		return DateTime.UtcNow - new DateTime(1970, 1, 1);
	}
}
