using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlls : MonoBehaviour
{
    private GameManager gameManager;
    [HideInInspector] public Controls controls;

    public Rigidbody2D PLRB;
    public Rigidbody2D PRRB;

    public int maxLU;
    [HideInInspector] public int leftUses;
    public int maxRU;
    [HideInInspector] public int rightUses;

    public float paddleForce;

    public GameObject ball;

    public float minLVel;
    public float maxLVel;

    public Animator lAnim;
    public float launchTime;
    public bool ballInPlay;


    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        controls = new Controls();
        controls.Paddles.Enable();
        controls.Paddles.Launch.canceled += launch;
        controls.Paddles.Launch.started += launch;
        controls.Paddles.Right.started += right;
        controls.Paddles.Right.canceled += right;
        controls.Paddles.Left.started += left;
        controls.Paddles.Left.canceled += left;
        maxLU = gameManager.stats.lFlips;
        maxRU = gameManager.stats.rFlips;
        leftUses = maxLU;
        rightUses = maxRU;
        gameManager.LUtxt.text = "Left Uses Left: " + leftUses;
        gameManager.RUtxt.text = "Right Uses Left: " + rightUses;
    }

    private void FixedUpdate()
    {
        if (controls.Paddles.Right.IsInProgress())
            PRRB.AddForceY(paddleForce);

        if (controls.Paddles.Left.IsInProgress())
            PLRB.AddForceY(paddleForce);

        if (controls.Paddles.Launch.inProgress && !ballInPlay)
            launchTime += Time.deltaTime;
    }

    public void launch(InputAction.CallbackContext phase)
    {
        if (phase.started && !ballInPlay)
            lAnim.SetTrigger("Up");
        if (phase.canceled && !ballInPlay)
        {
            ball.GetComponent<Rigidbody2D>().linearVelocityY = Mathf.Lerp(minLVel, maxLVel, launchTime);
            lAnim.SetTrigger("Down");
            launchTime = 0;
        }
    }
    
    public void right(InputAction.CallbackContext phase)
    {
        if (phase.started && rightUses > 0 && ballInPlay)
        {
            PRRB.GetComponent<HingeJoint2D>().useMotor = false;
            rightUses--;
            gameManager.RUtxt.text = "Right Uses Left: " + rightUses;
        }

        if (phase.canceled)
            PRRB.GetComponent<HingeJoint2D>().useMotor = true;
    }

    public void left(InputAction.CallbackContext phase)
    {
        if (phase.started && leftUses > 0 && ballInPlay)
        {
            PLRB.GetComponent<HingeJoint2D>().useMotor = false;
            leftUses--;
            gameManager.LUtxt.text = "Left Uses Left: " + leftUses;
        }
            

        if (phase.canceled)
            PLRB.GetComponent<HingeJoint2D>().useMotor = true;
    }
}
