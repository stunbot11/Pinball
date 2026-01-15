using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControlls : MonoBehaviour
{
    private GameManager gameManager;
    [HideInInspector] public Controls controls;

    public GameObject[] rightPaddles;
    public GameObject[] leftPaddles;

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


    public void sceneStart()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        controls = new Controls();
        controls.Paddles.Enable();
        rightPaddles = GameObject.FindGameObjectsWithTag("Paddle Right");
        leftPaddles = GameObject.FindGameObjectsWithTag("Paddle Left");
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
            rightUses--;
            gameManager.RUtxt.text = "Right Uses Left: " + rightUses;
            foreach (GameObject paddle in rightPaddles)
                paddle.GetComponent<Paddle>().Move(true);
        }
        else if (phase.canceled)
        {
            foreach (GameObject paddle in rightPaddles)
                paddle.GetComponent<Paddle>().Move(false);
        }

        //if (phase.started && rightUses > 0 && ballInPlay)
        //{
        //    for (int i = 0; i < PRRB.Length; i++)
        //        PRRB[i].GetComponent<HingeJoint2D>().useMotor = false;
        //    rightUses--;
        //    gameManager.RUtxt.text = "Right Uses Left: " + rightUses;
        //}

        //if (phase.canceled)
        //{
        //    for (int i = 0; i < PRRB.Length; i++)
        //        PRRB[i].GetComponent<HingeJoint2D>().useMotor = true;
        //}
    }

    public void left(InputAction.CallbackContext phase)
    {
        if (phase.started && leftUses > 0 && ballInPlay)
        {
            leftUses--;
            gameManager.LUtxt.text = "Left Uses Left: " + leftUses;
            foreach (GameObject paddle in leftPaddles)
                paddle.GetComponent<Paddle>().Move(true);
        }
        else if (phase.canceled)
        {
            foreach (GameObject paddle in leftPaddles)
                paddle.GetComponent<Paddle>().Move(false);
        }
        //if (phase.started && leftUses > 0 && ballInPlay)
        //{
        //    for (int i = 0; i < PLRB.Length; i++)
        //        PLRB[i].GetComponent<HingeJoint2D>().useMotor = false;
        //    leftUses--;
        //    gameManager.LUtxt.text = "Left Uses Left: " + leftUses;
        //}
            

        //if (phase.canceled)
        //{
        //    for (int i = 0; i < PLRB.Length; i++)
        //        PLRB[i].GetComponent<HingeJoint2D>().useMotor = true;
        //}
    }
}
