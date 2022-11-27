using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public enum SkillType
{
    None,
    Skill0,
    Skill1,
    Skill2,
    MAX,
}

public enum MoveType
{
    MOVE,
    RESET,
}

public class Entity : MonoBehaviour
{
    [SerializeField]
    private Animator an;

    #region HP
    public bool IsDead
    {
        get
        {
            if (m_HP <= 0)
                return true;
            else
                return false;
        }
    }
    private float m_HP;
    [SerializeField]
    private UIHpBar m_uiHP;
    #endregion

    #region MovePosition
    private Vector3 m_SrcPos;
    #endregion

    #region Attack
    [SerializeField]
    private bool m_IsAutoAttack;
    [HideInInspector]
    public bool m_IsAlreadyAttack;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        m_HP = 100f;
        m_uiHP.SetHp(m_HP);
        m_SrcPos = transform.position;

        m_IsAlreadyAttack = false;

    }

    /// <summary>
    /// AI 自动攻击
    /// </summary>
    /// <param name="target"></param>
    public void CastSkill(Entity target)
    {
        int indexSkill = Random.Range((int)SkillType.Skill0, (int)SkillType.MAX);
        CastSkill(target, (SkillType)indexSkill);
    }

    /// <summary>
    /// 主角， 手动攻击
    /// </summary>
    /// <param name="target"></param>
    /// <param name="skill"></param>
    public void CastSkill(Entity target, SkillType skill)
    {
        m_IsAlreadyAttack = true;
        Move(MoveType.MOVE, target.transform.position + (-transform.forward * 3), skill);

    }

    /// <summary>
    /// 判断实体是否为攻击移动
    /// 如是，则播放移动动画，并选择技能攻击
    /// 不是，则不播放，返回原位，并判断是否需要交换回合
    /// </summary>
    /// <param name="moveType"></param>
    /// <param name="v3TargetPos"></param>
    /// <param name="skillType"></param>
    private void Move(MoveType moveType, Vector3 v3TargetPos, SkillType skillType)
    {
        if(moveType == MoveType.MOVE)
        {
            an.SetBool("bMove", true);
            transform.DOMove(v3TargetPos, 1f).OnComplete(() =>
            {
                an.SetBool("bMove", false);
                Attack(skillType);
            });
        }
        else if(moveType == MoveType.RESET)
        {
            an.SetBool("bMove", false);
            transform.DOMove(v3TargetPos, 1f).OnComplete(() =>
            {
                //检测分组实体是否已经全都发起过攻击
                //是，则切换对方回合
                //否，则下一位攻击
                //TODO：
                BattleManager.Instance.ChangeCaster();
            });
        }
    }

    /// <summary>
    /// 技能选择，并播放对应动画
    /// </summary>
    /// <param name="skillType"></param>
    private void Attack(SkillType skillType)
    {
        switch(skillType)
        {
            case SkillType.Skill0:
                an.SetTrigger("triggerAttack1");
                break;
            case SkillType.Skill1:
                an.SetTrigger("triggerAttack2");
                break;
            case SkillType.Skill2:
                an.SetTrigger("triggerSkill");
                break;
            default:
                return ;
        }
    }

    /// <summary>
    /// 收到伤害，从battleManager传入
    /// 如死亡，则播放死亡动画
    /// 如仍然或者，则播放受伤动画
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(float damage)
    {
        m_HP -= damage;
        if(m_HP <= 0)
        {
            m_HP = 0;
            //播放死亡动画
            an.SetTrigger("triggerDead");
        }
        else
        {
            //播放受击动画
            an.SetTrigger("triggerHurt");
        }
        m_uiHP.Damage(damage);
    }

    /// <summary>
    /// 施法者回到原位，调用Move方法
    /// </summary>
    public void ResetMoveEvent()
    {
        Move(MoveType.RESET, m_SrcPos, SkillType.None);
    }
}
