using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{   
    [SerializeField]
    RectTransform BarPontBack;

    [SerializeField]
    RectTransform BarPoint;


    private void Awake()
    {
        bool flag = false;
        if (BarPoint == null) flag = true;
        if (BarPontBack == null) flag = true;

        if(flag)
        {
            this.enabled = false;
        }
    }

    /// <summary>
    /// バーの長さ全体の変更
    /// ポイントの背景部分の長さを指定
    /// 0より大きい場合成功
    /// </summary>
    /// <param name="Width">変更したい幅</param>
    /// <returns>成否</returns>
    protected bool SetBarSize(float Width)
    {
        if (Width > 0)
        {
            Vector2 v = BarPontBack.sizeDelta;
            v.x = Width;

            BarPontBack.sizeDelta = v;
            return true;
        }
        return false;
    }

    /// <summary>
    /// バーのポイント部分の長さを設定
    /// 背景部分の長さに対する割合を指定
    /// 0以上で成功
    /// </summary>
    /// <param name="rate">背景の長さに対する割合</param>
    /// <returns>成否</returns>
    protected bool SetBarRate(float rate)
    {
        if (rate >= 0 && rate <= 1.0f)
        {
            Vector3 v = BarPoint.localScale;
            v.x = rate;

            BarPoint.localScale = v;

            return true;
        }
        return false;
    }

  
}
