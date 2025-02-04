using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleSystem : SingletonLazy<JewelData>
{
    private Vector2[] sphereVectors;

    public ParticleSystem()
    {
        sphereVectors = new Vector2[360];

        for (int i = 0; i < sphereVectors.Length; ++i)
        {
            sphereVectors[i] = Random.onUnitSphere;
        }
    }

    public Vector2[] Sample(int cnt)
    {
        Vector2[] ret = new Vector2[cnt];

        for (int i = 0; i < cnt; ++i)
        {
            int idx = (int)Random.Range(0, sphereVectors.Length - 1);
            ret[idx] = sphereVectors[idx];
        }

        return ret;
    }
}
