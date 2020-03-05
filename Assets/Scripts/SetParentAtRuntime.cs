using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetParentAtRuntime : MonoBehaviour
{
    public GameObject parent;
    // Start is called before the first frame update
    void Awake()
    {
        transform.parent = parent.transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
