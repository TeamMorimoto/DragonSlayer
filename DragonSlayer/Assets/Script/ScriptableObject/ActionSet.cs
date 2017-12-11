using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "MyGame/Create ActionSet", fileName = "ActionSet")]
public class ActionSet : ScriptableObject
{
   [System.Serializable]
    public struct ActionSetElement
    {
        [SerializeField]
        public ActionParamater param;
        [SerializeField]
        public int parsentage;
    }

    [SerializeField]
    ActionSetElement[] actionSet;

    [SerializeField]
    int[] table = new int[100];

    public void OnEnable()
    {
        int index = 0;
        bool breakFlag=false;
        for(int i=0;i<actionSet.Length;i++)
        {
            for (int j=0;j<actionSet[i].parsentage;j++)
            {
                table[index] = i;
                index++;
                if(index==0)
                {
                    breakFlag = true;
                    break;
                }
            }
            if (breakFlag==true)
            {
                break;
            }
        }
    }

    public ActionParamater GetActionSetElement(int num)
    {
        if (num >= 0 && num < 100)
        {
            return actionSet[table[num]].param;
        }
        else
        {      
            return null;
        }
    }
	
}
