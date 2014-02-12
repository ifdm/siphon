using UnityEngine;
using System.Collections;

public class ParticleSortingLayer : MonoBehaviour {

	public string sortingLayer;
	public int sortingOrder;

	void Start() {
		particleSystem.renderer.sortingLayerName = sortingLayer;
		particleSystem.renderer.sortingOrder = sortingOrder;
	}
}
