using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssessObject : MonoBehaviour {
    public GameController gc;

    private void OnTriggerEnter(Collider other){
        gc.IsTaskComplete(other.tag);
    }
}
