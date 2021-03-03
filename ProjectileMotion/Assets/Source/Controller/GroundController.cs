using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour
{
    public bool CheckRayHit(Ray ray, out RaycastHit hitInfo)
    {
        return Physics.Raycast(ray, out hitInfo);
    }
}
