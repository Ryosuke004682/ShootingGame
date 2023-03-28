using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int hitPoint = 3;
    [SerializeField] private float hitFlashTime = 0.05f;

    private Material flashMaterial = null;


    private void Start()
    {
        flashMaterial = transform.GetComponentsInChildren<Renderer>()[0].material;
        flashMaterial.EnableKeyword("_EMISSION");
    }


    public void Hide()
    {
        Destroy(gameObject);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerBullet"))
        {
            var poolObj = other.GetComponent<PoolContent>();
            poolObj.Hide();

            hitPoint -= 1;

            if (hitPoint <= 0)
            {
                Hide();
            }
            else
            {
                StartCoroutine(HitFlashTime());
            }
        }
    }

    /*マテリアルを点滅させて当たってる感を出す*/
    private IEnumerator HitFlashTime()
    {
        flashMaterial.SetColor("_EmissionColor", Color.red);
        yield return new WaitForSeconds(hitFlashTime);
        flashMaterial.SetColor("_EmissionColor", Color.black);
    }

}
