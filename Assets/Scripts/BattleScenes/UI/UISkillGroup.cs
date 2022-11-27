using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillGroup : MonoBehaviour
{
    private Text m_txtOptTips;
    private Text m_TurnTips;

    private Button m_btnSkill0;
    private Button m_btnSkill1;
    private Button m_btnSkill2;

    public static UISkillGroup Instance;

    private Entity m_entity;
    private Entity m_Target;

    private void Awake()
    {
        Instance = this;
        this.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        m_txtOptTips = transform.Find("OptTips").GetComponent<Text>();
        m_TurnTips = transform.Find("TurnTips").GetComponent<Text>();
        m_btnSkill0 = transform.Find("Skill/Skill_0").GetComponent<Button>();
        m_btnSkill1 = transform.Find("Skill/Skill_1").GetComponent<Button>();
        m_btnSkill2 = transform.Find("Skill/Skill_2").GetComponent<Button>();

        m_btnSkill0.onClick.AddListener(OnClick_Skill0);
        m_btnSkill1.onClick.AddListener(OnClick_Skill1);
        m_btnSkill2.onClick.AddListener(OnClick_Skill2);

        m_entity = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
        m_Target = null;
        ChoseTips();
    }

    public void Show()
    {
        this.gameObject.SetActive(true);
    }

    /// <summary>
    /// 选择技能1，并传入到实体
    /// </summary>
    private void OnClick_Skill0()
    {
        Debug.Log("Skill0");
        if(CanUseSkill())
        {
            m_entity.CastSkill(m_Target, SkillType.Skill0);
            BattleManager.Instance.SetCasterAndByCaser(m_entity, m_Target);
            m_Target = null;
        }
       
    }

    /// <summary>
    /// 选择技能2，并传入到实体
    /// </summary>
    private void OnClick_Skill1()
    {
        Debug.Log("Skill1");
        if (CanUseSkill())
        {
            m_entity.CastSkill(m_Target, SkillType.Skill1);
            BattleManager.Instance.SetCasterAndByCaser(m_entity, m_Target);
            m_Target = null;
        }
      
    }

    /// <summary>
    /// 选择技能3，并传入到实体
    /// </summary>
    private void OnClick_Skill2()
    {
        Debug.Log("Skill2");
        if (CanUseSkill())
        {
            m_entity.CastSkill(m_Target, SkillType.Skill2);
            BattleManager.Instance.SetCasterAndByCaser(m_entity, m_Target);
            m_Target = null;
        }           
    }

    // Update is called once per frame
    void Update()
    {
        //显示哪方回合
        m_TurnTips.text = BattleManager.Instance.m_State == BattleState.PLAYER_TURN ? "乙方回合" : "敌方回合";

        //没有点击到地方实体，则返回
        if (Input.GetMouseButtonDown(0) == false)
        {
            return;
        }

        //如果是我方回合，我方实体没有死亡，并且没有攻击过
        //则射线检测攻击敌方哪个实体
        if(BattleManager.Instance.m_State == BattleState.PLAYER_TURN && m_entity.IsDead == false && m_entity.m_IsAlreadyAttack == false)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hitInfo;
            if (Physics.Raycast(ray, out hitInfo))
            {
                if (hitInfo.collider.tag == "Enemy1" || hitInfo.collider.tag == "Enemy2" || hitInfo.collider.tag == "Enemy3")
                {
                    //选择了哪个对象；
                    Entity entity = hitInfo.collider.GetComponent<Entity>();
                    //如选择的实体已死亡，提示选择的人物已死亡
                    if (entity.IsDead)
                    {
                        UITips.Instance.ShowTip();
                    }
                    //提示攻击的实体名称
                    else
                    {
                        m_Target = entity;
                        UITips.Instance.ShowTarget(entity);
                    }
                }
            }
        }        
    }

    /// <summary>
    /// 我方回合提示，传入给战斗管理器
    /// </summary>
    public void ChoseTips()
    {
        m_txtOptTips.text = "你需要一个目标";
        StartCoroutine(ShowTips());
    }

    /// <summary>
    /// 提示调用协程
    /// </summary>
    /// <returns></returns>
    IEnumerator ShowTips()
    {
        m_txtOptTips.gameObject.SetActive(true);
        yield return new WaitForSeconds(5f);
        m_txtOptTips.gameObject.SetActive(false);
    }

    /// <summary>
    /// 判断是否可以使用技能
    /// </summary>
    /// <returns></returns>
    private bool CanUseSkill()
    {
        //如果是地方回合，则不可使用技能
        if(BattleManager.Instance.m_State == BattleState.ENEMY_TURN)
        {
            m_txtOptTips.text = "当前是敌方回合，不可发起攻击";
            return false;
        }
        else
        {
            //如果是主角实体已死亡，则不可使用技能
            if (m_entity.IsDead)
            {
                m_txtOptTips.text = "你已阵亡，不可发起攻击";
                return false;
            }
            //如果是主角实体已经攻击过，则不可使用技能
            else if (m_entity.m_IsAlreadyAttack)
            {
                m_txtOptTips.text = "你已经发起过攻击，不可发起攻击";
                return false;
            }
            ////如果是主角实体没有选择敌方实体，则不可使用技能
            else if (m_Target == null)
            {
                m_txtOptTips.text = "你需要选定一个目标";
                return false;
            }
            return true;
        }
    }
}
