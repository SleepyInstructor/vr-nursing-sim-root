using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Valve.VR.InteractionSystem;
namespace Valve.VR.InteractionSystem
{
    public class ControllerMove : MonoBehaviour
    {

        public GameObject player;  
        public Hand hand1;
        public Hand hand2;

        // Use this for initialization
        void Start()
        {


        }

        // Update is called once per frame
        void Update()
        {

            if (hand1 != null)
            {
                LookAndMove(hand1.controller);
            }
            if (hand2 != null)
            {
                LookAndMove(hand2.controller);
            }

        }

        private float calculateSpeedScale(float val, float min, float sat, float scale)
        {
            return Math.Min(Math.Max(0, (val - min) / (sat - min) * scale), scale);
        }
        /** LookAndMove
         * A method of moving the camera where the player looks in the direction they wish to go. 
         * Touching the top and bottom of the touchpad propel the player forwards and backwards. 
         **/
        private void LookAndMove(SteamVR_Controller.Device device)
        {
            // if user is touching touchpad
            if (device.GetPress(SteamVR_Controller.ButtonMask.Grip))
            {
                if (device.GetTouch(SteamVR_Controller.ButtonMask.Touchpad))
                {
                    // read touchpad values
                    Vector2 touchpad = device.GetAxis(Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad);
                    float ymov = Math.Sign(touchpad.y) * calculateSpeedScale(Math.Abs(touchpad.y), 0.25f, 0.75f, 2f);
                    float xmov = Math.Sign(touchpad.x) * calculateSpeedScale(Math.Abs(touchpad.x), 0.25f, 0.75f, 2f);

                    if (xmov != 0f || ymov != 0f)
                    {
                        player.transform.position += Camera.main.transform.forward * ymov * Time.deltaTime;
                        player.transform.position += Camera.main.transform.right * xmov * Time.deltaTime;
                        player.transform.position = ChangeY(player.transform.position, 0.1f);

                    }
                }
            }
        }
        // edits the y value of a Vector3
        private Vector3 ChangeY(Vector3 v3, float y)
        {
            return new Vector3(v3.x, y, v3.z);
        }

    }
}