using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterChase : MonsterState
{
    public override void BeginState() {
        base.BeginState();
       
    }

    public override void EndState() {
        base.EndState();
    }

    private void Update(){
        GameLib.Log(this, "MonsterChase");
        if (!GameLib.DetectCharacter(_manager.sight, _manager.playercc))
        {
            _manager.SetState(MonsterStates.IDLE);
            return;
        }

        if (GameLib.DetectCharacter(_manager.sight, _manager.playercc)){
            if ((Vector3.Distance(_manager.playerTran.position, transform.position) < _manager.MonsterState.AttackRange))
            {
                _manager.SetState(MonsterStates.Attack);
                return;
            }
        }
        _manager.CC.CKMove(_manager.playerTran.position, _manager.MonsterState);

    }
}
