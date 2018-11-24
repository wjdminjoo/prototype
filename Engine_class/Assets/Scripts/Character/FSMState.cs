using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FSMSTATE의 하위를 넣을 때에 무조건 들어가게됨.
[RequireComponent(typeof(FSMManager))]
public class FSMState : MonoBehaviour {
    protected FSMManager _manager;
    
    private void Awake()
    {
        _manager = GetComponent<FSMManager>();  
    }

    public virtual void BeginState() { }
    public virtual void EndState() { }
    protected virtual void Update()
    {
        if(GetType().IsDefined(typeof(TargetChckAttribute), false))
        {
            if(_manager.target == null)
            {
                _manager.SetState(PlayerState.IDLE);
            }
        }
    }
}
