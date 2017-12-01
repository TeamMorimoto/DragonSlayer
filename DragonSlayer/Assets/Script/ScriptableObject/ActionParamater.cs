using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="MyGame/Create ActionParamater",fileName ="ActionParamater")]
public class ActionParamater : ScriptableObject
{
    public enum TYPE
    {
        ATTACK,
        GUARD,
        DODGE
    }
    [SerializeField]
    TYPE type;
    public TYPE Type { get { return type; } }

    [SerializeField]
    float time_preliminary_before;
    public float Time_preliminary_before { get { return time_preliminary_before; } }
    [SerializeField]
    float time_valid;
    public float Time_valid { get { return time_valid; } }

    [SerializeField]
    float time_preliminary_end;
    public float Time_preliminary_end { get { return time_preliminary_end; } }

    [SerializeField]
    Sprite sprite;
    public Sprite Sprite { get { return sprite; } }

    [SerializeField]
    uint staminaConsumption;
    public uint StaminaConsumption { get { return staminaConsumption; } }
    

}
