﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public static MobileInput Instance { set; get; }

    const float DEADZONE = 100.0f;
    bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    Vector2 swipeDelta, startTouch;
    public bool Tap { get { return tap; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }
    
    void Awake()
    {
        Instance = this;
    }

   

    void Update()
    {
        tap = swipeRight = swipeLeft = swipeUp = swipeDown = false;

        #region Standalone
        if(Input.GetMouseButtonDown(0))
        {
            tap = true;
            startTouch = Input.mousePosition;

        }
        else if(Input.GetMouseButtonUp(0))
        {
            startTouch = swipeDelta = Vector2.zero;
        }
        #endregion

        #region Mobile
        if (Input.touches.Length != 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                startTouch = Input.mousePosition;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                startTouch = swipeDelta = Vector2.zero;
            }

        }

        #endregion

        // Distance

        swipeDelta = Vector2.zero;
        if(startTouch != Vector2.zero)
        {
            if(Input.touches.Length != 0)
            {
                swipeDelta = Input.touches[0].position - startTouch;
            }
            else if(Input.GetMouseButton(0))
            {
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }
        }

        // Deadzone

        if(swipeDelta.magnitude > DEADZONE)
        {
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if(Mathf.Abs(x) > Mathf.Abs(y))
            {
                // right or left
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                // up or down
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }

            startTouch = swipeDelta = Vector2.zero;
        }
    }
}
