using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutOnGloves : MonoBehaviour {
    private Texture latex;
    private Renderer render;

	// Use this for initialization
	private void Start () {
        render = GetComponent<Renderer>();
        latex = Resources.Load("latex") as Texture;
	}

    /** OnTriggerEnter
     *  Changes the material of the hands to latex when coming into 
     *  contact with the box of gloves, signifying puttign on and wearing gloves
     **/
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "gloves" && render.materials[1].mainTexture != latex)
            render.materials[1].mainTexture = latex;
        // else back to hands? 
    }

}
