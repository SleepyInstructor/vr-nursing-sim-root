using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * With help from
 * https://answers.unity.com/questions/632792/how-to-make-an-object-a-child-of-another-object-sc.html
 **/

public class InContainer : MonoBehaviour {
    private Transform originalTransform;

    // Make the object a child of the container
    private void OnTriggerEnter(Collider other) {
        if(other.tag == "container" && this.tag != "container") {
            originalTransform = this.transform.parent;
            this.transform.parent = other.transform;
        }
    }

    // Free the object from the container
    private void OnTriggerExit(Collider other) {
        if(other.tag == "container" && this.tag != "container") {
            this.transform.parent = originalTransform;
        }
    }
}
