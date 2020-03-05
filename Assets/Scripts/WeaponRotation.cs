using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    // Start is called before the first frame update
    private Playable _playable;
    public Transform barrel;
    private float t;
    void Start()
    {
        _playable = GetComponentInParent<Playable>();
    }
    
    // Update is called once per frame
    void Update()
    {
        if (_playable.isAiming)
        { 
            transform.LookAt(transform.position+Camera.main.transform.forward);
            transform.Rotate(Vector3.up,90f);
        }
        

    }
}
