using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchWithHands : MonoBehaviour {
    public Canvas canvas;

    private Valve.VR.InteractionSystem.Hand handScript;
    private CodeBlueScript cBScript;

    private Texture latex;
    private Texture skin;
    private bool buttonPushed;

    // Use this for initialization
    private void Start () {
        handScript = this.GetComponent<Valve.VR.InteractionSystem.Hand>();
        cBScript = canvas.GetComponent<CodeBlueScript>();
        latex = Resources.Load("latex") as Texture;
        skin = Resources.Load("FemaleMedium") as Texture;
        buttonPushed = false;
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
        } else if (other.tag.Equals("blueButton")) {
            // prevent pushing the button multiple times
            if (!buttonPushed) { 
                cBScript.CodeBlue();
                buttonPushed = true;
            }
        }
    }
}
