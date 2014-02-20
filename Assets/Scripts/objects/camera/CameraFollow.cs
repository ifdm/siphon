using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	public float smooth = 0.5f;
	[HideInInspector] public GameObject player;
	[HideInInspector] public float additionalZ = 0;
	[HideInInspector] public float zSmooth = .5f;
	[HideInInspector] public Vector3 pullTo = Vector3.zero;
	[HideInInspector] public float pullSmooth;
	[HideInInspector] public float shake = 0;

	private float z;
	public float zStart;
	private float zVel = 0;
	private Vector3 posVel = Vector3.zero;

	void Start() {
		player = GameObject.Find("Player");
		z = transform.position.z;
		zStart = z;
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, z);
		shake = 0;

		camera.transparencySortMode = TransparencySortMode.Orthographic;
	}

	void Update() {
		shake -= Mathf.Min(shake, Time.deltaTime);
	}

	void OnPreRender() {
		float s = smooth;
		if(Mathf.Abs(player.rigidbody2D.velocity.y) > 20) {
			s = smooth * (1 - Mathf.Clamp((Mathf.Abs(player.rigidbody2D.velocity.y) - 20) / 20, 0.0f, 0.75f));
			z = Mathf.SmoothDamp(z, -20 - additionalZ, ref zVel, 0.3f);
			zSmooth = smooth;
		}
		else {
			z = Mathf.SmoothDamp(z, zStart - additionalZ, ref zVel, zSmooth);
		}

		Vector2 p2d;
		Vector3 target;
		if(pullTo == Vector3.zero) {
			p2d = (Vector2)player.transform.position + Vector2.Scale(player.GetComponent<BoxCollider2D>().center, (Vector2)player.transform.lossyScale);
			target = new Vector3(p2d.x, p2d.y + 2.0f, z);
		}
		else {
			target = pullTo;
			s = pullSmooth;
		}

		if(shake > 0) {
			target = new Vector3(target.x + Random.Range(-8, 8), target.y + Random.Range(-8, 8), target.z + Random.Range(-8, 8));
		}

		transform.position = Vector3.SmoothDamp(transform.position, target, ref posVel, s);

		CameraZoom.dirty = false;
	}
}
