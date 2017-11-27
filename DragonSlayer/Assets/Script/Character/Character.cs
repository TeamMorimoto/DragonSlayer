using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    //行動を反映させるアイコン
    [SerializeField]
    ActionIcon icon;

    //各種行動に関するパラメータ
    [SerializeField]
    ActionParamater AttackPram;
    [SerializeField]
    ActionParamater GuardParam;
    [SerializeField]
    ActionParamater DodgeParam;

    
    //行動中であるか
    [SerializeField]
    protected bool isDuaringAction;


    private void Awake()
    {
        //必要なデータが全てそろっているかをチェック
        bool flag = false;
        if (icon == null) flag = true;
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

        icon.Activate(ap);

        isDuaringAction = true;
    }


    private void Update()
    {
        if(isDuaringAction)
        {
            if(icon.Active == false)
            {
                isDuaringAction = false;
            }
        }
    }

}
