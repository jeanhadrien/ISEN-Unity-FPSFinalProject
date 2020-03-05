using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUITarget : MonoBehaviour
{
    private Playable _playable;
    public Camera _camera;
    public RectTransform crosshair;
    private Transform _transform;
    public Vector3 gunTargetPosition;

    
    void Start()
    {
        _camera = Camera.main;
        _transform = _camera.gameObject.GetComponentInChildren<Transform>();
        _playable = GetComponentInParent<Playable>();
    }

    /// <summary>
    /// Middle of the screen is the crosshair, sets the target point and updates raycasthit using raycasts 
    /// </summary>
    void Update()
    {
        RaycastHit hit;
        // layer mask for bullet holes 
        int layerMask = 1 << 8;
        layerMask = ~layerMask;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity,
            layerMask);
        if(hit.collider != null) MyUiManager.SetTextUpR(hit.collider.gameObject.name);
        if (hit.transform != null)
        {
            gunTargetPosition = hit.point;
            Vector3 pos = _camera.WorldToScreenPoint(gunTargetPosition);
            crosshair.position = new Vector3(pos.x,pos.y,0);
        }
        else
        {
            // if target is skybox, raycast doesn't register so we set target as a long distance away from camera
            gunTargetPosition = transform.position + transform.TransformDirection(Vector3.up) * 500f;
            crosshair.position = _camera.WorldToScreenPoint(gunTargetPosition);
        }
        _playable.SetGunTargetPosition(gunTargetPosition);
        _playable.SetGunTargetRaycastHit(hit);
    }
}
