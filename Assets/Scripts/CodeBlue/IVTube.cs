using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// From
// https://www.youtube.com/watch?v=QQiprHzSD1I

public class IVTube : MonoBehaviour {
    public GameObject patientArm;

    private Rigidbody rigidArm;
    private Rigidbody rigidTube;

	// Use this for initialization
	void Start () {
        this.gameObject.AddComponent<Rigidbody>();

        rigidArm = patientArm.GetComponent<Rigidbody>();
        rigidTube = this.gameObject.GetComponent<Rigidbody>();
        rigidTube.isKinematic = true;

        int childCount = this.transform.childCount;

        for(int i = 0; i < childCount; i++) {
            Transform t = this.transform.GetChild(i);

            t.gameObject.AddComponent<HingeJoint>();

            HingeJoint hinge = t.gameObject.GetComponent<HingeJoint>();

            if (i == 0)
                hinge.connectedBody = rigidTube;
            else
                hinge.connectedBody = this.transform.GetChild(i - 1).GetComponent<Rigidbody>();

            /**
            if (i == childCount - 1) {
                patientArm.AddComponent<HingeJoint>();
                hinge = patientArm.GetComponent<HingeJoint>();
                hinge.connectedBody = this.transform.GetChild(i).GetComponent<Rigidbody>();

                hinge.useSpring = true;
                hinge.enableCollision = true;
            }
            **/
        }
	}
}
