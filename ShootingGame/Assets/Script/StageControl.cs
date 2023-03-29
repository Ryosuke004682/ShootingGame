using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageControl : MonoBehaviour
{
    [SerializeField] public ObjectPool p_Bulletpool = null;
    [SerializeField] public ObjectPool e_BulletPool = null;
    [SerializeField] public PlayerControl playerObj = null;
    [SerializeField] public StageSequencer sequence = null;

    private float stageSpeed = 5.0f;


    private static StageControl instance;

    public  static StageControl Instance { get => instance; }
    private void Awake()
    {
        instance = this.GetComponent<StageControl>();
    }

    private void Start()
    {
        sequence.Load();

    }

    private void Update()
    {
        PlayerMoveInput();
    }

    /*入力軸移動を、PlayerControlのPlayerMove(Vector3)関数に参照*/
    private void PlayerMoveInput()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * stageSpeed);

        var inputX = Input.GetAxisRaw("Horizontal");
        var inputZ = Input.GetAxisRaw("Vertical");
        playerObj.PlayerMove(new(inputX, 0, inputZ));

        if(Input.GetMouseButton(0))
        {
            print("押されてるよp");
            playerObj.Shot();
        }
    }
}
