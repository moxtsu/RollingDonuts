using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class SpeedLimitBehaviour : MonoBehaviour {
	public float maxSpeed = 10.0f;

	// Use this for initialization
	void Start () {
		Rigidbody2D rigidbody = this.gameObject.GetComponent<Rigidbody2D>();
		this.gameObject.UpdateAsObservable()
			.Where (_ => rigidbody.velocity.magnitude > maxSpeed)
			.Subscribe (_ => {
				rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
			});
	}
}
