using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerState : CharacterState {

    private StateDataManager StateData;

    protected override void Awake()
    {
        base.Awake();
       _hp = StateData.maxHP;
        Debug.Log(StateData.maxHP);
        //GameLib.Log(this, "statePlayer.maxHP");
    }
}
