using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutOnGloves : MonoBehaviour {
    private GameObject handPrefab;
    private MeshRenderer render;
    private Texture latex;
    private Texture skin;

	// Use this for initialization
	private void Start () {
        Valve.VR.InteractionSystem.Hand handScript = this.GetComponent<Valve.VR.InteractionSystem.Hand>();

        handPrefab = handScript.controllerPrefab;
        render = handPrefab.GetComponent<MeshRenderer>();
        latex = Resources.Load("latex") as Texture;
        skin = Resources.Load("FemaleMedium") as Texture;

        render.material.mainTexture = latex;
    }

    /** OnTriggerEnter
     *  Changes the material of the hands to latex when coming into 
     *  contact with the box of gloves, signifying putting on and wearing gloves
     **/
    private void OnTriggerEnter(Collider other) {
        print("Touched " + other.tag + " with " + this.tag);

        if (other.tag == "gloves") {
            if (render.material.mainTexture != latex) { 
                render.material.mainTexture = latex;
                print("Changing skin to latex.");
            } else { 
                render.material.mainTexture = skin;
                print("Changing skin to skin.");
            }
         }
        // Maybe touching trash bin returns to skin material? 
    }
}
