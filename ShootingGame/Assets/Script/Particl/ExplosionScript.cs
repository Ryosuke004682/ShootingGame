using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionScript : MonoBehaviour
{
    ParticleSystem ps;
    PoolContent pool;


    public void Awake()
    {
        ps   = transform.GetComponent<ParticleSystem>();
        pool = transform.GetComponent<PoolContent>();
    }

    public void PlayPartcleSystem()
    {
        ps.Play();
        StartCoroutine(TimeWait());
    }

    IEnumerator TimeWait()
    {
        yield return new WaitForSeconds(0.3f);

        ps.Stop();
        pool.Hide();
    }
}
