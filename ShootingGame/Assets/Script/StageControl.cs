using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StageControl : MonoBehaviour
{
    [SerializeField] public ObjectPool particlePool = null;
    [SerializeField] public ObjectPool p_BulletPool = null;
    [SerializeField] public ObjectPool e_BulletPool = null;
    [SerializeField] public Transform  enemys       = null;
    [SerializeField] public PlayerControl playerObj = null;
    [SerializeField] public StageSequencer sequence = null;
    

    public float stageSpeed        = 5.0f;
    private float stageProgressTime = 0.0f;

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
    }

    private void Update()
    {
        sequence.Step(stageProgressTime);
        stageProgressTime += Time.deltaTime;

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
