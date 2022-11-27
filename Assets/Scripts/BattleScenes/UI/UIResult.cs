using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIResult : MonoBehaviour
{
    public GameObject mObjWin;
    public GameObject mObjLose;
    public GameObject mResultPanel;

    public static UIResult Instance;

    private void Awake()
    {
        Instance = this;        
    }

    private void Start()
    {
        mResultPanel.SetActive(false);
    }

    /// <summary>
    /// 显示战斗结果
    /// </summary>
    /// <param name="state"></param>
    public void ShowResult(BattleState state)
    {
        if(state == BattleState.WIN)
        {
            mObjWin.SetActive(true);
            mObjLose.SetActive(false);
        }
        else if(state == BattleState.LOSE)
        {
            mObjWin.SetActive(false);
            mObjLose.SetActive(true);
        }
        mResultPanel.SetActive(true);
    }
}
