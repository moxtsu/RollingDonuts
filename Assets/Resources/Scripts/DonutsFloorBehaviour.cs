using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class DonutsFloorBehaviour : MonoBehaviour {
	public GameObject donutsObject;

	// Use this for initialization
	void Start () {
		this.UpdateAsObservable()
			.Select (_ => donutsObject.transform.position)
			.Subscribe (pos => {
				this.transform.position = new Vector3(pos.x, pos.y+0.7f, this.transform.position.z);
			});
	}
}
