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


    [Header("���_�؂�ւ�")]
    [Space]
    [SerializeField] private Camera fpsCam;
    [SerializeField] private Camera tpsCam;



    public bool isDead = false;

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
                bullet.GetComponent<BulletMove>().speed = 30;

            interval = 0.1f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EnemyBullet"))
        {
            other.GetComponent<PoolContent>().Hide();
            isDead = true;
        }
    }

    public void SetupForPlay()
    {
        interval = 0;
        isDead = false;
        transform.localPosition = new Vector3(0.0f,0.0f,-3.5f);
        transform.localEulerAngles = new Vector3(0, 0, 0);
    }

}
