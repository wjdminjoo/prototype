using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDead : MonsterState {
    public override void BeginState()
    {
        base.BeginState();
        
    }

    public override void EndState()
    {
    }

    private void Update()
    {
        GameLib.Log(this, "MonsterDead");
    }
}
