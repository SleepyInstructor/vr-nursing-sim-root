using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * With help from
 * https://answers.unity.com/questions/632792/how-to-make-an-object-a-child-of-another-object-sc.html
 **/

public class InContainer : MonoBehaviour {
    private Transform originalTransform;
    private Valve.VR.InteractionSystem.Throwable throwable;

    private void Start() {
        throwable = GetComponent<Valve.VR.InteractionSystem.Throwable>();
    }

    // Make the object a child of the container
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "container" && this.tag != "container") {
            originalTransform = this.transform.parent;
            this.transform.parent = other.transform;
        }
    }

    // Free the object from the container if affected by gravity or by grabbing
    private void OnTriggerExit(Collider other) {
        if(other.tag == "container" && this.tag != "container" && !throwable.isAttached()) {
            this.transform.parent = originalTransform;
        }
    }
}
