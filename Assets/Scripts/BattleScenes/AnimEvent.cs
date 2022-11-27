using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimEvent : MonoBehaviour
{
    /// <summary>
    /// 触发事件：造成伤害，传入给战斗管理器
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(float damage)
    {
        BattleManager.Instance.Damage(damage);
    }

    /// <summary>
    /// 触发事件：回到原位，传入给战斗管理器
    /// </summary>
    public void ResetMoveEvent()
    {
        BattleManager.Instance.ResetMoveEvent();
    }
}
