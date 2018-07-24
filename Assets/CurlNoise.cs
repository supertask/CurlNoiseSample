using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurlNoiseSample : MonoBehaviour {
    public static float EPSILON = 1e-3f;

    public static Vector3 CurlNoise(Vector3 coord)
    {
        Vector3 dx = new Vector3(EPSILON, 0.0f, 0.0f);
        Vector3 dy = new Vector3(0.0f, EPSILON, 0.0f);
        Vector3 dz = new Vector3(0.0f, 0.0f, EPSILON);

        Vector3 dpdx0 = new Vector3(Simplex.Noise.Generate(coord - dx), 0.0f, 0.0f);
        Vector3 dpdx1 = new Vector3(Simplex.Noise.Generate(coord + dx), 0.0f, 0.0f);
        Vector3 dpdy0 = new Vector3(0.0f, Simplex.Noise.Generate(coord - dy), 0.0f);
        Vector3 dpdy1 = new Vector3(0.0f, Simplex.Noise.Generate(coord + dy), 0.0f);
        Vector3 dpdz0 = new Vector3(0.0f, 0.0f, Simplex.Noise.Generate(coord - dz));
        Vector3 dpdz1 = new Vector3(0.0f, 0.0f, Simplex.Noise.Generate(coord + dz));

        float x = dpdy1.z - dpdy0.z + dpdz1.y - dpdz0.y;
        float y = dpdz1.x - dpdz0.x + dpdx1.z - dpdx0.z;
        float z = dpdx1.y - dpdx0.y + dpdy1.x - dpdy0.x;

        return new Vector3(x, y, z) / EPSILON * 2.0f;
    }
}
