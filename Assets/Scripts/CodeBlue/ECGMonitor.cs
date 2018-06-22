using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

/**
 * ECG Video Clips from 
 * https://www.youtube.com/channel/UCvLIj1x5nuOm26nFqlccb1w/videos
 **/
public class ECGMonitor : MonoBehaviour {
    private VideoPlayer player;

    private VideoClip normalReading;
    private VideoClip cardiacArrest;

	// Use this for initialization
	void Start () {
        player = GetComponent<VideoPlayer>();

        normalReading = Resources.Load("ECG Monitor Loop") as VideoClip;
        cardiacArrest = Resources.Load("CardiacArrest") as VideoClip;

        player.clip = cardiacArrest;
    }
	
	// Plays the cardiac arrest clip on the monitor
	public void CardiacArrest () {
        player.clip = cardiacArrest;
    }


}
