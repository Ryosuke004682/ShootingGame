using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int hitPoint = 3;
    [SerializeField] private float hitFlashTime = 0.05f;

    private Material flashMaterial = null;
    private ObjectPool enemyBulletPool;

    private void Start()
    {
        flashMaterial = transform.GetComponentsInChildren<Renderer>()[0].material;
        flashMaterial.EnableKeyword("_EMISSION");


        enemyBulletPool = StageControl.Instance.e_BulletPool;
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

    public void Shot(EnemyBulletPatarn _o)
    {
        var stageInstanceObj = StageControl.Instance.playerObj;



        var angleDistance = (_o.Count - 1) / 2.0f;

        //暗黙な型指定すると、以下のVector3.SignedAngleで弾かれるので指定する。
        float baseDirection = 0;

        //Playerを狙うならPlayerのいる方向に、狙ってないなら自分が向いてる方向に弾を撃つ。
        if (_o.IsAimPlayer)
        {
            baseDirection = Vector3.SignedAngle
                           (Vector3.forward, stageInstanceObj.transform.localPosition - transform.localPosition,
                           Vector3.up);
        }
        else { baseDirection = _o.Direction; }


        for (var i = 0; i < _o.Count; i++)
        {
            var d = ((i - angleDistance) * _o.Angle);
            var obj = enemyBulletPool.Launch(transform.position, d + baseDirection);

            if (obj != null)
                obj.GetComponent<BulletMove>().speed = _o.Speed;
        }

    }


}
