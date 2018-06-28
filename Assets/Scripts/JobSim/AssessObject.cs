using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssessObject : MonoBehaviour {
    public ChangeScreen gameManager;

    private void OnTriggerEnter(Collider other){
        gameManager.IsTaskComplete(tag);
    }
}
