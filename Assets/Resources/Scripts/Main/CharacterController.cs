using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class CharacterController : MonoBehaviour {
	private float speed = 0.1f;

	// Use this for initialization
	void Start () {
		Rigidbody2D rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
		IObservable<bool> characterMoveObservable = this.UpdateAsObservable()
			.Where(_ => Input.GetMouseButton(0))
			.Select(_ => Input.mousePosition.x)
			.Select(x => x/Screen.width >= 0.5f);
			
		characterMoveObservable
			.Subscribe(right => {
				rigidbody.velocity = (right)
					? rigidbody.velocity + new Vector2(2.0f, -0.6f).normalized * speed
					: rigidbody.velocity + new Vector2(2.0f, -0.6f).normalized * -speed;
			});
	}
}