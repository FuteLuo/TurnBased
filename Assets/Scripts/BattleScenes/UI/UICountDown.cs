using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UICountDown : MonoBehaviour
{
    public Text m_TxtNums;

    private int m_totalCounts = 3;
    // Start is called before the first frame update
    void Start()
    {
        m_TxtNums.text = m_totalCounts.ToString();
        InvokeRepeating("Decrease", 1, 1);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Decrease()
    {
        m_totalCounts--;
        if(m_totalCounts <= 0)
        {
            m_totalCounts = 3;
            m_TxtNums.text = "Go";
            CancelInvoke("Decrease");
            StartCoroutine(Waiting());
        }
        else
        {
            m_TxtNums.text = m_totalCounts.ToString();
        }
    }

    private IEnumerator Waiting()
    {
        yield return new WaitForSeconds(1f);
        this.gameObject.SetActive(false);
        UISkillGroup.Instance.Show();
        
    }
}
