﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterStatus : MonoBehaviour,
    Character.Attachment
{
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
    uint staminaMax = STAMINA_MAX_DEFAULT;
    public uint StaminaMax { get { return staminaMax; } }

    //スタミナ
    [SerializeField]
    uint stamina =0;
    public uint Stamina { get { return stamina; } }

    //現在のヒットポイントの割合
    public float HitPointRate { get { return (float)HitPoint / (float)HitPointMax; } }

    //現在のスタミナの割合
    public float StatminaRate { get { return (float)Stamina / (float)StaminaMax; } }

    //所有しているキャラクター
    [SerializeField]
    Character owner;
    public Character Owner { get { return owner; } }
    void Character.Attachment.SetOwner(Character ch) { owner = ch; }


}
