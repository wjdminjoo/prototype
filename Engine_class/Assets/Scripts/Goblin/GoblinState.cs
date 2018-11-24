using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MonsteFSMManager))]
public class GoblinState : MonoBehaviour {
    protected MonsteFSMManager _manager;

    private void Awake()
    {
        _manager = GetComponent<MonsteFSMManager>();
    }
    protected virtual void BeginState() { }
    protected virtual void EndState() { }
}
