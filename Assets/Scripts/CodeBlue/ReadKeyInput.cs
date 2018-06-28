using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ReadKeyInput : MonoBehaviour
{
    public const string Instructions = "t - toggle instructions\ni - introduce\nc - check ID band\na - ask for pain level\nb - trigger code blue";

    public Canvas otherCanvas;
    public Text instructionDisplay;
    public GameObject wrist; // the plane over the client's wrist
    public RawImage image; // where the wristband is displayed

    private CodeBlueScript script;
    private bool displayVisible;

    // Use this for initialization
    void Start() {
        script = otherCanvas.GetComponent<CodeBlueScript>();
        instructionDisplay.text = Instructions;
        wrist.SetActive(false);
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
            if (wrist.activeSelf)
                ToggleImage(false);
        }
        // Check patient ID
        else if (Input.GetKeyDown(KeyCode.C)){
            script.CheckBand();
            ToggleImage(true);
        }
        // Perform pain assessment
        else if (Input.GetKeyDown(KeyCode.A)) {
            script.Assessment();
            // make UI Image transparent
            if (wrist.activeSelf)
                ToggleImage(false);
        }
        // Trigger a code blue
        // Only scares patient at the moment
        else if (Input.GetKeyDown(KeyCode.B)) {
            script.CodeBlue();
            if (wrist.activeSelf)
                ToggleImage(false);
        }
    }

    /** ToggleImage
     *  Toggles the view of the client's wristband 
     *  on and off (in both UI and on plane)
     **/
    private void ToggleImage(bool turnOn) {
        Color temp = image.color;

        // if the wristband is visible
        if (turnOn)
            temp.a = 1f;
        else
            temp.a = 0f;

        image.color = temp;
        wrist.SetActive(turnOn);
    }
}