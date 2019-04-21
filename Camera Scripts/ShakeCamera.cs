﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour {

    public float power = 0.2f;
    public float duration = 0.2f;
    public float slowDownAmount = 1f;
    private bool should_Shake;
    private float initialDuration;

    private Vector3 startPosition;

    void Start() {
        
        initialDuration = duration;
    }

    // Update is called once per frame
    void Update() {
        startPosition = transform.localPosition;
        Shake();
    }

    void Shake() { 

        if(should_Shake) { 
        
            if(duration > 0f) {

                //camera position = Randompoint inside a sphere with unit one multiplied by power (every frame)
                transform.localPosition = startPosition + Random.insideUnitSphere * power; 
                duration -= Time.deltaTime * slowDownAmount;

            } else {

                should_Shake = false;
                duration = initialDuration;
                transform.localPosition = startPosition;
            }

        } // if we should shake the camera

    } // shake

    public bool ShouldShake { 
        get {
            return should_Shake;
        }
        set {
            should_Shake = value;
        }
    }


} // class






































