using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageControl : MonoBehaviour
{
    [SerializeField] public ObjectPool p_Bulletpool = null;
    [SerializeField] public PlayerControl playerObj = null;

    private float stageSpeed = 5.0f;
    private static StageControl instance;


    public static StageControl Instance { get => instance; }
    private void Awake()
    {
        instance = this.GetComponent<StageControl>();
    }



    private void Start()
    {
        instance = this.GetComponent<StageControl>();
    }

    private void Update()
    {
        PlayerMoveInput();
    }

    /*“ü—Í²ˆÚ“®‚ğAPlayerControl‚ÌPlayerMove(Vector3)ŠÖ”‚ÉQÆ*/
    private void PlayerMoveInput()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * stageSpeed);

        var inputX = Input.GetAxisRaw("Horizontal");
        var inputZ = Input.GetAxisRaw("Vertical");
        playerObj.PlayerMove(new(inputX, 0, inputZ));

        if(Input.GetMouseButton(0))
        {
            print("‰Ÿ‚³‚ê‚Ä‚é‚æp");
            playerObj.Shot();
        }
    }
}
