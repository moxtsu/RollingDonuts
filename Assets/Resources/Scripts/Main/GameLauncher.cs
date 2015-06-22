using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class GameLauncher : MonoBehaviour {
	public float endPositionX = 100.0f; // ゲームの終了位置
	public GameObject characterObject;
	public Slider slider;
	
	// Use this for initialization
	void Start () {
		GameManager.Instance.GamePlay(
			characterObject.UpdateAsObservable().Select(_ => characterObject.transform.position),
			endPositionX,
			slider);
	}
}