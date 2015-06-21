using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class CharacterBehaviour : MonoBehaviour {
	public GameObject donutsFloorObject;

	// Use this for initialization
	void Start () {
		// 台座より下の位置にキャラクターが移動したらコテンっていうアニメーション
		this.gameObject.UpdateAsObservable()
			.Select(_ => this.transform.position - donutsFloorObject.transform.position)
			.Select(diff => diff.y < 0)
			.Subscribe(_ => {
				// TODO: animation stateを変更
			});
			
		// 床についたらゲームオーバー
		this.gameObject.OnCollisionEnter2DAsObservable()
			.Where(collision => collision.gameObject.tag == "Ground")
			.Subscribe(_ => {
				GameManager.Instance.GameOver();
			});
	}
}
