using System.Collections;
using System.Collections.Generic;
using UnityEngine;



/*
1. MonsterStat 제작 ( 이동 속도와 회전 속도 )  v
2. MonsterFSMManager 제작.  v
2-1. MonsterFSMManager 클래스 생성.  v
2-2 MonsterState enum 선언  v
IDLE , PATROL, CHASE, ATTACK, DEAD
3. MonsterFSMState 부모 클래스 제작  v
4. MonsterFSMState를 상속받은 5개의 스테이트 클래스 제작   v
4-1 MonsterIDLE v
4-2 MonsterPATROL v
4-3 MonsterCHASE v
4-4 MonsterATTACK v
4-5 MonsterDEAD v
5. MonsterFSMManager에서 자동으로 스테이트 컴포넌트를 추가하도록 로직 추가 v
6. MonsterFSMManager 스크립트의 실행 순서를 조절 ( 가장 먼저 실행되도록. )
7. MonsterFSMManager에 Camera, Animation, CharacterController, Stat 을 접근할 수 있는 변수 생성 및 초기화
8. MonsterFSMManager에 SetState 함수 제작
9. MonsterFSMManager의 Start 함수에서 SetState 함수 실행

*/

public enum MonsterStates
{
    IDLE = 0,
    Patrol,
    Chase,
    Attack,
    Dead
}

[RequireComponent(typeof(MonsterState))]
[ExecuteInEditMode]
public class MonsteFSMManager : MonoBehaviour, IFSMManager {

    private bool _isinit = false;

    public MonsterStates startState = MonsterStates.IDLE;
    private Dictionary<MonsterStates, MonsterState> _state = new Dictionary<MonsterStates, MonsterState>();

    private Camera _sight;
    public Camera sight { get { return _sight; } }

    private CharacterState _MonsterState;
    public CharacterState MonsterState { get { return _MonsterState; } }

    private Animator _anim;
    //public Animation Animatoion { get { return _animation; } }

    [SerializeField]
    private MonsterStates _currentState;
    public MonsterStates CurrntState { get { return _currentState; }}

    private CharacterController _cc;
    public CharacterController CC { get { return _cc; } }

    private CharacterController _playercc;
    public CharacterController playercc { get { return _playercc; } }


    private Transform _playerTran;
    public Transform playerTran { get { return _playerTran; } }
    // 시야 넓이 설정.
    public int sightaspectRatio = 3;


    private void Start()
    {
        SetState(startState);
        _isinit = true;
    }

    private void Awake()
    {
        _cc = GetComponent<CharacterController>();
        _MonsterState = GetComponent<CharacterState>();
        _playercc = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterController>();
        _playerTran = GetComponent<Transform>();
        _anim = GetComponentInChildren<Animator>();
        _sight = GetComponentInChildren<Camera>();
        _sight.aspect = sightaspectRatio;

        MonsterStates[] stateValue = (MonsterStates[])System.Enum.GetValues(typeof(MonsterStates)); // 시스템 정보를 배열형식으로 요청하여 가져옴.
        foreach (var s in stateValue) // var 대신 PlayerState 써도됨. var가 뭔지 알아보기.
        {
            System.Type FSMType = System.Type.GetType("Monster" + s.ToString());
            MonsterState state = (MonsterState)GetComponent(FSMType);
            if (null == state) state = (MonsterState)gameObject.AddComponent(FSMType); // 만약에 컴포넌트가 없으면 넣어버린다.
                                                                                   // 예외값 처리인듯
                                                                                   // 미리 NULL인지 아닌지 해주는게 좋기는 함.
            _state.Add(s, state);
            //GameLib.Log(this ,s.ToString("G"));
            state.enabled = false; // 로그를 전부다 꺼준다.
        }

    }



    // Update is called once per frame
    void Update () {
		
	}


    public void SetState(MonsterStates newState)
    {
        if (_isinit)
        {
            _state[_currentState].enabled = false;
            _state[_currentState].EndState();
        }
        _currentState = newState;
        _state[_currentState].BeginState();


        _state[_currentState].enabled = true; // 지정한 친구만 로그를 보여준다.
        _anim.SetInteger("MonsterState", (int)_currentState);
    }

    private void OnDrawGizmos()
    {
        if (_sight != null)
        {
            Gizmos.color = Color.blue;

            Matrix4x4 temp = Gizmos.matrix;

            // TRS = Translation, Rotaion, Sight.
            Gizmos.matrix = Matrix4x4.TRS(_sight.transform.position, _sight.transform.rotation, Vector3.one);

            Gizmos.DrawFrustum(Vector3.zero, _sight.fieldOfView, _sight.farClipPlane, _sight.nearClipPlane, _sight.aspect);

            Gizmos.matrix = temp;
        }
     }
    
    public void NotifyTargetKilled()
    {
        SetState(MonsterStates.IDLE);
    }

    public void SetDeadState()
    {
        SetState(MonsterStates.Dead);
    }

}
