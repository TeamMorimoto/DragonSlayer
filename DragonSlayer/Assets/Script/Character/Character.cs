using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public interface Attachment
    {
        void SetOwner(Character ch);
    }

    [SerializeField]
    CharacterStatus status ;
    public CharacterStatus Status{ get { return status; } }


    //行動を反映させるアイコン
    [SerializeField]
    ActionSequencer actionSequencer;
    public ActionSequencer ActionSequencer { get { return actionSequencer; } }

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
        if (status == null) flag = true;
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

        //キャラクターの付属クラスに所有者を設定する
        Attachment at = actionSequencer;
        at.SetOwner(this);
        at = status;
        at.SetOwner(this);

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

            if (status.UseStamina(10))
            {
        actionSequencer.Activate(ap);
            }

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

    public void OnAttacked(Character other)
    {
        if (other == null) { return; }

        uint Attack = other.Status.Attack;

        status.DamageProcess(Attack);

    }

}
