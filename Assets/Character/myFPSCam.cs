using UnityEngine;

public class myFPSCam : MonoBehaviour
{
    [SerializeField] private GameObject _character;

    private Vector2 _mouseRotation;

    [SerializeField] private float sensitivity = 1.0f;

    private void Start()
    {
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Application.targetFrameRate = 120;
        _character = transform.parent.gameObject;
    }

    // Update is called once per frame
    private void Update()
    {
        // Retrieve mouse movement deltas and store it in Vector2
        var mouseRotationDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));

        // Apply user sensitivity
        mouseRotationDelta = Vector2.Scale(mouseRotationDelta, new Vector2(sensitivity, sensitivity));

        // Add delta to global mouse rotation
        _mouseRotation += mouseRotationDelta;

        // Make sure y rotation can't loop around us
        _mouseRotation.y = Mathf.Clamp(_mouseRotation.y, -90f, 90f);

        // Rotate camera for y rotation
        transform.Rotate(Vector3.right,-mouseRotationDelta.y);


        
        // We add x component a second time to have 2:1 sensitivity ratio, which is common in fps (cs:go, etc)
        _mouseRotation.x += mouseRotationDelta.x;

        // Rotate character for x rotation so is always faces forward (useless here)
        _character.transform.localRotation = Quaternion.AngleAxis(_mouseRotation.x, _character.transform.up);


        
        // If user wants to exit..
        if (Input.GetKeyDown("escape"))
            // .. we release the cursor
            Cursor.lockState = CursorLockMode.None;
    }
}