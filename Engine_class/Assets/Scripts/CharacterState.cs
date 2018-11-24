using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class CharacterState : MonoBehaviour {

    [SerializeField]
    protected float _hp = 10.0f;
    public float HP { get { return _hp; } }

    [SerializeField]
    protected float _moveSpeed = 10.0f;
    public float moveSpeed { get { return _moveSpeed; } }

    [SerializeField]
    protected float _turnSpeed = 360.0f;
    public float turnSpeed { get { return _turnSpeed; } }

    [SerializeField]
    protected float _AttackRange = 2.0f;
    public float AttackRange { get { return _AttackRange; } }

    [SerializeField]
    protected StateDataManager _stateData;
    public StateDataManager stateData { get { return _stateData; } }

    protected virtual void Awake()
    {
        _hp = _stateData.maxHP;
    }

    private CharacterState LastHitBy = null;

    public void TakeDamage(CharacterState from, float damage)
    {
        _hp = Mathf.Clamp(_hp - damage, 0, 10);
        if(_hp <= 0)
        {
            if (LastHitBy == null) LastHitBy = from;



            GetComponent<IFSMManager>().SetDeadState();
            from.GetComponent<IFSMManager>().NotifyTargetKilled();

            GameLib.Log(this, name + "IS KILLED BY" + LastHitBy.name);
        }
    }

    public static float CalcDamage(CharacterState from, CharacterState to)
    {
        return 5.0f;
    }

    public static void ProcessDamage(CharacterState from, CharacterState to)
    {
        float Damage = CalcDamage(from, to);
        to.TakeDamage(from, Damage);
    }
}
