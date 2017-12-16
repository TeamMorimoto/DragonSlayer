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
    
    //行動中であるか
    [SerializeField]
    private bool isDuaringAction;
    protected bool IsDuaringAction { get { return isDuaringAction; } }

    //戦闘マネージャ
    [SerializeField]
    protected BattleManager battleManager;
    public void SetBattleManager(BattleManager bm) { battleManager = bm; }

    protected bool initialized = true;

    protected virtual void Awake()
    {
        //必要なデータが全てそろっているかをチェック
        bool flag = false;
        if (status == null) flag = true;
        if (actionSequencer == null) flag = true; 

        if(flag)
        {
            //そろってない
            Debug.LogError("Characterクラス Awake 必要なデータがセットされていません");
            this.enabled = false;
            initialized = false;
            return;
        }

        //キャラクターの付属クラスに所有者を設定する
        Attachment at = actionSequencer;
        at.SetOwner(this);
        at = status;
        at.SetOwner(this);

   

        isDuaringAction = false;
    }

    private void Start()
    {
        battleManager.AddEventOnMatchDeside(EndAction);
    }

    /// <summary>
    /// 行動を開始する
    /// </summary>
    public void StartAction(ActionParamater ap)
    {
        if (ap != null)
        {
            if (battleManager != null && (!battleManager.IsMatchDeside))
            {
                if (!isDuaringAction)
                {
                    if (status.UseStamina((float)ap.StaminaConsumption))
                    {
                        actionSequencer.Activate(ap);
                    }

                    isDuaringAction = true;
                }
            }
        }
    }

    public void EndAction()
    {
        ActionSequencer.EndActionContinulation();
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

        float Attack =(float) other.Status.Attack;

        float skilpower = other.actionSequencer.CurrentActionParamater.SkilPower;

        status.DamageProcess((uint)(skilpower* Attack));

    }

}
