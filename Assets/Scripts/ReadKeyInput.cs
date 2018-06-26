using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ReadKeyInput : MonoBehaviour
{
    public const string Instructions = "t - toggle instructions\ni - introduce\nc - check ID band\na - ask for pain level\nb - trigger code blue";

    public Canvas otherCanvas;
    public Text instructionDisplay;
    public RawImage image;

    private CodeBlueScript script;
    private bool displayVisible;

    // Use this for initialization
    void Start() {
        script = otherCanvas.GetComponent<CodeBlueScript>();
        instructionDisplay.text = Instructions;
        displayVisible = true;
    }

    // Update is called once per frame
    void Update() {
        // Toggle the UI on and off
        if (Input.GetKeyDown(KeyCode.T)) {
            if (displayVisible) { 
                instructionDisplay.text = "t - toggle instructions";
            } else { 
                instructionDisplay.text = Instructions;
            }
            displayVisible = !displayVisible;
        }
        // Introduce self 
        else if (Input.GetKeyDown(KeyCode.I)) {
            script.Introduction();
            // make UI Image transparent
            if (image.color.a != 0f) {
                Color temp = image.color;
                temp.a = 0f;
                image.color = temp;
            }
        }
        // Check patient ID
        else if (Input.GetKeyDown(KeyCode.C)){
            script.CheckBand();
            Color temp = image.color;
            temp.a = 1f;
            image.color = temp;
        }
        // Perform pain assessment
        else if (Input.GetKeyDown(KeyCode.A)) {
            script.Assessment();
            // make UI Image transparent
            if (image.color.a != 0f) {
                Color temp = image.color;
                temp.a = 0f;
                image.color = temp;
            }
        }
        // Trigger a code blue
        // Only scares patient at the moment
        else if (Input.GetKeyDown(KeyCode.B)) {
            script.CodeBlue();
        }
    }
}