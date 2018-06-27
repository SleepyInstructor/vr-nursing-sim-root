using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// From
// https://www.youtube.com/watch?v=QQiprHzSD1I

public class IVTube : MonoBehaviour {
    private Rigidbody rigidTube;

	// Use this for initialization
	void Start () {
        this.gameObject.AddComponent<Rigidbody>();
        rigidTube = this.gameObject.GetComponent<Rigidbody>();
        rigidTube.isKinematic = true;

        int childCount = this.transform.childCount;

        for(int i = 0; i < childCount; i++) {
            Transform t = this.transform.GetChild(i);

            t.gameObject.AddComponent<HingeJoint>();
            t.gameObject.AddComponent<Rigidbody>();

            HingeJoint hinge = t.gameObject.GetComponent<HingeJoint>();

            if (i == 0)
                hinge.connectedBody = rigidTube;
            else
                hinge.connectedBody = this.transform.GetChild(i - 1).GetComponent<Rigidbody>();

            hinge.useSpring = true;
            hinge.enableCollision = true;
        }
	}
}
