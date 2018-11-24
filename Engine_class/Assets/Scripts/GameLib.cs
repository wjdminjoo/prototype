using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetChckAttribute: System.Attribute
{// Attribute -> 사용자 정의
    

} 


public sealed class ShowLogError : System.Attribute // System.Attribute가 상속을 받을 수 있게끔 해주어 다른 클래스에서 참조 가능.
{
    public ShowLogError() { }
}



public static class GameLib {
    // 로그를 에디터에서만 보일 수 있게 함.

    
    
    static GameLib()
    {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled= true;
#else
         Debug.unityLogger.logEnabled = false;
#endif
    }

    public static void WriteLog(object target, string logTag, string message)
    {
        if (target.GetType().IsDefined(typeof(ShowLogError), false)) Debug.LogError(logTag);
        else Debug.unityLogger.Log(logTag, message);
    }


    public static void Log(object target, string message)
    {
        string logTag = "";
        MonoBehaviour b = target as MonoBehaviour;

        if (b != null) logTag = string.Format("{0}.{1}", b.GetType().Name, b.gameObject.name);

        bool logStatus = Debug.unityLogger.logEnabled;
        if (logStatus) WriteLog(target, logTag, message);
    }


    // 확장 메서드의 형식
    // 메서드를 추가한다고 생각하면된다.
    // C++ 에서의 클래스라고 생각하면될듯.
    // this CharacterController 라는건 다른곳에서 CharacterController를 불러줄때 CKMOVE라는 메서드를 사용할 수 있게 해주는것.
    public static void CKMove(this CharacterController cc, Vector3 targetPosition,CharacterState state)
    {
        Transform t = cc.transform;

        Vector3 deltaMove = Vector3.zero;
        Vector3 moveDir = targetPosition - t.position;
        moveDir.y = 0.0f;
        if (moveDir != Vector3.zero)
        {
            t.rotation = Quaternion.RotateTowards(
                t.rotation,
                Quaternion.LookRotation(moveDir),
                state.turnSpeed * Time.deltaTime);
        }

        Vector3 nextMove = Vector3.MoveTowards(
            t.position,
            targetPosition,
            state.moveSpeed * Time.deltaTime);

        deltaMove = nextMove - t.position;
        deltaMove += Physics.gravity * Time.deltaTime;
        cc.Move(deltaMove);

    }


    public static bool DetectCharacter(Camera sight, CharacterController cc)
    {
        Plane[] ps = GeometryUtility.CalculateFrustumPlanes(sight);
        return GeometryUtility.TestPlanesAABB(ps, cc.bounds);
    }

}
