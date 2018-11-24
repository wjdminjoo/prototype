using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIDLE : FSMState{

    public override void BeginState(){
        base.BeginState();
        //if (null != _manager)
        //    _manager.Animatoion.CrossFade("KK_Idle");


    }

    public override void EndState(){
    }

    private void Update(){
        GameLib.Log(this, "IDLE");
    }

    
    

}
