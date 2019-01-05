using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct IntToVector2  {
    public int x, z;
    public IntToVector2(int x, int z)
    {
        this.x = x;
        this.z = z;
    }

    public static IntToVector2 operator + (IntToVector2 a, IntToVector2 b)
    {
        a.z += b.z;
        a.x += b.x;
        return a;
    }
}
