using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum emTriggerType
{
    None,
    Enter,
    Exit,
}

public class Npc : MonoBehaviour
{

    private emTriggerType m_emTriggerType;

    private Player m_player;

    public float m_RotationSpeed;

    private float m_timer;

    private Quaternion m_SrcQ;

    // Start is called before the first frame update
    void Start()
    {
        m_emTriggerType = emTriggerType.None;
        m_player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        m_timer = 0f;
        m_SrcQ = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject obj = GameObject.Find("EFX_Portal_prefab");
        if (m_emTriggerType == emTriggerType.Enter)
        {
            //获取向量
            Vector3 dir = m_player.transform.position - transform.position;
            Quaternion targetQ = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetQ, m_RotationSpeed * Time.deltaTime);

            m_timer += Time.deltaTime;
            if(m_timer >= 3f)
            {
                m_emTriggerType = emTriggerType.None;
                m_timer = 0f;
            }
            obj.transform.Find("chuansongmen_anim").gameObject.SetActive(true);
        }

        if (m_emTriggerType == emTriggerType.Exit)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, m_SrcQ, m_RotationSpeed * Time.deltaTime);
            m_timer += Time.deltaTime;
            if (m_timer >= 0.5f)
            {
                m_emTriggerType = emTriggerType.None;
                m_timer = 0f;
            }
            obj.transform.Find("chuansongmen_anim").gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            m_emTriggerType = emTriggerType.Enter;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            m_emTriggerType = emTriggerType.Exit;
        }
    }
}
