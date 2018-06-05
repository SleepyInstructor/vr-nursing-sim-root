using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/** With help from code at 
 * https://answers.unity.com/questions/1188342/traditional-movement-in-vr-using-the-vive-controll.html
 * and
 * https://answers.unity.com/questions/1028940/move-in-direction-camera-is-facing.html
 **/
public class TrackpadMove : MonoBehaviour
{
    // The camera
    public GameObject player;

    private SteamVR_TrackedObject controller;
    private SteamVR_Controller.Device device;

    private void Start()
    {
        controller = gameObject.GetComponent<SteamVR_TrackedObject>();
    }

    // Update is called once per frame
    private void Update()
    {
        device = SteamVR_Controller.Input((int)controller.index);
        LookAndMove(device);
    }

    /** RotateAndMove
    * A method of moving the camera where the player is stationary. 
    * Touching the top and bottom of the touchpad propel the player forwards and backwards. 
    * To move sideways, the camera is rotated by touching the left and right of the touchpad,
    * and then the top/bottom of the touchpad is pressed to move in the new direction.
    **/
    private void RotateAndMove(SteamVR_Controller.Device device)
    {
        // if user is touching touchpad
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            // read touchpad values
            Vector2 touchpad = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);

            // move forward/backward
            if (touchpad.y > 0.5f || touchpad.y < -0.5f)
            {
                player.transform.position -= player.transform.forward * Time.deltaTime * (touchpad.y * 3f);
            }

            // rotate camera
            if (touchpad.x > 0.5f || touchpad.x < -0.5f)
            {
                player.transform.Rotate(0, touchpad.x * 0.5f, 0);
            }

        }
    }

    /** LookAndMove
    * A method of moving the camera where the player looks in the direction they wish to go. 
    * Touching the top and bottom of the touchpad propel the player forwards and backwards. 
    **/
    private void LookAndMove(SteamVR_Controller.Device device)
    {
        // if user is touching touchpad
        if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
        {
            // read touchpad values
            Vector2 touchpad = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);

            // move forwards
            if (touchpad.y > 0.75f)
            {
                // change x and z values, then reset y to the default
                player.transform.position += Camera.main.transform.forward * 2f * Time.deltaTime;
                player.transform.position = ChangeY(player.transform.position, 0.1f);
            }

            // move backwards
            if (touchpad.y < -0.75f)
            {
                // change x and z values, then reset y to the default
                player.transform.position -= Camera.main.transform.forward * 2f * Time.deltaTime;
                player.transform.position = ChangeY(player.transform.position, 0.1f);
            }
           
            // move right
            if (touchpad.x > 0.75f)
            {
                // change x and z values, then reset y to the default
                player.transform.position += Camera.main.transform.right * 2f * Time.deltaTime;
                player.transform.position = ChangeY(player.transform.position, 0.1f);
            }
            
           // move left
           if (touchpad.x < -0.75f)
           {
               // change x and z values, then reset y to the default
               player.transform.position -= Camera.main.transform.right * 2f * Time.deltaTime;
               player.transform.position = ChangeY(player.transform.position, 0.1f);
           }
           
        }
    }

    // edits the y value of a Vector3
    private Vector3 ChangeY(Vector3 v3, float y)
    {
        return new Vector3(v3.x, y, v3.z);
    }
}
