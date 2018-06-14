using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutOnGloves : MonoBehaviour {
    private Valve.VR.InteractionSystem.Hand handScript;

    private Texture latex;
    private Texture skin;

    // Use this for initialization
    private void Start () {
        handScript = this.GetComponent<Valve.VR.InteractionSystem.Hand>();
        latex = Resources.Load("latex") as Texture;
        skin = Resources.Load("FemaleMedium") as Texture;
    }

    /** OnTriggerEnter
     *  Changes the material of the hands to latex when coming into 
     *  contact with the box of gloves, signifying putting on and wearing gloves.
     *  Changes the material of the hands back to skin when coming into 
     *  contact with the trash can below the gloves, signifying the disposal
     *  of the worn gloves. 
     **/
    private void OnTriggerEnter(Collider other) {
        if (other.tag.Equals("gloves")) { 
            handScript.ChangeModelTexture(latex);
        } else if (other.tag.Equals("trash")) { 
            handScript.ChangeModelTexture(skin);
        }
    }
}
