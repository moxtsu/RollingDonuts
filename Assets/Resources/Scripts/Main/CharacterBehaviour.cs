using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public enum CharacterState : int {
	stay = 0,
	right = 1,
	left = 2,
	miss = 3,
	clear = 4,
}

public class CharacterBehaviour : MonoBehaviour {
	
	public GameObject donutsFloorObject;

	// Use this for initialization
	void Start () {
		Animator characterAnimator = this.GetComponent<Animator>();
		Bounds characterBounds     = this.gameObject.GetComponent<Renderer>().bounds;
		Bounds donutsFloorBounds   = donutsFloorObject.GetComponent<Collider2D>().bounds;
		IObservable<Vector3> characterPositionObservable = this.gameObject.UpdateAsObservable()
			.Select(_ => this.transform.position);
		Vector3 characterFloorOffsetPosition = this.transform.position - donutsFloorObject.transform.position;
		
		// 台座より下の位置にキャラクターが移動したらコテンっていうアニメーション
		IObservable<bool> missObservable = characterPositionObservable
			.Select(position => position.y - donutsFloorObject.transform.position.y)
			.Select(y => y - characterBounds.size.y/2 - donutsFloorBounds.size.y/2)
			.Select(y => y < -0.5); // 微調整必要
			
		missObservable.Subscribe(miss => {
			SetAnimatorState(characterAnimator, miss ? CharacterState.miss : CharacterState.stay);
		});
		
		// キャラが左右に動いてるアニメーション
		IObservable<bool> characterMoveObservable = this.UpdateAsObservable()
			.Where(_ => Input.GetMouseButton(0))
			.Select(_ => Input.mousePosition.x)
			.Select(x => x/Screen.width >= 0.5f)
			.Where(_ => GameManager.Instance.scene == GameScene.Playing);
		
		characterMoveObservable
			.Select(right => right ? CharacterState.right : CharacterState.left)
			.Subscribe(state => SetAnimatorState(characterAnimator, state));
		
		// 床についたらゲームオーバー
		this.gameObject.OnCollisionEnter2DAsObservable()
			.Where(collision => collision.gameObject.tag == "Ground")
			.Subscribe(_ => GameManager.Instance.GameOver());
			
		// はじめはキャラの位置を固定
		characterPositionObservable
			.Where(position => position.x < 0)
			.Select(_ => donutsFloorObject.transform.position + characterFloorOffsetPosition)
			// ガタつくのを抑制
			.Select(newPosition => Vector3.Slerp(this.transform.position, newPosition, 0.1f))
			.Subscribe(newPosition => this.transform.position = newPosition);
	}
	
	void SetAnimatorState(Animator animator, CharacterState state) {
		animator.SetInteger("state", (int)state);
	}
}
