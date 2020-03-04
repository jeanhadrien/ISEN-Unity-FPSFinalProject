using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetUITarget : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public Camera camera;

    public RectTransform crosshair;
    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.up), out hit, Mathf.Infinity))
        {
            Vector3 pos = camera.WorldToScreenPoint(hit.point);
            crosshair.position = new Vector3(pos.x,pos.y,0);
        }
    }
}
