using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "EnemyBulletPattern")]

/*�G�̒e�ɕK�v�ȍ\���v�f*/
public class EnemyBulletPatarn : ScriptableObject
{
    /*�e�����ł�������*/
    [SerializeField] private float direction;
    public float Direction { get => direction; set => direction = value; }


    /*�e�����ł����X�s�[�h*/
    [SerializeField] private float speed;
    public float Speed { get => speed; set => speed = value; }


    /*�����ɂǂꂭ�炢���Ă邩*/
    [SerializeField] private int count;
    public int Count { get => count; set => speed = value; }


    /*�������˂��鎞�ɂǂꂭ�炢�̊p�Ԋu�ɂ��邩*/
    [SerializeField] private float angle;
    public float Angle { get => angle; set => angle = value; }


    /*/�v���C���[��_�����ǂ���*/
    [SerializeField] private bool isAimPlayer;
    public bool IsAimPlayer { get => isAimPlayer; set => isAimPlayer = value; }
}
