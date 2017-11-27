using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ActionIcon : MonoBehaviour
{
 

    [SerializeField]
    float sizeRate_normal = 0.5f;
    [SerializeField]
    float sizeRate_valid = 1.0f;

    public bool Active{ get { return active;} }
    bool active;  

    Image image;

    [SerializeField]
    ActionSequencer actionSequencer;
    
    private void Awake()
    {
        image = GetComponent<Image>();
        if(image && actionSequencer)
        {
            actionSequencer.SetEventOnChangeMode(OnSequencerChangeMode);
            Deactivate(true);
        }
        else
        {
            this.enabled = false;
        }
    }

    //アクティブにする
    public void Activate(ActionParamater ap)
    {
        if(!active)
        {
            //状態初期化
            image.rectTransform.localScale = new Vector3(1,1,1)*sizeRate_normal;
          
            active = true;
          
            //完全不透過
            Color c = image.color;
            c.a = 1;
            image.color = c;

            //アイコン画像を設定
            image.sprite = ap.Sprite;          
        }
    }

    //非アクティブにする
    public void Deactivate(bool force = false)
    {
        if (active || force)
        {
            //完全透過
            Color c = image.color;
            c.a = 0;
            image.color = c;

            active = false;
        }
    }

    void OnSequencerChangeMode(ActionSequencer.Mode newMode, ActionParamater ap)
    {
        //アクションシーケンサーの状態の変化によって表示を変更する
        //デリゲートとしてアクションシーケンサーに登録して利用する関数

        switch (newMode)
        {
            case ActionSequencer.Mode.PRELIMINARY_BEFORE:
                Activate(ap);
                break;

            case ActionSequencer.Mode.MAIN:
                image.rectTransform.localScale = new Vector3(1, 1, 1) * sizeRate_valid;
                break;

            case ActionSequencer.Mode.PRELIMINARY_END:
                image.rectTransform.localScale = new Vector3(1, 1, 1) * sizeRate_normal;
                break;

            case ActionSequencer.Mode.FINISHED:
                Deactivate();
                break;

            default:
                Debug.Log("バグ");
                break;
        }
    }
}
