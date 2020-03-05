using System;
using System.Collections;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    private static readonly int IsFiring = Animator.StringToHash("isFiring");
    private float _lerpTimeZoom = 0;
    private Camera _mainCamera;

    
    private Playable _playable;

    private float _timeSinceLastBullet = 0;
    [Header("Objects")]
    public Animator animator;
    public ParticleSystem flare, tracers;
    public Light myLight;
    public GameObject bulletHole;
    
    [Header("Parameters")]
    public float minTimeBetweenBullets = 0.3f;
    public float normalFov = 60f, zoomedFov = 20f;
    public float zoomSpeed = 1;

    void Start()
    {
        _mainCamera = Camera.main;
        _playable = GetComponent<Playable>();
    }

    void Update()
    {
        // update time since last bullet 
        _timeSinceLastBullet += Time.fixedDeltaTime;

        UpdateZoomState();

        // only fire if in aiming position and got left click
        if (Input.GetMouseButton(0) && _playable.isAiming) Fire();
    }

    /// <summary>
    /// fire, check bullet impact and add VFX
    /// </summary>
    private void Fire()
    {
        if (_timeSinceLastBullet >= minTimeBetweenBullets)
        {
            _timeSinceLastBullet = 0;
            // muzzleflash and light
            flare.Emit(1);
            myLight.enabled = true;
            StartCoroutine(TurnOffLightAfter(myLight, 0.1f));
            Instantiate(bulletHole, _playable.GunTargetRaycastHit.point + (_playable.GunTargetRaycastHit.normal * 0.0001f),
                Quaternion.FromToRotation (Vector3.up, _playable.GunTargetRaycastHit.normal));
        }
    }

    /// <summary>
    /// Right click to zoom 
    /// </summary>
    private void UpdateZoomState()
    {
        if (Input.GetMouseButton(1))
        {
            _lerpTimeZoom += Time.deltaTime * zoomSpeed;
            _lerpTimeZoom = Mathf.Clamp(_lerpTimeZoom, 0, 1);
        }
        else
        {
            _lerpTimeZoom -= Time.deltaTime * zoomSpeed;
            _lerpTimeZoom = Mathf.Clamp(_lerpTimeZoom, 0, 1);
        }

        _mainCamera.fieldOfView = Mathf.Lerp(normalFov, zoomedFov, _lerpTimeZoom);
    }

    /// <summary>
    /// turn light off after time
    /// </summary>
    /// <param name="light"></param>
    /// <param name="time"></param>
    /// <returns></returns>
    IEnumerator TurnOffLightAfter(Light light, float time)
    {
        yield return time;
        light.enabled = false;
    }
}