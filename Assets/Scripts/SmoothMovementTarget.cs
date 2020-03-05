using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothMovementTarget : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform target;
    public float maxMovement;
    public Vector3 offset;
    void Start()
    {
        if (target != null)
        {
            
        }
        
    }
    
    // Update is called once per frame
    void Update()
    {
        var diffVector = transform.position - (target.position +offset);
        transform.position = transform.position - new Vector3(
            Mathf.Clamp(diffVector.x, -maxMovement, maxMovement),
            Mathf.Clamp(diffVector.y, -maxMovement, maxMovement),
            Mathf.Clamp(diffVector.z, -maxMovement, maxMovement));

    }
}
