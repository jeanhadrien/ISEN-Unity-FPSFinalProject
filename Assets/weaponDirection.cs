using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponDirection : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] public GameObject target;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      transform.LookAt(target.transform);  
      transform.Rotate(Vector3.right,90f);
    }
    
    

}
