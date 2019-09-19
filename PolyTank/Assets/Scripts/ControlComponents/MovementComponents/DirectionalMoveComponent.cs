using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseInput))]
[RequireComponent(typeof(Rigidbody))]
public class DirectionalMoveComponent : MonoBehaviour
{
    BaseInput input;
    Rigidbody rb;

    public Transform model;
    public Camera cam;
    public float speed = 1;
    public float maxSpeed = 1;
    public float gravity = 1;
    public float turnRate = 0.01f;
    public float reverseTolerance = 1.2f;

    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<BaseInput>();
        rb = GetComponent<Rigidbody>();        
    }

    private void Update()
    {
        model.transform.localPosition = transform.localPosition - new Vector3(0, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        UpdateRotation();
        UpdateMovement();
        
    }

    Vector3 move;
    void UpdateRotation()
    {
        Vector3 camForward = cam.transform.forward;
        camForward.y = 0;
        camForward.Normalize();
        Vector3 camRight = cam.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        move = (camForward * input.Directional.y) + (camRight * input.Directional.x);
        move.Normalize();
        debugMove = move;

        if (move.magnitude >= 1f)
        {
            //model.transform.rotation = Quaternion.Slerp(model.transform.rotation, Quaternion.LookRotation(move), turnRate);
            if ((model.transform.forward + move).magnitude > reverseTolerance)
            {
                model.transform.rotation = Quaternion.RotateTowards(model.transform.rotation, Quaternion.LookRotation(move), turnRate);
            }
            else
            {
                model.transform.rotation = Quaternion.RotateTowards(model.transform.rotation, Quaternion.LookRotation(-move), turnRate);
            }
        }
    }


    public Vector3 debugMove;
    void UpdateMovement()
    {        
        if(move.magnitude < maxSpeed && move.magnitude > 0)
        {
            if ((model.transform.forward + move).magnitude > reverseTolerance)
            {
                rb.AddForce(model.transform.forward * speed, ForceMode.VelocityChange);
            }
            else
            {
                rb.AddForce(model.transform.forward * -speed, ForceMode.VelocityChange);
            }
        }
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        }
    }

    //void UpdateMovement()
    //{
    //    Vector3 move = transform.TransformDirection(new Vector3(input.Directional.x, 0, input.Directional.y).normalized) * speed;
    //    debugMove = move;
    //    if (move.magnitude < maxSpeed)
    //    {
    //        rb.AddForce(move, ForceMode.VelocityChange);
    //    }
    //    if (rb.velocity.magnitude > maxSpeed)
    //    {

    //        rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
    //    }
    //}
}
