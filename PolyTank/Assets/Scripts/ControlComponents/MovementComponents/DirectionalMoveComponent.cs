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
    public float minSpeed = 0.5f;
    public float currentSpeed = 0.5f;
    public float acceleration = 0.1f;
    public float maxSpeed = 1;
    public float gravity = 1;
    public float turnRate = 0.01f;
    public float reverseTolerance = 1.2f;
    public float turnTolerance = 1.8f;
    public float boostAmount = 30;

    Vector3 lastDirection;
    // Start is called before the first frame update
    void Start()
    {
        input = GetComponent<BaseInput>();
        rb = GetComponent<Rigidbody>();        
    }

    private void Update()
    {
        model.transform.localPosition = transform.localPosition - new Vector3(0, 1);
        UpdateRotation();
        Boost();
    }

    void FixedUpdate()
    {        
        UpdateMovement();
        RotateModel();
        AdjustSpeed();
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
    }

    void RotateModel()
    {
        if (move.magnitude >= 1f)
        {
            //model.transform.rotation = Quaternion.Slerp(model.transform.rotation, Quaternion.LookRotation(move), turnRate);
            if ((model.transform.forward + move).magnitude > reverseTolerance)
            {
                model.transform.rotation = Quaternion.RotateTowards(model.transform.rotation, Quaternion.LookRotation(move), turnRate);
                lastDirection = model.transform.forward;
            }
            else
            {
                model.transform.rotation = Quaternion.RotateTowards(model.transform.rotation, Quaternion.LookRotation(-move), turnRate);
                lastDirection = -model.transform.forward;
            }
        }
    }


    public Vector3 debugMove;
    void UpdateMovement()
    {        
        if(move.magnitude > 0)
        {
            if ((model.transform.forward + move).magnitude > reverseTolerance)
            {
                rb.AddForce(model.transform.forward * currentSpeed, ForceMode.VelocityChange);
            }
            else
            {
                rb.AddForce(model.transform.forward * -currentSpeed, ForceMode.VelocityChange);
            }
        }
        rb.AddForce(Vector3.down * gravity, ForceMode.Acceleration);

        //if (rb.velocity.magnitude > maxSpeed)
        //{
        //    rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxSpeed);
        //}
    }

    public float vel;
    void AdjustSpeed()
    {
        vel = rb.velocity.magnitude;
        if ((lastDirection + move).magnitude > turnTolerance)
        {
            if(currentSpeed < maxSpeed)
            {
                currentSpeed += acceleration/10;
            }
            else
            {
                currentSpeed = maxSpeed;
            }
        }
        else
        {
            currentSpeed = minSpeed;
        }
    }

    void Boost()
    {
        if (input.Btn1)
        {
            rb.AddForce(model.transform.forward * boostAmount, ForceMode.VelocityChange);
            Debug.Log(rb.velocity.magnitude);
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
