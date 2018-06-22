using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


// For next time
// https://answers.unity.com/questions/160848/how-do-i-run-two-animations-at-once-on-the-same-ga.html


public class CodeBlueScript : MonoBehaviour {
    // where the dialogue script will be read from
    // only appears to work with hardlinks? 
    public const string ScriptLocation = "C:\\Users\\Erin\\Documents\\VRNursingSim\\Assets\\Scripts\\CodeBlue\\PatientScript.txt";

    // Indexes of beginning and end of each grouping of dialogue
    public const int StartIntro = 1;
    public const int EndIntro = 3;

    public const int StartAssessment = 4;
    public const int EndAssessment = 6;

    public Canvas canvas;
    public GameObject patient;
    public Text dialogueDisplay;

    // the entire script of dialogue
    private string[] patientScript;
    // total number of lines in script
    private int totalLines;
    // for playing certain animations of the patient
    private Animator animator;

	// Use this for initialization
	void Start () {
        animator = patient.GetComponent<Animator>();
        // read dialogue from a file, if it exists
        if (System.IO.File.Exists(ScriptLocation))
            patientScript = System.IO.File.ReadAllLines(ScriptLocation);
        else
            patientScript = new string[0];

        // set starting values
        totalLines = patientScript.Length;

        Introduction();
	}

    /**
     * With help from 
     * https://forum.unity.com/threads/please-help-me-with-script-making-ui-canvas-look-towards-camera.336270/
     **/
    void Update () {
        // face the canvas towards the player
        Camera camera = Camera.main;
        canvas.transform.LookAt(camera.transform.position + Vector3.forward);
        canvas.transform.Rotate(0, 180, 0);
	}

    /** Introduction
     *  The first part of the simulation. 
     *  The patient: 
     *      - rings call bell
     *      - is writhing in bed
     *      - is holding left shoulder
     *      - is complaining or pain and nausea
     **/
    private void Introduction() {
        dialogueDisplay.text = patientScript[StartIntro-1];
        Dialogue(5, StartIntro, EndIntro);

        //animator.Play("nameOfAnimation");
    }

    /** Assessment 
     *  After coming to the patient's room. 
     *  The nurse must: 
     *      - introduce themselves
     *      - check patient's ID band
     *      - perform a series of assessments (?)
     **/
    private void Assessment() {
        Dialogue(5, StartAssessment, EndAssessment);
    }

    /** Wait
     *  Starts the coroutine "PaceDialogue"
     *  Accepts 3 parameters: 
     *  seconds - the number of seconds to wait between each line of dialogue
     *  start - the index of the first line of dialogue taken from patientScript
     *  end - the index of the final line of dialogue taken from patientScript
     **/
    private void Dialogue(int seconds, int start, int end) {
        StartCoroutine(PaceDialogue(seconds, start, end));
    }

    /** PaceDialogue
    *   A method to wait for a certain number of seconds before displaying
    *   each line of the script
    **/
    private IEnumerator PaceDialogue(int seconds, int start, int end) {
        int subLine = start;
        while (subLine < end) {
            yield return new WaitForSecondsRealtime(seconds);
            dialogueDisplay.text = patientScript[subLine];
            subLine++;
        }
    }
}
