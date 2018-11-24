using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FSMSTATE의 하위를 넣을 때에 무조건 들어가게됨.
[RequireComponent(typeof(MonsteFSMManager))]
public class MonsterState : MonoBehaviour {
    protected MonsteFSMManager _manager;

    private void Awake()
    {
        _manager = GetComponent<MonsteFSMManager>();
    }

    public virtual void BeginState() { }
    public virtual void EndState() { }

}
