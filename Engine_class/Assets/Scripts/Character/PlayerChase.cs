using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[TargetChck]
public class PlayerChase : FSMState
{


    public override void BeginState()
    {
        base.BeginState();
        //if (null != _manager)
        //    _manager.Animatoion.CrossFade("KK_Run");

    }

    public override void EndState()
    {
        base.EndState();
    }

    protected override void Update()
    {
        base.Update();
        Vector3 dest = _manager.target.transform.position;
        dest.y = 0.0f;
        Vector3 playerPos = transform.position;
        playerPos.y = 0.0f;

        if (Vector3.Distance(dest, playerPos) < 2.0f)
        {
            _manager.SetState(PlayerState.Attack);
            
            return;
        }
        
        GameLib.Log(this, "CHASE");
        //_marker.Maerker.postion;

        // 이동.
        Vector3 deltaMove = Vector3.zero;

        Vector3 moveDir = _manager.Marker.position - transform.position;
        moveDir.y = 0.0f;

        Vector3 nextMove = Vector3.MoveTowards(transform.position, _manager.Marker.position, _manager.playerState.moveSpeed * Time.deltaTime);
        deltaMove = nextMove - transform.position;
        deltaMove += Physics.gravity * Time.deltaTime;
        _manager.CC.Move(deltaMove);

        // 방향 전환
        if (moveDir != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(moveDir), _manager.playerState.turnSpeed * Time.deltaTime);
        }

        // Quaternion playerRotation = Quaternion.LookRotation(Vector3 rot(transform.rotation, 0.0f, 0.0f));
    }

  
}
