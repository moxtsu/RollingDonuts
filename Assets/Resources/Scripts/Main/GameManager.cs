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
	public GameScene scene = GameScene.Start;
	
	private float endPositionX = 0.0f;
	
	private float endDelayTime = 1.0f; // Clear/Miss時の終了遅延時間
	
	public void GamePlay(IObservable<Vector3> characterPositionObservable, float endPositionX, Slider slider) {
		this.scene        = GameScene.Playing;
		this.endPositionX = endPositionX;
		
		characterPositionObservable
			.Where(position => position.x > endPositionX)
			.Where(_ => scene == GameScene.Playing)
			.Subscribe(_ => GameClear());
		
		characterPositionObservable
			.Select(position => (float)(position.x / endPositionX))
			.Where(ratio => ratio <= 1.0f)
			.Subscribe(ratio => slider.normalizedValue = ratio);
	}
	
	public void GameOver() {
		if (scene != GameScene.Playing) { return; }
		
		scene = GameScene.Over;
		
		// ちょっと間を置く
		this.gameObject.UpdateAsObservable()
			.First()
			.Delay(TimeSpan.FromSeconds(endDelayTime))
			.Subscribe(_ => Application.LoadLevel ("Over"));
	}
	
	public void GameClear() {
		if (scene != GameScene.Playing) { return; }
		
		scene = GameScene.Clear;
		
		// ちょっと間を置く
		this.gameObject.UpdateAsObservable()
			.First()
			.Delay(TimeSpan.FromSeconds(endDelayTime))
			.Subscribe(_ => Application.LoadLevel ("Clear"));
	}
	
	public void GameStart() {
		scene = GameScene.Start;
		
		Application.LoadLevel("Start");
	}
	
	public float GetEndPositionX() {
		return this.endPositionX;
	}
	
	private TimeSpan GetEpoch() {
		return DateTime.UtcNow - new DateTime(1970, 1, 1);
	}
	
}
