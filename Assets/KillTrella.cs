﻿using UnityEngine;
using System.Collections;

public class KillTrella : MonoBehaviour {

	float zoomTo = 300;
	float zoomSpeed = 4;
	float fadeSpeed = .05f;

	void OnTriggerEnter2D(Collider2D col) {
		if(col.gameObject.name == "Player") {
			//col.rigidbody2D.isKinematic = true;
			col.rigidbody2D.drag = 4;
			col.rigidbody2D.gravityScale = .8f;
			col.GetComponent<PlayerPhysics>().maxSpeed = 0;
			StartCoroutine(ZoomOut());
		}
	}

	IEnumerator ZoomOut() {
		GameObject camera = GameObject.Find("Main Camera");
		GameObject player = GameObject.Find("Player");
		GameObject sun = GameObject.Find("The Sun");
		GameObject logo = GameObject.Find("siphonLogoCredits");
		CameraFollow follow = camera.GetComponent<CameraFollow>();
		follow.pullTo = new Vector3(player.transform.position.x, player.transform.position.y, camera.transform.position.z);
		follow.pullSmooth = .5f;
		follow.ambientSmooth = 0;
		camera.camera.backgroundColor = Color.black;
		float targetY = player.transform.position.y - 50f;
		float logoBias = 0;
		while(camera.transform.position.z > -zoomTo) {
			follow.pullTo = new Vector3(player.transform.position.x, player.transform.position.y, follow.pullTo.z);
			follow.pullTo = Vector3.Lerp(follow.pullTo, new Vector3(logo.transform.position.x, logo.transform.position.y, follow.pullTo.z), logoBias);
			follow.pullTo -= Vector3.forward * Time.deltaTime * zoomSpeed;
			if(player.transform.position.y < targetY){player.transform.position = new Vector3(player.transform.position.x, targetY, player.transform.position.z);}
			RenderSettings.ambientLight = Color.Lerp(RenderSettings.ambientLight, Color.black, Time.deltaTime * fadeSpeed);
			sun.light.intensity = Mathf.Lerp(sun.light.intensity, 0, Time.deltaTime * fadeSpeed);
			zoomSpeed += Time.deltaTime * zoomSpeed * .02f;
			logoBias += Time.deltaTime * .1f;
			yield return new WaitForSeconds(0);
		}

		yield return new WaitForSeconds(2.5f);

		GameObject credits = GameObject.Find("siphonLogoCreditsTree");
		GameObject fadeIn1 = GameObject.Find("CreditTreeBaseFadeIn");
		GameObject fadeIn2 = GameObject.Find("CreditTreeBase2FadeIn");
		GameObject fadeIn3 = GameObject.Find("CreditTreeGroundFadeIn");
		Color c = new Color(1, 1, 1, 0);
		fadeIn1.renderer.material.color = c;
		fadeIn2.renderer.material.color = c;
		fadeIn3.renderer.material.color = c;
		camera.transform.position = new Vector3(credits.transform.position.x, credits.transform.position.y, credits.transform.position.z - 11);
		follow.pullTo = camera.transform.position;
		while(c.a < 1) {
			c = new Color(c.r, c.g, c.b, c.a + Time.deltaTime / 2);
			fadeIn1.renderer.material.color = c;
			fadeIn2.renderer.material.color = c;
			//fadeIn3.renderer.material.color = c;
			yield return new WaitForSeconds(0);
		}

		float orig = camera.transform.position.y;
		float xx = .008f;
		targetY = camera.transform.position.y + 495f;
		Vector3 posVel = Vector3.zero;
		while(camera.transform.position.y < targetY) {
			follow.pullTo += Vector3.up * Time.deltaTime * .6f;
			//follow.pullTo += Vector3.right * Mathf.Sin((follow.pullTo.y - orig) / 10) * .002f;
			follow.pullTo += Vector3.right * xx * Time.deltaTime;
			xx = Mathf.Min(xx + Time.deltaTime * .002f, 0.009f);
			yield return new WaitForSeconds(0);
		}

		yield return new WaitForSeconds(1);
		Application.LoadLevel("Menu");
	}
}
