using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinAttack : GoblinState {
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
        GameLib.Log(this, "GoblinAttak");
        if ((Vector3.Distance(_manager.playerTran.position, transform.position) >= _manager.MonsterState.AttackRange))
        {
            _manager.SetState(MonsterStates.Chase);
            return;
        }
        transform.LookAt(_manager.playerTran);
    }
}
