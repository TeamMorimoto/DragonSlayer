using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : Bar
{
    [SerializeField]
    Character character;

    protected override void Awake()
    {
        base.Awake();
 
        if(this.enabled)
        {
            bool flag = false;
            if (character == null) flag = true;
            if (flag) this.enabled = false;
        }
    }

    private void Start()
    {

        if (!SetBarSize(character.Status.HitPointMax * 1.5f))
        {
            Debug.LogError("キャラクターのヒットポイント最大値が不正です"); 
        }
        if (!SetBarRate(character.Status.HitPointRate))
        {
            Debug.LogError("キャラクターのヒットポイント割合が不正です");
        }
    }



}
