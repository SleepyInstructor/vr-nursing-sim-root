using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class ECGMonitor : MonoBehaviour {
    private VideoPlayer player;

    private VideoClip normalReading;
    private VideoClip cardiacArrest;

	// Use this for initialization
	void Start () {
        player = GetComponent<VideoPlayer>();

        normalReading = Resources.Load("ECG Monitor Loop") as VideoClip;
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
