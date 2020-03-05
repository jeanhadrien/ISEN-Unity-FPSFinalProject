﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    // Start is called before the first frame update
    private float _timeSinceLastBullet = 0;
    public float minTimeBetweenBullets = 0.3f;
    public Animator animator;
    private static readonly int IsFiring = Animator.StringToHash("isFiring");
    private bool isAiming;
    private bool firing;

    private Playable _playable;
    private float lerpTimeZoom = 0;
    public float zoomSpeed = 1;

    public ParticleSystem flare, tracers;
    public Light light;
    public float normalFOV = 60f, zoomedFOV=20f;
    void Start()
    {
        _playable = GetComponent<Playable>();
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (Input.GetMouseButton(1))
        {
            lerpTimeZoom += Time.deltaTime * zoomSpeed;
            lerpTimeZoom = Mathf.Clamp(lerpTimeZoom, 0, 1);

        }
        else
        {
            lerpTimeZoom -= Time.deltaTime * zoomSpeed;
            lerpTimeZoom = Mathf.Clamp(lerpTimeZoom, 0, 1);

        }
        Camera.main.fieldOfView = Mathf.Lerp(normalFOV, zoomedFOV, lerpTimeZoom);
        
        _timeSinceLastBullet += Time.deltaTime;
        
        if (Input.GetMouseButton(0) && _playable.isAiming)
        {
            if (_timeSinceLastBullet >= minTimeBetweenBullets)
            {
                
                _timeSinceLastBullet = 0;
                // fire 
                flare.Emit(1);
                light.enabled = true;
                StartCoroutine(TurnOffLightAfter(light, 0.1f));

            }
            else
            {
                firing = false;
            }
        }
    }

    IEnumerator TurnOffLightAfter(Light light, float time)
    {
        yield return time;
        light.enabled = false;
    }
}