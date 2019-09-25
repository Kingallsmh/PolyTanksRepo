using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BaseInput))]
[RequireComponent(typeof(Rigidbody))]
public class DirectionalMoveComponentNew : MonoBehaviour
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
        
        move = (cam.transform.up * input.Directional.y + cam.transform.right * input.Directional.x);
        move.Normalize();
        move = model.InverseTransformDirection(move);
        move.y = 0;
        move = model.TransformDirection(move);
        model.LookAt(model.position + move, model.up);
        
        debugMove = model.forward;
    }

    void RotateModel()
    {
        if (Physics.Raycast(model.position, -model.transform.up, out RaycastHit hit))
        {
            if(hit.transform != transform)
            {
                model.rotation = Quaternion.FromToRotation(model.up, hit.normal) * model.rotation;
            }            
        }
        
    }


    public Vector3 debugMove;
    public Vector3 debugmove2;
    void UpdateMovement()
    {        
        if(move.magnitude > 0)
        {
            rb.AddForce(model.forward * currentSpeed, ForceMode.VelocityChange);            
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
