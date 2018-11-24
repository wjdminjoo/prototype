using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPatrol : MonsterState {
    Vector3 destination;


    public override void BeginState()
    {
        base.BeginState();
        //if (null != _manager)
        //    _manager.Animatoion.CrossFade("SL_Walk");
        destination = new Vector3(Random.Range(-20, 20), 0, Random.Range(-20, 20));
    }

    public override void EndState()
    {
    }

    private void Update()
    {
        GameLib.Log(this, "MonsterPATROL");

        if (GameLib.DetectCharacter(_manager.sight, _manager.playercc))
        {
            _manager.SetState(MonsterStates.Chase);
            return;
        }

        if ((Vector3.Distance(destination, transform.position) < 0.1f))
        {
            _manager.SetState(MonsterStates.IDLE);
            return;
        }
        
        _manager.CC.CKMove(destination, _manager.MonsterState);
    }
}
