using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionSequencer : MonoBehaviour
    ,Character.Attachment
{
    public delegate void EventOnChangeMode(Mode newMode,ActionParamater ap);

    public enum Mode
    {
        STAND_BY,
        PRELIMINARY_BEFORE,
        MAIN,
        PRELIMINARY_END,
        FINISHED
    }

    Timer timer;

    public float CurrentTimerProgress { get {return  timer.Progress; } }

    public bool Active { get {return  mode != Mode.STAND_BY && mode != Mode.FINISHED; } }

    [SerializeField]
    Mode mode;
    public Mode CurrentMode { get { return mode; } }

    //モードの変化に伴って実行するデリゲート
    List<EventOnChangeMode> eventOnChangeMode=new List<EventOnChangeMode>();
    public void AddEventOnChangeMode(EventOnChangeMode ev) { eventOnChangeMode.Add(ev); }

    //所有しているキャラクター
    [SerializeField]
    Character owner;
    public Character Owner { get { return owner; } }
    void Character.Attachment.SetOwner(Character ch) { owner = ch; }

    
    ActionParamater currentActionParameter;
    public ActionParamater CurrentActionParamater { get { return currentActionParameter; } }

    private void Awake()
    {
        timer = this.gameObject.AddComponent<Timer>();
        timer.SetEventTimerFinish(OnFinishTimer);
        mode=Mode.STAND_BY;
    }

    //アクティブにする
    public void Activate(ActionParamater ap,bool combo=false)
    {
        if (mode == Mode.STAND_BY || combo == true)
        {
            //状態初期化            
            mode = Mode.PRELIMINARY_BEFORE;

            //攻撃のコンボならタイマーをリセットする　
            //でないとタイマーのセットでエラーになる
            if (combo == true)
            {
                timer.ResetTimer();
            }

            //稼働時間を設定
            currentActionParameter = ap;
            timer.StartTimer(ap.Time_preliminary_before);

            if (eventOnChangeMode != null)
            {
                foreach (EventOnChangeMode ev in eventOnChangeMode)
                {
                   ev(mode, currentActionParameter);
                }
            }
        }
    }

    //非アクティブにする
    public void Deactivate(bool force = false)
    {
        if (mode != Mode.STAND_BY || force)
        {
            timer.ResetTimer();
            mode=Mode.STAND_BY;
            currentActionParameter = null;
        }
    }

    //タイマーにデリゲートとして登録し
    //タイマーの終了時に呼び出してもらう関数
    private void OnFinishTimer(float exccess)
    {
        //次のモードへ
        switch (mode)
        {
            case Mode.PRELIMINARY_BEFORE:
                mode = Mode.MAIN;
                break;
            case Mode.MAIN:
                mode = Mode.PRELIMINARY_END;
                break;
            case Mode.PRELIMINARY_END:
                mode = Mode.FINISHED;
                break;          
            default:
                Debug.Log("バグ");
                break;
        }

        bool deactiveFlag = false;

        //次のモードに応じてタイマーを再スタート
        float time = -1;
        timer.ResetTimer();

        bool timerSetFlag = true;

        switch (mode)
        {            
            case Mode.MAIN:
     
                time = currentActionParameter.Time_valid;
                if (currentActionParameter.ActionContinulation)
                {
                    timerSetFlag = false;
                }
                
                break;

            case Mode.PRELIMINARY_END:               
                time=currentActionParameter.Time_preliminary_end;
                break;

            case Mode.FINISHED:
                deactiveFlag = true;
                break;
            default:
                Debug.Log("バグ");
                break;
        }

        if (timerSetFlag)
        {
            if (!deactiveFlag)
            {

                if (!(timer.StartTimer(time - exccess)))
                {
                    if (!timer.StartTimer(time))
                    {
                        Debug.Log("タイマーセット失敗");
                    }
                }
            }
        }

        //モードが変化したことを通知する
        if(eventOnChangeMode!=null)
        {
            foreach (EventOnChangeMode ev in eventOnChangeMode)
            {
                ev(mode, currentActionParameter);
            }
        }

        //非アクティブにする条件が整っていたら
        if(deactiveFlag)
        {
            Deactivate();//終了　非アクティブへ
        }

    }

    public void EndActionContinulation()
    {
        if (currentActionParameter == null|| currentActionParameter.ActionContinulation==false) return;

        bool timeSetFlag = false;
        float time = 0;
        bool changed = false;

        switch(mode)
        {
            case Mode.PRELIMINARY_BEFORE:
                mode = Mode.PRELIMINARY_END;
                time = currentActionParameter.Time_preliminary_end;
                timeSetFlag = true;
                changed = true;
                break;
            case Mode.MAIN:
                mode = Mode.PRELIMINARY_END;
                time = currentActionParameter.Time_preliminary_end;
                timeSetFlag = true ;
                changed = true;
                break;
        }

        //モードが変化したことを通知する
        if (changed)
        {
            if (eventOnChangeMode != null)
            {
                foreach (EventOnChangeMode ev in eventOnChangeMode)
                {
                    ev(mode, currentActionParameter);
                }
            }
        }

        if (timeSetFlag)
        {
            timer.ResetTimer();
            timer.StartTimer(time);
        }
    }

  

}
