using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   //public float horizontal;
    //public float vertical;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;
    public float speed = 6f;

    public Vector3 velocity;

    public float gravity = -9.81f;
    public float sens = 100.0f;
    public float SmoothAngle = -.4f;
    float turnSmoothVel;
    public Transform cam;

    public Animator anim;

    public CharacterController controller;

    bool isGrounded;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Physics.gravity = new Vector3(0, -1.0F, 0);
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        

        if(isGrounded && velocity.y < 0){
            velocity.y = -2f;
        }

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical);


        if(horizontal != 0 || vertical != 0){
            anim.SetBool("Forward", true);
        }
        else{
            anim.SetBool("Forward", false);
        }

        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

        if(direction.magnitude >= 0.1f){
           //float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
          // float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, SmoothAngle);
           // transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);

        }
        
         float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVel, SmoothAngle);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        if(isGrounded)
        {
            anim.SetBool("isGrounded", true);
        }
        else{
            anim.SetBool("isGrounded", false);
        }


    }



    void OnDrawGizmos() {
            

        Gizmos.color = Color.green;
        Gizmos.DrawSphere(groundCheck.position, groundDistance);

        }
}