using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandGrabbing : MonoBehaviour {
    public SteamVR_TrackedController controller;

    private Animator animator;

	// Use this for initialization
	void OnEnable () {
        animator = GetComponent<Animator>();
        controller.TriggerClicked += CloseHand;
        controller.TriggerUnclicked += OpenHand;
	}

    // For deinitialization
    void OnDisable()
    {
        controller.TriggerClicked -= CloseHand;
        controller.TriggerUnclicked -= OpenHand;
    }

    // Triggers the animation for closing a hand
    void CloseHand(object sender, ClickedEventArgs e)
    {
        animator.SetBool("isGrabbing", true);  
    }

    // Triggers the animation for opening a han
    void OpenHand(object sender, ClickedEventArgs e)
    {
        animator.SetBool("isGrabbing", false);
    }
}
