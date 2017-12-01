using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    protected override void Update()
    {
        base.Update();

        if(Input.GetKeyDown(KeyCode.A))
        {
            StartAction(0);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            StartAction(1);
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            StartAction(2);
        }

    }
}
