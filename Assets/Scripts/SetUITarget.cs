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
    
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _transform = _camera.gameObject.GetComponentInChildren<Transform>();
        _playable = GetComponentInParent<Playable>();
    }


    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity);
        if (hit.transform != null)
        {
            gunTargetPosition = hit.point;
            Vector3 pos = _camera.WorldToScreenPoint(gunTargetPosition);
            crosshair.position = new Vector3(pos.x,pos.y,0);
        }
        else
        {
            gunTargetPosition = transform.position + transform.TransformDirection(Vector3.up) * 500f;
            crosshair.position = _camera.WorldToScreenPoint(gunTargetPosition);
        }
        _playable.SetGunTargetPosition(gunTargetPosition);
    }
}
