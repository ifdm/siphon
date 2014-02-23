using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
public class CursorBehavior : MonoBehaviour
{
	SpriteRenderer myRenderer;
	private Sprite[] cursorLoop;
	private Sprite[] cursorStart;
	public Sprite bm_ghost;
	public Sprite rb_ghost;
	public Sprite gv_ghost;


	private bool isCursorStart = false;
	private bool isCursorLoop = false;
	private int frameCounter = 0;

	[HideInInspector] public bool isCursorDrawing = false;
	[HideInInspector] public Vector2 position = new Vector2(0,0);
	[HideInInspector] public RaycastHit2D raycast;
	[HideInInspector] public Plant plant;

	// Use this for initialization
		
	void Start ()
	{
		myRenderer = GetComponent<SpriteRenderer> ();


		cursorLoop = Resources.LoadAll <Sprite>("GUI Animations/crosshair-loop");		
		cursorStart = Resources.LoadAll <Sprite>("GUI Animations/crosshair-start");
		transform.localScale -= new Vector3 (0.88f, 0.88f);

	}
	
	// Update is called once per frame
	void Update ()
	{

		transform.position = position;

		if (!isCursorDrawing) {
			isCursorLoop = isCursorStart = false;
			myRenderer.sprite = null;
			return;
		} 

		else if (plant.canPlant (raycast)) {
			Sprite sprite = null;
			float scale = 0.2f;
			float vOffset = .3f;

			if(plant is Mushroom) { sprite = bm_ghost; scale = 0.2f; }
			else if(plant is GrappleVineGrowth) { sprite = gv_ghost; scale = 0.05f; }
			else if(plant is RootbridgeGrowth) { sprite = rb_ghost; scale = 0.2f;}
			myRenderer.sprite = sprite;
			transform.localScale = new Vector3(scale, scale);
			transform.position += new Vector3(0, vOffset);

			return;
		}

		else if (!isCursorLoop) {
			isCursorStart = true;
		}

		if(isCursorStart || isCursorLoop){
			transform.localScale = new Vector3 (0.12f, 0.12f);
			Sprite[] cur = (isCursorStart) ? cursorStart : cursorLoop;
			Sprite tex = cur[frameCounter];
			myRenderer.sprite = tex;
			
			frameCounter++;
			if(frameCounter >= cur.Length){
				if(isCursorStart){   //switch to the loop after doing start
					isCursorStart = false;
					isCursorLoop = true;
					frameCounter = 0;
				}
				frameCounter = 0;
			}
		}
		else
			frameCounter = 0;
	}


}

