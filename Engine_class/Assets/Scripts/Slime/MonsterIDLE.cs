using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MonsterIDLE : MonsterState
{
    public float idleTime = 5.0f;
    private float time = 0.0f;

    public override void BeginState()
    {
        base.BeginState();
        //if (null != _manager)
        //    _manager.Animatoion.CrossFade("SL_Idle");
    }

    public override void EndState()
    {
    }

    private void Update()
    {
        GameLib.Log(this, "MonsterIDLE");
        time += Time.deltaTime;

        if (time > idleTime) _manager.SetState(MonsterStates.Patrol);
    }

}
