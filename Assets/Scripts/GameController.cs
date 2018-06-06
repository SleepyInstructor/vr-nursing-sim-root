using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    // An array of items in the scene that can be grabbed and thrown
    public GameObject[] interactibles;


	// Use this for initialization
	void Start () {
        if (interactibles != null) {
            // Add Throwable (and Rigidbody) to all interactible items
            for (int i = 0; i < interactibles.Length; i++){
                //interactibles[i].AddComponent<Rigidbody>();
                interactibles[i].AddComponent<Valve.VR.InteractionSystem.Throwable>();
            }
        }
	}
}
