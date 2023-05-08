using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CharacterController))]

public class AdvancedPlayerController : MonoBehaviour
{
    CharacterController characterController;

    [Header("Movement Settings")]
    [SerializeField] float walk_speed = 3f;
    [SerializeField] float run_speed = 6f;
    [SerializeField] float jumpSpeed = 8.0f;
    [SerializeField] float gravity = 9.81f;

    [Header("Camera Settings")]
    [SerializeField] float turnSmoothingTime = 0.1f;
    [SerializeField] Transform cam;

    Animator anim;
    public int happiness = 1;

    float Hor_move;
    float Ver_move;
    float turnSmoothVel;
    string currState;


    Vector3 MoveDir = Vector3.zero;

    void Awake()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        if (characterController.isGrounded)
        {
            Hor_move = Input.GetAxis("Horizontal");
            Ver_move = Input.GetAxis("Vertical");
            MoveDir = Vector3.zero;
            Vector3 Direction = new Vector3(Hor_move, 0.0f, Ver_move);

            if (Direction.magnitude >= 0.1f)
            {
                float targetAngle = Mathf.Atan2(Direction.x, Direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, turnSmoothingTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                MoveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

                if (Input.GetKey(KeyCode.LeftShift))
                {
                    Animating("Run");
                }
                else
                {
                    Animating("Walk");
                }
            }
            else
            {
                if(happiness == 0)
                {
                    Animating("Idle_Scared");
                }
                else if (happiness == 1)
                {
                    Animating("Idle");
                }
                else
                {
                    Animating("Idle_Happy");
                }
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                MoveDir *= run_speed;
            }
            else
            {
                MoveDir *= walk_speed;
            }

            if (Input.GetButton("Jump"))
            {
                MoveDir.y = jumpSpeed;
            }
        }
        else
        {
            Animating("Jump");
        }
        MoveDir.y -= gravity * Time.deltaTime;
        characterController.Move(MoveDir * Time.deltaTime);
    }

    void Animating(string newstate)
    {
        if (currState == newstate) return;

        anim.Play(newstate);

        currState = newstate;
    }

    public IEnumerator ReturnToDefaultIdle()
    {
        yield return new WaitForSeconds(6);
        happiness = 1;
    }
}