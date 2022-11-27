using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITips : MonoBehaviour
{
    public GameObject mTipsPanel;

    private Text m_TargetText;

    public static UITips Instance;

    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        mTipsPanel.SetActive(false);
        m_TargetText = transform.Find("Panel/TextTips").GetComponent<Text>();
    }


    private IEnumerator ShowTips(Entity entity = null)
    {
        if(entity == null)
        {
            m_TargetText.text = "你选择的攻击单位已经阵亡，请选择其他英雄";
            yield return new WaitForSeconds(2f);
            mTipsPanel.SetActive(false);
        }
        else
        {
            m_TargetText.text = "你选择的攻击单位是：" + entity.gameObject.name;
            yield return new WaitForSeconds(1f);
            mTipsPanel.SetActive(false);
        }
       
    }

    /// <summary>
    /// 提示选择
    /// </summary>
    public void ShowTip()
    {
        mTipsPanel.SetActive(true);
        StartCoroutine(ShowTips(null));
    }
    
    /// <summary>
    /// 提示目标
    /// </summary>
    /// <param name="entity"></param>
    public void ShowTarget(Entity entity)
    {
        mTipsPanel.SetActive(true);
        StartCoroutine(ShowTips(entity));
    }
}
