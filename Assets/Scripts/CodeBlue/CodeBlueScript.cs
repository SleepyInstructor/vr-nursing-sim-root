﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


// For next time
// https://answers.unity.com/questions/160848/how-do-i-run-two-animations-at-once-on-the-same-ga.html

public class CodeBlueScript : MonoBehaviour {
    // where the dialogue script will be read from
    // only appears to work with hardlinks? 
    public const string ScriptLocation = "C:\\Users\\Erin\\Documents\\VRNursingSim\\Assets\\Scripts\\CodeBlue\\PatientScript.txt";

    public GameObject patient;
    public Text dialogueDisplay;
    public Text intercomDisplay;

    // checklist of performed actions
    private bool[] completed;
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
        /** Meanings
        *   1) has introduced
        *   2) has checked ID 
        *   3) has assessed pain
        *   4) has triggered code blue **/
        completed = new bool[] { false, false, false, false };
        dialogueDisplay.text = "";
        intercomDisplay.text = "";
	}

    /**
     * With help from 
     * https://forum.unity.com/threads/please-help-me-with-script-making-ui-canvas-look-towards-camera.336270/
     **/
    void Update () {
        // face the canvas towards the player
        Camera camera = Camera.main;
        this.transform.LookAt(camera.transform.position + Vector3.forward);
        this.transform.Rotate(0, 180, 0);
	}

    /** Introduction 
     *  After coming to the patient's room. 
     *  The nurse must: 
     *      - introduce themselves
     **/
    public void Introduction() {
        // introduce self first time
        if (!completed[0]) {
            Dialogue(dialogueDisplay, 4, IndexProtocol.StartIntro, IndexProtocol.EndIntro);
            completed[0] = true;
        // if nurse has already introduced themselves
        } else {
            Dialogue(dialogueDisplay, 4, IndexProtocol.MultiIntro);
        }
    }

    /** CheckBand 
     *  After coming to the patient's room. 
     *  The nurse must: 
     *      - check patient's ID band
     **/
    public void CheckBand() {
        // if nurse has not introduced themselves
        // will still show ID
        if (!completed[0]) {
            Dialogue(dialogueDisplay, 4, IndexProtocol.IDNoIntro);
        // check ID first time
        } else if (!completed[1]) {
            Dialogue(dialogueDisplay, 4, IndexProtocol.CheckID);
            completed[1] = true;
            // if nurse has already checked band
        } else {
            Dialogue(dialogueDisplay, 4, IndexProtocol.MultiID);
        }
    }

    /** Assessment 
     *  After coming to the patient's room. 
     *  The nurse must: 
     *      - perform a series of assessments (?)
     **/
    public void Assessment() {
        // if nurse has not introduced themselves first
        if (!completed[0]) {
            Dialogue(dialogueDisplay, 4, IndexProtocol.IDNoIntro);
        } else if (!completed[2]) {
            Dialogue(dialogueDisplay, 4, IndexProtocol.AssessPain);
            completed[2] = true;
        } else {
            Dialogue(dialogueDisplay, 4, IndexProtocol.MultiAssess);
        }
    }

    /** CodeBlue
     *  When the nurse triggers a code blue. 
     *  Can be triggered too early and scare the patient, 
     *  triggering their heart attack. 
     **/
    public void CodeBlue() {
        // If code blue is called before patient begins arrest
        if (!animator.GetBool("isUnconscious")) { 
            Dialogue(dialogueDisplay, 3, IndexProtocol.EarlyBlue);
            animator.SetBool("isUnconscious", true);
        }
        Dialogue(intercomDisplay, 10, IndexProtocol.FirstContact, IndexProtocol.SecondContact);
    }

    /** Debrief
     *  Gives a rundown on the performance of the player in a text file. 
     **/
    private void Debrief() {

     }

    /** Wait
    *  Starts the coroutine "PaceDialogue"; used for single lines
    *  Accepts 3 parameters: 
    *  outlet - where the text will be displayed
    *  seconds - the number of seconds to wait between each line of dialogue
    *  start - the index of the first line of dialogue taken from patientScript
    **/
    private void Dialogue(Text outlet, int seconds, int start) {
        IEnumerator display;
        display = PaceDialogue(outlet, seconds, start, start+1);
        StartCoroutine(display);
    }

    /** Wait
     *  Starts the coroutine "PaceDialogue"
     *  Accepts 4 parameters: 
     *  outlet - the location where text will be displayed
     *  seconds - the number of seconds to wait between each line of dialogue
     *  start - the index of the first line of dialogue taken from patientScript
     *  end - the index of the final line of dialogue taken from patientScript
     **/
    private void Dialogue(Text outlet, int seconds, int start, int end) {
        IEnumerator display;
        display = PaceDialogue(outlet, seconds, start, end);
        StartCoroutine(display);
    }

    /** PaceDialogue
    *   A method to wait for a certain number of seconds before displaying
    *   each line of the script
    **/
    private IEnumerator PaceDialogue(Text outlet, int seconds, int start, int end) {
        int subLine = start;
        while (subLine < end) {
            outlet.text = patientScript[subLine-1];
            subLine++;
            yield return new WaitForSecondsRealtime(seconds);
            outlet.text = "";
        }
    }
}
