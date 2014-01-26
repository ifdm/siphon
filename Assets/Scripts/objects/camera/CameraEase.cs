using UnityEngine;
using System.Collections;

public class CameraEase : MonoBehaviour {

	public float size = 5;
	public float smooth = 3.0f;

	private CameraFollow cameraFollow;
	private Rect triggerRect;

	public void Start() {
		cameraFollow = GameObject.Find("Main Camera").GetComponent<CameraFollow>();

		BoxCollider2D box = GetComponent<BoxCollider2D>();
		Vector2 p = (Vector2) transform.position;
		p -= Vector2.Scale(box.size, transform.lossyScale);
		triggerRect = new Rect(p.x, p.y, box.size.x * transform.lossyScale.x, box.size.y * transform.lossyScale.y);
	}

	public void Update() {
		if(triggerRect.Contains((Vector2)GameObject.Find("Player").transform.position)) {
			cameraFollow.targetSize = size;
			cameraFollow.sizeSmooth = smooth;
		}
		else {
			cameraFollow.targetSize = cameraFollow.defaultSize;
		}

		Debug.DrawLine(new Vector3(triggerRect.xMin, triggerRect.yMin, 0), new Vector3(triggerRect.xMax, triggerRect.yMax, 0), Color.blue);
	}
}
