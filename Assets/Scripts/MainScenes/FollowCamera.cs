using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Player m_Player;
    private Vector3 m_relativePos;
    // Start is called before the first frame update
    void Start()
    {
        m_relativePos = transform.position - m_Player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = m_Player.transform.position + m_relativePos;
    }
}
