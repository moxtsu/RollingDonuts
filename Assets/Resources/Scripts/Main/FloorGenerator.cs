using UnityEngine;
using System.Collections;
using UniRx;
using UniRx.Triggers;

public class FloorGenerator : MonoBehaviour {
	public GameObject trackObject;
	public GameObject floorObject;
	private float floorPositionOffsetY = 0.5f;
	private float floorPositionOffsetX = -0.2f;

	// Use this for initialization
	void Start () {
		Renderer floorRenderer = floorObject.GetComponent<Renderer>();

		GenerateFloor (1, floorRenderer.bounds);
		trackObject.UpdateAsObservable()
			.Select (_ => (int)((trackObject.transform.position.x + 2*floorRenderer.bounds.size.x) / floorRenderer.bounds.size.x))
			.DistinctUntilChanged ()
			.Where (times => times > 1)
			.Subscribe (times => {
					GenerateFloor (times, floorRenderer.bounds);
			});
	} 

	void GenerateFloor(int times, Bounds bounds) {
		Vector3 position = new Vector3(times * (bounds.size.x + floorPositionOffsetX), times * (-bounds.size.y + floorPositionOffsetY), 0);
		Instantiate(floorObject, position, floorObject.transform.rotation);
	}
}
