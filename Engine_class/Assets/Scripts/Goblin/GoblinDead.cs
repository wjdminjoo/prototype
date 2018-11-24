using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoblinDead : GoblinState {
    protected override void BeginState()
    {
        base.BeginState();
      
    }

    protected override void EndState()
    {
        base.EndState();
    }

    private void Update()
    {
        GameLib.Log(this, "GoblinDead");
    }
}
