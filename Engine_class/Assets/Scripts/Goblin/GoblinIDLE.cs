using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinIDLE : GoblinState {

    public float idleTime = 4.0f;
    private float time = 0.0f;

    protected override void BeginState()
    {
        base.BeginState();
    }

    protected override void EndState()
    {
        base.EndState();
    }

    private void Update()
    {
        GameLib.Log(this, "GoblinIDLE");
        time += Time.deltaTime;

        if (time > idleTime) _manager.SetState(MonsterStates.Patrol);
    }

}
