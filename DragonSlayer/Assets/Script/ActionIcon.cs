using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class ActionIcon : MonoBehaviour
{
    enum Mode
    {
        PRELIMINARY_BEFORE,
        MAIN,
        PRELIMINARY_END,
    }

    [SerializeField]
    float sizeRate_normal = 0.5f;
    [SerializeField]
    float sizeRate_valid = 1.0f;

    [SerializeField]
    float time_preliminary_before=0.3f;
    [SerializeField]
    float time_valid=0.5f;
    [SerializeField]
    float time_preliminary_end=0.1f;



    bool active;
    float time;

    Image image;

    Mode mode;

    private void Awake()
    {
        image = GetComponent<Image>();
        if(image)
        {
            Deactivate(true);
        }
        else
        {
            this.enabled = false;
        }
    }

    public void Activate()
    {
        if(!active)
        {
            image.rectTransform.localScale = new Vector3(1,1,1)*sizeRate_normal;
            time = 0f;
            active = true;
            mode = Mode.PRELIMINARY_BEFORE;

            Color c = image.color;
            c.a = 1;
            image.color = c;
        }
    }

    public void Deactivate(bool force = false)
    {
        if (active || force)
        {
            Color c = image.color;
            c.a = 0;
            image.color = c;
            time = 0;
            active = false;
        }
    }

    private void Update()
    {
        if(active)
        {
            float targetTime = 0 ;
            switch(mode)
            {
                case Mode.PRELIMINARY_BEFORE:
                    targetTime = time_preliminary_before;
                    break;
                case Mode.MAIN:
                    targetTime = time_valid;
                    break;
                case Mode.PRELIMINARY_END:
                    targetTime = time_preliminary_end;
                    break;
                default:
                    Debug.Log("バグ");
                    break;
            }

            time += Time.deltaTime;

            if(time>targetTime)
            {
                time -= targetTime;

                switch (mode)
                {
                    case Mode.PRELIMINARY_BEFORE:
                        mode = Mode.MAIN;
                        image.rectTransform.localScale = new Vector3(1, 1, 1) * sizeRate_valid;
                        break;
                    case Mode.MAIN:
                        mode = Mode.PRELIMINARY_END;
                        image.rectTransform.localScale = new Vector3(1, 1, 1) * sizeRate_normal;
                        break;
                    case Mode.PRELIMINARY_END:
                        Deactivate();
                        break;
                    default:
                        Debug.Log("バグ");
                        break;
                }
            }            
        }
    }

    
}
