using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour {
    public GameObject door;

    private Animator animator;

    private void Start() {
        animator = door.GetComponent<Animator>();
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("patientSideDoor")) {
            animator.SetBool("PatientDoor", true);
        } else if (other.CompareTag("storageSideDoor")) {
            animator.SetBool("StorageDoor", true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("patientSideDoor")) {
            animator.SetBool("PatientDoor", false);
        } else if (other.CompareTag("storageSideDoor")) {
            animator.SetBool("StorageDoor", false);
        }
    }
}
