using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[CreateAssetMenu(menuName = "EnemyBulletPattern")]

/*“G‚Ì’e‚É•K—v‚È\¬—v‘f*/
public class EnemyBulletPatarn : ScriptableObject
{
    /*’e‚ª”ò‚ñ‚Å‚¢‚­Œü‚«*/
    [SerializeField] private float direction;
    public float Direction { get => direction; set => direction = value; }


    /*’e‚ª”ò‚ñ‚Å‚¢‚­ƒXƒs[ƒh*/
    [SerializeField] private float speed;
    public float Speed { get => speed; set => speed = value; }


    /*“¯Žž‚É‚Ç‚ê‚­‚ç‚¢Œ‚‚Ä‚é‚©*/
    [SerializeField] private int count;
    public int Count { get => count; set => speed = value; }


    /*“¯Žž”­ŽË‚·‚éŽž‚É‚Ç‚ê‚­‚ç‚¢‚ÌŠpŠÔŠu‚É‚·‚é‚©*/
    [SerializeField] private float angle;
    public float Angle { get => angle; set => angle = value; }


    /*/ƒvƒŒƒCƒ„[‚ð‘_‚¤‚©‚Ç‚¤‚©*/
    [SerializeField] private bool isAimPlayer;
    public bool IsAimPlayer { get => isAimPlayer; set => isAimPlayer = value; }
}
