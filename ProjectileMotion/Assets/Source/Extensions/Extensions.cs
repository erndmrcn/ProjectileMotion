using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extensions
{
    public static Vector3 CheckIsNaN(this Vector3 value)
    {
        if (float.IsNaN(value.x))
        {
            value.x = 0;
        }

        if (float.IsNaN(value.y))
        {
            value.y = 0;
        }

        if (float.IsNaN(value.z))
        {
            value.z = 0;
        }

        return value;
    }
}
