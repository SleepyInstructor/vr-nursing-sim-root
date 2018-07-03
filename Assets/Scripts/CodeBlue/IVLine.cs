using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IVLine : MonoBehaviour {
    public const int MaxObjects = 20;

    private static int numObjects = 0;
	// Use this for initialization
	void Start () {
        if (numObjects < MaxObjects) {
            GameObject nextLink = Instantiate(this.gameObject, this.transform);
            GetComponent<HingeJoint>().connectedBody = transform.parent.GetComponent<Rigidbody>();
            numObjects++;
        } else {
            GetComponent<HingeJoint>().connectedBody = transform.parent.GetComponent<Rigidbody>();
        }
	}
}
