using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : BaseInput
{
    public bool isGamepad = false;

    private void Update()
    {
        ListenForInput();
    }

    public override void ListenForInput()
    {
        if (isGamepad)
        {
            ListenForInputLoopGamepad();
        }
        else
        {
            ListenForInputLoop();
        }

    }

    void ListenForInputLoop()
    {
        Directional = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Btn1 = Input.GetButtonDown("Btn1");
        Btn1Held = Input.GetButton("Btn1");
        Btn2 = Input.GetButtonDown("Btn2");
        Btn2Held = Input.GetButton("Btn2");
    }

    void ListenForInputLoopGamepad()
    {
        Directional = new Vector2(Input.GetAxisRaw("HorizontalPAD"), Input.GetAxisRaw("VerticalPAD"));
        Btn1 = Input.GetButtonDown("Btn1PAD");
        Btn1Held = Input.GetButton("Btn1PAD");
        Btn2 = Input.GetButtonDown("Btn2PAD");
        Btn2Held = Input.GetButton("Btn2PAD");
    }
}
