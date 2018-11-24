using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinPatrol : GoblinState {

    Vector3 dest;

    protected override void BeginState()
    {
        base.BeginState();
        dest = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
    }

    protected override void EndState()
    {
        base.EndState();
    }


    private void Update()
    {
        GameLib.Log(this, "GoblinPatrol");

        if (GameLib.DetectCharacter(_manager.sight, _manager.playercc))
        {
            _manager.SetState(MonsterStates.Chase);
            return;
        }

        if ((Vector3.Distance(dest, transform.position) < 0.1f))
        {
            _manager.SetState(MonsterStates.IDLE);
            return;
        }

        _manager.CC.CKMove(dest, _manager.MonsterState);
    }
}
