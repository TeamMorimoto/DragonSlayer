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

        if(Input.GetKeyUp(KeyCode.S))
        {
            EndAction();
        }

        if(Input.GetKeyDown(KeyCode.D))
        {
            StartAction(2);
        }
    }

    public void StartAction(int n)
    {
        ActionParamater ap = null;

        switch (n)
        {
            case 0:
                ap = AttackPram;
                break;
            case 1:
                ap = GuardParam;
                break;
            case 2:
                ap = DodgeParam;
                break;

            default:
                return;
        }

        StartAction(ap);
    }
}
