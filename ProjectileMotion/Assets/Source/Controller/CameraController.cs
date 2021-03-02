using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // handle camera controls(transfroms) here
    public Vector3 movement = Vector3.zero;
    public Vector3 rotation = Vector3.zero;
    [SerializeField] [Range(1.0f, 10.0f)] private float camSpeed = 2.0f;
    private void Update()
    {
        // WASD movement XZ rotation
        // MiddleMouseClick -> rotation up/down & rigt/left w.r.t. X & Y
        movement = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        rotation = new Vector3(Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X"), 0.0f);

        if (movement != Vector3.zero)
        {
            transform.position += movement * camSpeed * Time.deltaTime;
        }

        if (Input.GetMouseButton(2))
        {
            transform.Rotate(rotation);
        }
    }

}
