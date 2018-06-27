using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeadTracking : MonoBehaviour {
    public const double Tolerance = 0.5;

    public GameObject pillow;

    private Camera player;
    private Animator animator;
    private string[] animBools = { "leftOfPatient", "rightOfPatient", "frontOfPatient" };

    // Use this for initialization
    void Start () {
        player = Camera.main;
        animator = this.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if ((player.transform.position.x + Tolerance) < transform.position.x) {
            ToggleAnim("leftOfPatient");
        } else if ((player.transform.position.x - Tolerance) > transform.position.x) {
            ToggleAnim("rightOfPatient");
        } else {
            ToggleAnim("frontOfPatient");
        }
	}

    /**ToggleAnim
     * Set the variable with the given name to true, and the rest to false
     **/
    private void ToggleAnim(string flipOn){
        for(int i=0; i<animBools.Length; i++) {
            if (animBools[i].Equals(flipOn))
                animator.SetBool(animBools[i], true);
            else
                animator.SetBool(animBools[i], false);
        }
    }

    /** EnableRagdoll
     *  A method to be used by the animation that represents 
     *  unconsciousness.
     **/
    private void EnableRagdoll(){
        animator.enabled = false;
        // Make pillow throwable after patient has gone unconscious
        pillow.AddComponent<Valve.VR.InteractionSystem.Throwable>();
        pillow.AddComponent<InContainer>();
    }
}