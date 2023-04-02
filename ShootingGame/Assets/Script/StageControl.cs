using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StageControl : MonoBehaviour
{
    /* 場面の状態管理 */
    public enum PlayStopCodeDef
    {
        PlayerDead,
        BossDefeat,
    }
    public PlayStopCodeDef playStop;



    [SerializeField] public ObjectPool particlePool = null;
    [SerializeField] public ObjectPool p_BulletPool = null;
    [SerializeField] public ObjectPool e_BulletPool = null;
    [SerializeField] public Transform  enemys       = null;
    [SerializeField] public PlayerControl playerObj = null;
    [SerializeField] public StageSequencer sequence = null;

    [SerializeField] private Text scoreValue;

    public  float stageSpeed        = 5.0f;
    private float stageProgressTime = 0.0f;

    public bool isPlay = false;
    public bool isBossDestroy = false;

    private int score = 0;

    private static StageControl instance;

    public  static StageControl Instance { get => instance; }
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        instance = this.GetComponent<StageControl>();
    }

    private void Start()
    {
        sequence.Load();
        sequence.StageReset();
        stageProgressTime = 0;

        isPlay = false;
    }

    private void Update()
    {
        /*Playerが死んだとき、ボスを倒したときの状態管理*/
        if(playerObj.isDead)
        {
            playStop = PlayStopCodeDef.PlayerDead;
            isPlay = false;
        }
        if(isBossDestroy)
        {
            playStop = PlayStopCodeDef.BossDefeat;
            isPlay = false;
        }


        if(isBossDestroy) isPlay = false;
        if (!isPlay) return;

        sequence.Step(stageProgressTime);
        stageProgressTime += Time.deltaTime;

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

    public void StageStart()
    {
        isPlay           = true;
        isBossDestroy    = false;
        playerObj.isDead = false;


        stageProgressTime = 0.0f;
        stageSpeed        = 0.0f;
        sequence.StageReset();
        playerObj.SetupForPlay();
        SetScore(0);
    }

    public void ResetStage()
    {
        BroadcastMessage("Hide" , SendMessageOptions.DontRequireReceiver);
        transform.position = Vector3.zero;
    }

    public void AddScore(int value)
    {
        SetScore(score + value);
    }

    public void SetScore(int value)
    {
        score = value;
        scoreValue.text = $"{score : 00000}";
    }
}
