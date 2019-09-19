using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseInput : MonoBehaviour
{
    Vector2 directional;
    bool btn1, btn1Held;
    bool btn2, btn2Held;
    bool btnStart, btnStartHeld;
    bool btnSelect, btnSelectHeld;

    public Vector2 Directional { get => directional; set => directional = value; }
    public bool Btn1 { get => btn1; set => btn1 = value; }
    public bool Btn1Held { get => btn1Held; set => btn1Held = value; }
    public bool Btn2 { get => btn2; set => btn2 = value; }
    public bool Btn2Held { get => btn2Held; set => btn2Held = value; }
    public bool BtnStart { get => btnStart; set => btnStart = value; }
    public bool BtnStartHeld { get => btnStartHeld; set => btnStartHeld = value; }
    public bool BtnSelect { get => btnSelect; set => btnSelect = value; }
    public bool BtnSelectHeld { get => btnSelectHeld; set => btnSelectHeld = value; }

    public virtual void ListenForInput()
    {

    }
} 

