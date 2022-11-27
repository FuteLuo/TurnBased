using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIHpBar : MonoBehaviour
{
    private Text UIName;

    private GameObject HpBar;
    private Image hpImage;
    private float curHp;
    private float MaxHp;

    private GameObject m_obj;

    [SerializeField]
    private Vector3 mV3Offset;

    private void Start()
    {
        GameObject uiUnit = Resources.Load<GameObject>("Prefabs/UI/Unit_Entity");
        uiUnit.SetActive(true);
        m_obj = Instantiate(uiUnit);
        m_obj.transform.parent = GameObject.Find("UnitEntityRoot").transform;

        UIName = m_obj.transform.Find("Context/txtCharacterName").GetComponent<Text>();
        UIName.text = transform.gameObject.name;

        HpBar = m_obj.transform.Find("Context/HPBar").gameObject;
        HpBar.SetActive(!SceneManager.Instance.isMainScene);
        hpImage = HpBar.transform.Find("hp").GetComponent<Image>();
    }

    /// <summary>
    /// 传入血量
    /// </summary>
    /// <param name="hp"></param>
    public void SetHp(float hp)
    {
        MaxHp = hp;
        curHp = hp;
    }

    /// <summary>
    /// 接收实体造成的伤害，并更新血量
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(float damage)
    {
        curHp -= damage;
        if(curHp <= 0)
        {
            curHp = 0;
        }

        hpImage.fillAmount = curHp / MaxHp;
    }

    /// <summary>
    /// 确定血条位置
    /// </summary>
    private void Update()
    {
        m_obj.transform.position = Camera.main.WorldToScreenPoint(transform.position + mV3Offset);
    }
}
