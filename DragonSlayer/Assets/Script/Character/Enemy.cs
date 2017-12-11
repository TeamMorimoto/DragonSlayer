using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy :Character
{

    Timer timer;

    bool isDuaringActionPrev=false;

    [SerializeField]
    float actionInterval = 5;

    [SerializeField]
    ActionSet actionSet;

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
                    int rand = Random.Range(0, 100);
                    ActionParamater act = actionSet.GetActionSetElement(rand);
                    StartAction(act);
                }
            }
        }

        isDuaringActionPrev = IsDuaringAction;
    }
}
