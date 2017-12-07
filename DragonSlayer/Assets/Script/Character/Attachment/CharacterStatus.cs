using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStatus : MonoBehaviour,
    Character.Attachment
{
    public delegate void OnEventParamChange();

    const uint HIT_POINT_MAX_DEFAULT = 100;
    const uint STAMINA_MAX_DEFAULT = 50;

    //攻撃力
    [SerializeField]
    uint attack = 10;
    public uint Attack { get { return attack; } }

    //防御力
    [SerializeField]
    uint defence = 10;
    public uint Defence { get { return defence; } }

    //ヒットポイントマックス
    [SerializeField]
    uint hitPointMax = HIT_POINT_MAX_DEFAULT;
    public uint HitPointMax { get { return hitPointMax; } }

    //ヒットポイント
    [SerializeField]
    uint hitPoint=0;
    public uint HitPoint { get { return hitPoint; } }

    //スタミナマックス
    [SerializeField]
    float staminaMax = STAMINA_MAX_DEFAULT;
    public float StaminaMax { get { return staminaMax; } }

    //スタミナ
    [SerializeField]
    float stamina =0;
    public float Stamina { get { return stamina; } }

    [SerializeField]
    float staminaRecoverPerSecond;

    public bool IsDead() { return (hitPoint == 0); }
    

    //現在のヒットポイントの割合
    public float HitPointRate { get { return (float)HitPoint / (float)HitPointMax; } }

    //現在のスタミナの割合
    public float StaminaRate { get { return (float)Stamina / (float)StaminaMax; } }

    //所有しているキャラクター
    [SerializeField]
    Character owner;
    public Character Owner { get { return owner; } }
    void Character.Attachment.SetOwner(Character ch) { owner = ch; }


    public OnEventParamChange ChangeHitpoint;
    public void SetEventChangeHitpoint(OnEventParamChange ev) { ChangeHitpoint = ev; }


    public OnEventParamChange ChangeStamina;
    public void SetEventChangeStamina(OnEventParamChange ev) { ChangeStamina = ev; }

    private void Awake()
    {
        stamina = StaminaMax;
        hitPoint = HitPointMax;
    }

    private void Update()
    {
        //スタミナ回復
        if(stamina<staminaMax)
        {
            stamina += staminaRecoverPerSecond * Time.deltaTime;

            if (stamina>StaminaMax)
            {
                stamina = staminaMax;
            }

            if(ChangeStamina!=null)
            {
                ChangeStamina();
            }

        }
    }


    public void DamageProcess(uint othAttack)
    {
        
        uint Damage = (othAttack < defence) ? 0 : othAttack - defence;

        uint newHitPoint = (hitPoint < Damage) ? 0 : hitPoint - Damage;

        hitPoint = newHitPoint;

        if(ChangeHitpoint!=null)
        {
            ChangeHitpoint();
        }
    }

    public bool UseStamina(float point)
    {
        if(stamina>=point)
        {
            stamina -= point;

            if (ChangeStamina != null)
            {
                ChangeStamina();
            }

            return true;
        }
        return false;
    }

}
