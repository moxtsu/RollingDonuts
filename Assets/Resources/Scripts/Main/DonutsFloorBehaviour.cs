using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class DonutsFloorBehaviour : MonoBehaviour {
	public GameObject donutsObject;

	// Use this for initialization
	void Start () {
		Vector3 offsetPosition = this.transform.position - donutsObject.transform.position;
		
		this.UpdateAsObservable()
			.Select (_ => donutsObject.transform.position)
			.Subscribe (donutsPosition => this.transform.position = donutsPosition + offsetPosition);
	}
}
