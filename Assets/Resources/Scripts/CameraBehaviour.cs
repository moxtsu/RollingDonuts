using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class CameraBehaviour : MonoBehaviour {
	public GameObject trackObject;

	// Use this for initialization
	void Start () {
		IObservable<Vector3> positionObservable = Observable.EveryUpdate()
			.Select (_ => { return trackObject.transform.position; });

		positionObservable.Subscribe(pos => { 
			Camera.main.transform.position = new Vector3(pos.x, pos.y, Camera.main.transform.position.z);
		});
	}
}
