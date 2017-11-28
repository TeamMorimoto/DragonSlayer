using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Timer : MonoBehaviour
{
    public enum Status
    {
        StandBy,
        Run,
        Finished
    }

    public delegate void EventTimerFinish(float exccess);

    //残り時間
    [SerializeField]
    float timeRemain;
    public float TimeRemain { get { return timeRemain; } }

    //過剰時間
    [SerializeField]
    float timeExccess;
    public float TimeExccess{get{ return timeExccess; }}

    //終わったときに呼び出すイベント
    EventTimerFinish eventTimerFinish;
    public void SetEventTimerFinish(EventTimerFinish eventTimerFinish) { this.eventTimerFinish = eventTimerFinish; }
    public void ResetEventTimerFinish() { eventTimerFinish = null; }

    //現在の状態
    [SerializeField]
    Status status;

    public Status CurrentStatus { get { return status; } }

    public bool isStandingBy { get { return status == Status.StandBy; } }    
    public bool isRunning    { get { return status == Status.Run; } }
    public bool isFinished   { get { return status == Status.Finished; } }


    private void Awake()
    {
        ResetTimer();
        eventTimerFinish = null;
    }

    private void Update()
    {
        if(isRunning)
        {
            timeRemain -= Time.deltaTime;

            if(timeRemain<=0)
            {
                //タイマーの終了
                timeExccess = -timeRemain;

                status = Status.Finished;

                //タイマー使用部分で終了時呼び出し関数を登録していると実行する
                if(eventTimerFinish!=null)
                {
                    eventTimerFinish(timeExccess);
                }
            }
        }
    }

    //タイマーの開始
    public bool StartTimer(float time)
    {
        if (time > 0)
        {
            if (isStandingBy || isFinished)
            {
                timeRemain = time;
                timeExccess = 0.0f;

                status = Status.Run;
                return true;
            }
            else
            {
                Debug.Log("タイマーが稼働中なので新しくスタートできない");
            }
        }
        else
        {
            Debug.Log("タイマーの初期時間が不正");
        }
     
        return false;        
    }

    //状態の初期化
    public void ResetTimer()
    {
        timeRemain = 0;
        timeExccess = 0;
        status = Status.StandBy;
    }


}
