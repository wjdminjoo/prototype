using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRun : FSMState {

    public override void BeginState()
    {
        base.BeginState();
        //if (null != _manager)
        //    _manager.Animatoion.CrossFade("KK_Run_No");
    }

    public override void EndState()
    {
        base.EndState();
    }

    public void Update()
    {
        //Plane[] ps = GeometryUtility.CalculateFrustumPlanes(_manager.sight);

        //bool _isDestected = GeometryUtility.TestPlanesAABB(ps, _manager._testTarget.bounds);
        //if (_isDestected)
        //{
        //    Debug.Log(_manager._testTarget.bounds);
        //   // GameLib.Log()
        //}




        Vector3 dest = _manager.Marker.transform.position;
        dest.y = 0.0f;
        Vector3 playerPos = transform.position;
        playerPos.y = 0.0f;

        if(Vector3.Distance(dest, playerPos) < Mathf.Epsilon)
        {
            _manager.SetState(PlayerState.IDLE);
            return;
        }


        
        GameLib.Log(this, "RUN");
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
