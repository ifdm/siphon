using UnityEngine;
using System.Collections;

[RequireComponent (typeof(SpriteRenderer))]
public class CursorBehavior : MonoBehaviour
{
	SpriteRenderer myRenderer;
	private Sprite[] cursorLoop;
	private Sprite[] cursorStart;
	
	//Note: I'd prefer to package the sprites with information about 
	//scale and offset. I will probably migrate this information into
	//the plant classes themselves. Just gotta make sure I don't break
	//anything in the process.
	public Sprite bm_ghost;
	public Sprite rb_ghost;
	public Sprite gv_ghost;
	
	//The scales for the ghost images are absurdly large, and all different from each other.
	public float bm_scale;
	public float gv_scale;
	public float rb_scale_h; 	//Root bridge: the scale of actual creation is gigantic.
	public float rb_scale_v;
	
	public float bm_offset;
	public float gv_offset;
	public float rb_offset;
	
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
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (!isCursorDrawing) {
			isCursorLoop = isCursorStart = false;
			frameCounter = 0;
			myRenderer.sprite = null;
			return;
		} 
		
		transform.position = position;
		transform.localScale = new Vector3 (0.12f, 0.12f);
		transform.rotation = new Quaternion(0, 0, 0, 1);

		//If we have a plant that we can draw, do that
		if (plant != null && plant.canPlant (raycast)) {
			Sprite sprite = null;
			float scaleh = 1.0f;
			float scalev = 1.0f;
			
			float vOffset = 0.0f;
			float hOffset = 0.0f;

			if(plant is Mushroom) { 
				sprite = bm_ghost; 
				scaleh = scalev = bm_scale; 
				vOffset = bm_offset;
			}
			else if(plant is GrappleVineGrowth) { 
				sprite = gv_ghost; 
				scaleh = scalev = gv_scale; 
				vOffset = gv_offset;
			}
			else if(plant is RootbridgeGrowth) { 
				sprite = rb_ghost; 
				scaleh = rb_scale_h;
				scalev = rb_scale_v;
				hOffset = rb_offset;
				if(raycast.normal.x < 0){
					transform.rotation = new Quaternion(0, 180, 0, 1);
					hOffset = -rb_offset;
				}		
			}
			myRenderer.sprite = sprite;
			transform.localScale = new Vector3(scaleh, scalev);
			transform.position = raycast.point;
			transform.position += new Vector3(hOffset, vOffset);

			return;
		}
	
		//Draws the crosshairs if no plants can be drawn
		if (!isCursorLoop) {
			isCursorStart = true;
		}
		
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
}

