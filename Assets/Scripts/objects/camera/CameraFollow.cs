using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
	
	public float smooth = 0.5f;
	[HideInInspector] public GameObject player;
	[HideInInspector] public float additionalZ = 0;
	[HideInInspector] public float zSmooth = .5f;

	private float z;
	private float zVel = 0;
	private Vector3 posVel = Vector3.zero;

	void Start() {
		player = GameObject.Find("Player");
		z = transform.position.z;
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, z);

		camera.transparencySortMode = TransparencySortMode.Orthographic;
	}

	void OnPreRender() {
		float s = smooth;
		if(Mathf.Abs(player.rigidbody2D.velocity.y) > 20) {
			s = smooth * (1 - Mathf.Clamp((Mathf.Abs(player.rigidbody2D.velocity.y) - 20) / 20, 0.0f, 0.75f));
			z = Mathf.SmoothDamp(z, -20 - additionalZ, ref zVel, 0.3f);
			zSmooth = smooth;
		}
		else {
			z = Mathf.SmoothDamp(z, -10 - additionalZ, ref zVel, zSmooth);
		}

		Vector2 p2d = (Vector2)player.transform.position + Vector2.Scale(player.GetComponent<BoxCollider2D>().center, (Vector2)player.transform.lossyScale);
		Vector3 target = new Vector3(p2d.x, p2d.y, z);

		transform.position = Vector3.SmoothDamp(transform.position, target, ref posVel, s);

		CameraEase.dirty = false;
	}
}
