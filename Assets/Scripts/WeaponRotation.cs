using UnityEngine;

public class WeaponRotation : MonoBehaviour
{
    private Playable _playable;

    void Start()
    {
        _playable = GetComponentInParent<Playable>();
    }

    /// <summary>
    /// if player is aiming, align weapon with aimed target
    /// </summary>
    void Update()
    {
        if (_playable.isAiming)
        {
            transform.LookAt(transform.position + Camera.main.transform.forward);
            transform.Rotate(Vector3.up, 90f);
        }
    }
}