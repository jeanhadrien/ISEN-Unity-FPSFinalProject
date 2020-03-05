using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DestroyAfter : MonoBehaviour
{
    [SerializeField] public float seconds;
    void Start()
    {
        StartCoroutine(Destroy(seconds));
    }

    IEnumerator Destroy(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }

}
