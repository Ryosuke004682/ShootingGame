using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Playerのパラメーター設定")]
    [Space]
    [SerializeField] private float moveSpeed = 5.0f;


    [Header("Playerの稼働領域 - 横方向 -")]
    [Space]
    [SerializeField] private float minPositionX = -3.0f;
    [SerializeField] private float maxPositionX = 3.0f;
    
    
    [Header("Playerの稼働領域 - 縦方向 -")]
    [Space]
    [SerializeField] private float minPositionZ = -4.0f;
    [SerializeField] private float maxPositionZ = 4.0f;


    [Header("視点切り替え")]
    [Space]
    [SerializeField] private Camera fpsCam;
    [SerializeField] private Camera tpsCam;


    private float interval = 0;
    private ObjectPool bulletPool;


    private void Start()
    {
        bulletPool = StageControl.Instance.p_BulletPool;

        fpsCam.enabled = false;
        tpsCam.enabled = true;
    }

    private void Update()
    {
        interval -= Time.deltaTime;

        if(Input.GetKeyDown(KeyCode.F))
        {
            fpsCam.enabled = !fpsCam.enabled;
            tpsCam.enabled = !tpsCam.enabled;
        }
    }



    /* 移動制限をかける。 */
    public void PlayerMove(Vector3 moveDirection)
    {
        transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        var thisPosition = transform.localPosition;
        thisPosition.x = Mathf.Clamp(thisPosition.x, minPositionX, maxPositionX);
        thisPosition.z = Mathf.Clamp(thisPosition.z, minPositionZ, maxPositionZ);

        transform.localPosition = thisPosition;
    }

    public void Shot()
    {
        if (interval <= 0)
        {
            var bullet = bulletPool.Launch(transform.position , 0);

            if (bullet != null)
                bullet.GetComponent<BulletMove>().speed = 30;

            interval = 0.1f;
        }
    }
}
