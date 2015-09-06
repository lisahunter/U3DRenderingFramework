using UnityEngine;
using System.Collections;
using Rendering;

public class RenderingNodeTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Camera cam = GetComponent<Camera>();
        cam.enabled = false;
        Rendering.RenderingUnit unit = new RenderingUnit(cam);
        RenderingMgr.Instance.AddUnitAtLast(unit);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
