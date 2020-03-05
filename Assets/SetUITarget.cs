using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUITarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        _camera = Camera.main;
        _transform = _camera.gameObject.GetComponentInChildren<Transform>();
    }

    public Camera _camera;

    public RectTransform crosshair;

    private Transform _transform;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity);
        if (hit.transform != null)
        {
            Vector3 pos = _camera.WorldToScreenPoint(hit.point);
            crosshair.position = new Vector3(pos.x,pos.y,0);
        }
        else
        {

            crosshair.position =
                _camera.WorldToScreenPoint(transform.position + transform.TransformDirection(Vector3.up) * 500f);
        }


    }
}
