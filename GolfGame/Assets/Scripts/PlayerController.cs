using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   public float horz;
    public float vert;
    public float speed = 0.0000001f;
    public float xRange = 2500000000.0f;
    public float sens = 100.0f;

    public Rigidbody rb;
    public float jumpAmount = 10000; 
    public Vector2 turn;
    public GameObject obj;
    public Camera userCamera;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        //Physics.gravity = new Vector3(0, -1.0F, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            rb.AddForce(Vector2.up * jumpAmount, ForceMode.Impulse);
        } 
        horz = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * Time.deltaTime * speed * horz);
        vert = Input.GetAxis("Vertical");
        transform.Translate(Vector3.forward * Time.deltaTime * speed * vert);

        turn.x += Input.GetAxis("Mouse X") * sens;
        turn.y += Input.GetAxis("Mouse Y") * sens;
        transform.localRotation = Quaternion.Euler(0, turn.x, 0);

        userCamera.transform.RotateAround(obj.transform.position, Vector3.up, 20 * Time.deltaTime);

    }
}