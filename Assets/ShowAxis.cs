using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowAxis : MonoBehaviour
{

    [SerializeField] public static bool debug = true;
    public float distance = 0.2f;

    void OnDrawGizmos()
    {
        if (ShowAxis.debug)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(transform.position, transform.position+ transform.up * distance);
            
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, transform.position+ transform.right * distance);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawLine(transform.position, transform.position+ transform.forward * distance);
                    
        }
  
    }
}
