using UnityEngine;
public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    [SerializeField] bool IsWalking = false;
    [SerializeField] float ForwardSpeed = 1.0f;
    public float strafeSpeedClamp ;
    [SerializeField] float StrafeSpeed = 1.0f;
    [SerializeField] float ClampX = 3.0f;

    private InputControls Inputs = null;
    private Transform Self = null;
    public Animator PlayerAnim;
    private Rigidbody RB = null;
    private Collider Col = null;
    private float InputX = 0.0f;
    private Vector3 MovePos = Vector3.zero;
    Quaternion rot;
    public BubbleController handBubble;
    //public GameObject prefab;
    // bool isHold= true;

    private void Start() => Initialize();

    private void Initialize()
    {
        Self = transform;
        // Anim   = GetComponent<Animator>();
        RB = GetComponent<Rigidbody>();
        Col = GetComponent<Collider>();
        Inputs = GetComponentInChildren<InputControls>();
    }//Initialize() end

    private void Update()
    {
        if (UIManager.instance.gameState != GameState.GamePlay)
            return;

        if (IsWalking == false)
            IsWalking = Inputs.TouchDown;
        // Anim.SetBool("Walk", IsWalking);
        
        InputX = Inputs.Horizontal * StrafeSpeed;
        InputX = Mathf.Clamp(InputX,-strafeSpeedClamp,strafeSpeedClamp);
    }

    private void FixedUpdate()
    {
        if (UIManager.instance.gameState != GameState.GamePlay)
            return;
        if(Input.GetMouseButton(0))
        {
            IsWalking = true;
        }
        else
        {
            IsWalking = false;
            PlayerAnim.SetBool("isRunning",false);
        }

        if (IsWalking)
        {
            PlayerAnim.SetBool("isRunning", true);
            MovePos = Self.position + (new Vector3(InputX , 0, ForwardSpeed * Time.deltaTime) * Time.deltaTime);
            MovePos.x = Mathf.Clamp(MovePos.x, -ClampX, ClampX);
            rot = Quaternion.AngleAxis(InputX * StrafeSpeed, Vector3.up);
            RB.MovePosition(MovePos);
            PlayerRotation();

        }//if end
        else
            MovePos = Vector3.zero;
        if (IsWalking == true)
        {
            IsWalking = Inputs.TouchDown;
            PlayerAnim.SetBool("isRunning", true);
        }
        else
        {
            PlayerAnim.SetBool("isRunning", false);
        }
           
    }//FixedUpdate() end
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Finish")
        {
            UIManager.instance.gameWinPanel.SetActive(true);
            UIManager.instance.gameState = GameState.LevelComplete;

        }
    }
    private float currentRotation;
    void PlayerRotation()
    {
        currentRotation = Mathf.Atan(InputX / 1) * Mathf.Rad2Deg;
        currentRotation = Mathf.Clamp(currentRotation, -30, 30);
        var rotation = Quaternion.Euler(Vector3.up * currentRotation);
        //        Debug.LogError(rotation);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * 2.5f);
    }

}//class end
