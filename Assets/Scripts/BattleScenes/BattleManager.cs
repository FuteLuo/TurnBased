using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleState
{
    ENEMY_TURN,
    PLAYER_TURN,
    WIN,
    LOSE,
}

public class BattleManager : MonoBehaviour
{
    public BattleState m_State;

    public List<Entity> m_ListEnenmy;
    public List<Entity> m_ListPlayer;

    public Entity m_Player;

    public Entity m_caser;
    public Entity m_byCaser;

    public static BattleManager Instance;


    private void Awake()
    {
        Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        m_State = BattleState.PLAYER_TURN;
        m_Player = GameObject.FindGameObjectWithTag("Player").GetComponent<Entity>();
    }

    /// <summary>
    /// 当前归属的回合对象是否都完成了攻击，如完成，则切换回合，否则让尚未完成的对象完成。
    /// 检测对方实体是否都已阵亡
    /// 递归
    /// </summary>
    public void ChangeCaster()
    {
        //敌方回合
        if (m_State == BattleState.ENEMY_TURN)
        {
            foreach(Entity enemy in m_ListEnenmy)
            {
                if (enemy.m_IsAlreadyAttack || enemy.IsDead)
                    continue;
                //发起攻击
                Entity target = FindAlive(m_ListPlayer);
                if (target == null)
                    continue;

                enemy.CastSkill(target);
                SetCasterAndByCaser(enemy, target);
                return;
            }

            ResetEntityAttackState();

            if (FindAlive(m_ListPlayer) == null)
            {
                m_State = BattleState.LOSE;//敌人全部阵亡, 闯关失败
                UIResult.Instance.ShowResult(m_State);
            }
            else
            {
                m_State = BattleState.PLAYER_TURN;//切换回合
                ChangeCaster();
            }
        }
        //我方回合
        else if (m_State == BattleState.PLAYER_TURN)
        {
            
            if(!m_Player.IsDead && !m_Player.m_IsAlreadyAttack)
            {
                //比卡丘先攻击
                UISkillGroup.Instance.ChoseTips();
                return;
            }
            else
            {
                foreach(Entity player in m_ListPlayer)
                {
                    //如果是自己，则跳过
                    if (player.gameObject.tag == "Player")
                        continue;
                    //剩下的死了或者打过了，跳过
                    if (player.m_IsAlreadyAttack || player.IsDead)
                        continue;

                    Entity target = FindAlive(m_ListEnenmy);
                    if (target == null)
                        continue;

                    player.CastSkill(target);
                    SetCasterAndByCaser(player, target);
                    return;
                }

                ResetEntityAttackState();

                if (FindAlive(m_ListEnenmy) == null)
                {
                    m_State = BattleState.WIN;//敌人全部阵亡, 闯关成功
                    UIResult.Instance.ShowResult(m_State);
                }
                else
                {
                    m_State = BattleState.ENEMY_TURN;//切换回合
                    ChangeCaster();
                }
            }
        }
    }
    
    /// <summary>
    /// 重置当前攻击状态
    /// </summary>
    private void ResetEntityAttackState()
    {
        foreach(var item in m_ListEnenmy)
        {
            item.m_IsAlreadyAttack = false;
        }
        foreach (var item in m_ListPlayer)
        {
            item.m_IsAlreadyAttack = false;
        }
    }

    /// <summary>
    /// 设置 施法者和被施法者
    /// </summary>
    /// <param name="caser"></param>
    public void SetCasterAndByCaser(Entity caser, Entity byCaser)
    {
        m_caser = caser;
        m_byCaser = byCaser;
    }

    /// <summary>
    /// 寻找对方是否有活的实体
    /// </summary>
    /// <param name="targets"></param>
    /// <returns></returns>
    private Entity FindAlive(List<Entity> targets)
    {
        int index = Random.Range(0, 3);
        Entity target = targets[index];
        if(target.IsDead)
        {
            target = null;
            foreach(Entity targetEntity in targets)
            {
                if(!targetEntity.IsDead)
                {
                    target = targetEntity;
                    break;
                }
            }
        }
        return target;
    }

    /// <summary>
    /// 收到动画事件，被施法者造成伤害，传入给实体
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(float damage)
    {
        m_byCaser.Damage(damage);
    }

    /// <summary>
    /// 收到动画事件，施法者回到原位，传入给实体
    /// </summary>
    public void ResetMoveEvent()
    {
        m_caser.ResetMoveEvent();
    }
}
