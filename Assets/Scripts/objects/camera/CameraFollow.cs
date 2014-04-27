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
	[HideInInspector] public float shakeStrength = 0;
	[HideInInspector] public Color targetAmbient;
	[HideInInspector] public float ambientSmooth = 5;

	private float z;
	public float zStart;
	private float zVel = 0;
	private Vector3 posVel = Vector3.zero;
	private Vector3 shakeVel = Vector3.zero;

	void Start() {
		player = GameObject.Find("Player");
		z = transform.position.z;
		zStart = z;
		transform.position = new Vector3(player.transform.position.x, player.transform.position.y, z);
		shake = 0;
		shakeStrength = 0;
		targetAmbient = RenderSettings.ambientLight;

		camera.transparencySortMode = TransparencySortMode.Orthographic;
	}

	void Update() {
		shake -= Mathf.Min(shake, Time.deltaTime);
	}

	void OnPreRender() {
		float s = smooth;
		if(Mathf.Abs(player.rigidbody2D.velocity.y) > 20) {
			s = smooth * (1 - Mathf.Clamp((Mathf.Abs(player.rigidbody2D.velocity.y) - 20) / 20, 0.0f, 0.75f));
			z = Mathf.SmoothDamp(z, Mathf.Min(-20, zStart - additionalZ), ref zVel, 0.3f);
			zSmooth = smooth;
		}
		else {
			if(additionalZ > 0 && z < zStart - additionalZ){zSmooth = 0.3f;}
			z = Mathf.SmoothDamp(z, zStart - additionalZ, ref zVel, zSmooth);
		}

		Vector2 p2d;
		Vector3 target;
		if(pullTo == Vector3.zero) {
			p2d = (Vector2)player.transform.position + Vector2.Scale(player.GetComponent<BoxCollider2D>().center, (Vector2)player.transform.lossyScale);
			target = new Vector3(p2d.x, p2d.y + 2.0f, z);
			if(Mathf.Abs(pullSmooth - smooth) > .0001){
				s = pullSmooth;
				pullSmooth = Mathf.Lerp(pullSmooth, smooth, 4 * Time.deltaTime);
			}
		}
		else {
			target = pullTo;
			s = pullSmooth;
		}

		transform.position = Vector3.SmoothDamp(transform.position, target, ref posVel, s);

		if(shake > 0) {
			target = new Vector3(transform.position.x + Random.Range(-shakeStrength, shakeStrength), transform.position.y + Random.Range(-shakeStrength, shakeStrength), transform.position.z + Random.Range(-0, 0));
			transform.position = Vector3.SmoothDamp(transform.position, target, ref shakeVel, smooth);
		}

		CameraZoom.dirty = false;

		RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, targetAmbient, Mathf.Clamp(ambientSmooth * Time.deltaTime, 0, 1));
	}
}
