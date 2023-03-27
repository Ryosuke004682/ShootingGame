using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("Player�̃p�����[�^�[�ݒ�")]
    [Space]
    [SerializeField] private float moveSpeed = 5.0f;


    [Header("Player�̉ғ��̈� - ������ -")]
    [Space]
    [SerializeField] private float minPositionX = -3.0f;
    [SerializeField] private float maxPositionX = 3.0f;
    [Header("Player�̉ғ��̈� - �c���� -")]

    [Space]
    [SerializeField] private float minPositionZ = -4.0f;
    [SerializeField] private float maxPositionZ = 4.0f;

    private float interval = 0;

    private ObjectPool bulletPool;


    private void Start()
    {
        bulletPool = StageControl.Instance.p_Bulletpool;
    }

    private void Update()
    {
        interval -= Time.deltaTime;
    }

    private void FixedUpdate()
    {
    }

    /* �ړ�������������B */
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
                bullet.GetComponent<BulletMove>().speed = 10;

            interval = 0.1f;
        }
    }
}
