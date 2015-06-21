using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class StartFlagBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		this.OnTriggerEnterAsObservable()
			.Select (collider => collider.tag == "Character")
			.Subscribe (_ => {
					Debug.Log ("TODO: write camera move");
			});
	}
}
