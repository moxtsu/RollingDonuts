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

		positionObservable
			.Subscribe(pos => {
				// a little bit position changed
				if (Vector3.Distance (pos, Camera.main.transform.position) < 1.0f) {
					Vector3 slerp = Vector3.Slerp (Camera.main.transform.position, pos, 0.1f);
					Camera.main.transform.position = new Vector3(pos.x, slerp.y, Camera.main.transform.position.z);
				}
				else {
					Camera.main.transform.position = new Vector3(pos.x, pos.y, Camera.main.transform.position.z);
				}
			});
	}
}
