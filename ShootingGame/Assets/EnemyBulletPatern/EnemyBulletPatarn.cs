using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "EnemyBulletPattern")]

/*敵の弾に必要な構成要素*/
public class EnemyBulletPatarn : ScriptableObject
{
    /*弾が飛んでいく向き*/
    [SerializeField] private float direction;
    public float Direction { get => direction; set => direction = value; }


    /*弾が飛んでいくスピード*/
    [SerializeField] private float speed;
    public float Speed { get => speed; set => speed = value; }


    /*同時にどれくらい撃てるか*/
    [SerializeField] private int count;
    public int Count { get => count; set => speed = value; }


    /*同時発射する時にどれくらいの角間隔にするか*/
    [SerializeField] private float angle;
    public float Angle { get => angle; set => angle = value; }


    /*/プレイヤーを狙うかどうか*/
    [SerializeField] private bool isAimPlayer;
    public bool IsAimPlayer { get => isAimPlayer; set => isAimPlayer = value; }
}
