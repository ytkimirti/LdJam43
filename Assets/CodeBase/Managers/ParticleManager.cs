using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{

    public ParticleSystem[] particles;

    public static ParticleManager main;

    void Awake()
    {
        main = this;
    }

    public void play(Vector3 pos, Vector3 rot, int particleId)
    {
        ParticleSystem p = particles[particleId];
        p.transform.position = pos;
        p.transform.eulerAngles = rot;
        p.Play();

        if (p.transform.childCount != 0)
        {
            p.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        }
    }

    public void play(Vector3 pos, int particleId)
    {
        ParticleSystem p = particles[particleId];
        p.transform.position = pos;
        p.Play();

        if (p.transform.childCount != 0)
        {
            p.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        }
    }

    public void play(Vector3 pos, Vector3 rot, int particleId, Color col)
    {
        ParticleSystem p = particles[particleId];
        p.transform.position = pos;
        p.transform.eulerAngles = rot;
        p.startColor = col;
        p.Play();

        if (p.transform.childCount != 0)
        {
            p.transform.GetChild(0).GetComponent<ParticleSystem>().Play();
        }
    }
}
