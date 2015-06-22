using UnityEngine;
using System;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class CameraBehaviour : MonoBehaviour {
	public GameObject trackObject;

	// Use this for initialization
	void Start () {
		//　物体のを追跡する. 開始と終了の遅延を除く
		IObservable<Vector3> positionObservable = this.gameObject.UpdateAsObservable()
			.Select (_ => { return trackObject.transform.position; })
			.Where(position => position.x >= 0)
			.Where(position => position.x <= GameManager.Instance.GetEndPositionX());

		positionObservable
			.Subscribe(pos => {
				// 揺れ防止 移動はカメラをがたつかせない
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
