using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public interface Attachment
    {
        void SetOwner(Character ch);
    }

    //行動を反映させるアイコン
    [SerializeField]
    ActionSequencer actionSequencer;

    //各種行動に関するパラメータ
    [SerializeField]
    ActionParamater AttackPram;
    [SerializeField]
    ActionParamater GuardParam;
    [SerializeField]
    ActionParamater DodgeParam;

    
    //行動中であるか
    [SerializeField]
    private bool isDuaringAction;
    protected bool IsDuaringAction { get { return isDuaringAction; } }


    protected virtual void Awake()
    {
        //必要なデータが全てそろっているかをチェック
        bool flag = false;
        if (actionSequencer == null) flag = true;
        if (AttackPram == null) flag = true;
        if (GuardParam == null) flag = true;
        if (DodgeParam == null) flag = true;

        if(flag)
        {
            //そろってない
            Debug.LogError("Characterクラス Awake 必要なデータがセットされていません");
            this.enabled = false;
            return;
        }

        isDuaringAction = false;
    }

    /// <summary>
    /// 行動を開始する
    /// </summary>
    /// <param name="n">行動の番号(種類)</param>
    public void StartAction(int n)
    {
        ActionParamater ap = null;
        switch(n)
        {
            case 0: ap = AttackPram;
                break; 
            case 1: ap = GuardParam;
                break;
            case 2: ap = DodgeParam;
                break;

            default:
                return;
        }

        actionSequencer.Activate(ap);

        isDuaringAction = true;
    }


    protected virtual void Update()
    {
        if(isDuaringAction)
        {
            if(actionSequencer.Active == false)
            {
                isDuaringAction = false;
            }
        }
    }

}
