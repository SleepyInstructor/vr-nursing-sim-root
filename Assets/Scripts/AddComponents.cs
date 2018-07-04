using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddComponents : MonoBehaviour {
    // An array of items in the scene that can be grabbed and thrown
    public GameObject[] interactables;

	// Use this for initialization
	void Start () {
        if (interactables != null) {
            // Add applicable scripts to all interactible items
            for (int i = 0; i < interactables.Length; i++){
                interactables[i].AddComponent<Valve.VR.InteractionSystem.Throwable>();
                interactables[i].AddComponent<InContainer>();
            }
        }
	}
}
