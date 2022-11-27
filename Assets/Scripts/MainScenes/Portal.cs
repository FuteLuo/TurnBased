using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            //切换场景
            SceneManager.Instance.ChangeScene("Scenes/BattleScene"); 
        }
    }
}
