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

    void OnDisable()
    {
        controller.TriggerClicked -= CloseHand;
        controller.TriggerUnclicked -= OpenHand;
    }

    void CloseHand(object sender, ClickedEventArgs e)
    {
        animator.SetBool("isGrabbing", true);  
    }

    void OpenHand(object sender, ClickedEventArgs e)
    {
        animator.SetBool("isGrabbing", false);
    }
}
