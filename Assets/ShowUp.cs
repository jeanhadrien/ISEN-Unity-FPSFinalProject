using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    

    void OnDrawGizmosSelected()
    {
// Draws a blue line from this transform to the target
        Gizmos.color = Color.green;
        Gizmos.DrawLine(transform.position, transform.position+ transform.up * 20);
        
    }
}
