using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimEvent : MonoBehaviour {

    FSMManager _manager;
    private void Awake()
    {
        _manager = transform.root.GetComponent<FSMManager>();
    }

    void HitCheck()
    {
        GameLib.Log(this, "HitCheck");

        // 효율 쓰레기니까 사용하지 않으면 좋다.
        // transform.root.SendMessage("AttackCheck");


        PlayerAttack attackState = _manager.CurrentSateComponent as PlayerAttack;
        if(attackState != null)
        {
            attackState.AttackCheck();
        }
    }

}
