using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy :Character
{

    Timer timer;

    bool isDuaringActionPrev=false;

    [SerializeField]
    float actionInterval = 5;

    protected override void Awake()
    {
        base.Awake();

        timer = this.gameObject.AddComponent<Timer>();
        timer.StartTimer(actionInterval);

    }

    protected override void Update()
    {
        base.Update();

        if(!IsDuaringAction)
        {
            if (isDuaringActionPrev == true)
            {
                timer.StartTimer(actionInterval);
            }
            else
            {
                if (!(timer.isRunning))
                {
                    int rand = Random.Range(0, 3);
                    StartAction(rand);
                }
            }
        }

        isDuaringActionPrev = IsDuaringAction;
    }
}
