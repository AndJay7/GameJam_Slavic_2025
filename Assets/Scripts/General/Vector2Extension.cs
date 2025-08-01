using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Vector2Extension
{
    public static bool IsValid(this Vector2Int vector)
    {
        return vector.x >= 0 && vector.y >= 0;
    }
}
