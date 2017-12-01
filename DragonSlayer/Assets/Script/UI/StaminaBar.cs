using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaminaBar :Bar {

    [SerializeField]
    Character character;

    protected override void Awake()
    {
        base.Awake();

        if (this.enabled)
        {
            bool flag = false;
            if (character == null) flag = true;
            if (flag) this.enabled = false;
        }
    }

    private void Start()
    {

        character.Status.SetEventChangeStamina(OnChangeStamina);

        if (!SetBarSize(character.Status.StaminaMax * 1.5f))
        {
            Debug.LogError("キャラクターのスタミナの最大値が不正です");
        }
        if (!SetBarRate(character.Status.StaminaRate))
        {
            Debug.LogError("キャラクターのスタミナの割合が不正です");
        }
    }

    void OnChangeStamina()
    {
        if (!SetBarRate(character.Status.StaminaRate))
        {
            Debug.LogError("キャラクターのスタミナの割合が不正です");
        }
    }

}
