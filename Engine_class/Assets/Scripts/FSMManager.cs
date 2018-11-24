using System.Collections;
using System.Collections.Generic;
using UnityEngine;




public enum PlayerState
{
    IDLE = 0,
    Run,
    Chase,
    Attack,
    Dead
}

[RequireComponent(typeof(playerState))]
[ExecuteInEditMode]
public class FSMManager : MonoBehaviour, IFSMManager {

    private bool _isinit = false;

    public PlayerState startState = PlayerState.IDLE;
    private Dictionary<PlayerState, FSMState> _state = new Dictionary<PlayerState, FSMState>();

    // 프로퍼티 방식.
    
    private Transform _marker;
    public Transform Marker
    {
        get { return _marker; }
    }


    // 스테이트값을 바로 알아볼 수 있도록 빼준다.
    public FSMState CurrentSateComponent
    {
        get { return _state[CurrntState]; }
    }


    [SerializeField] // 에디터에선 볼 수 있지만 코드에선 은닉이 유지 되어비림~~!
    private PlayerState _currentState;
    public PlayerState CurrntState{get { return _currentState; }}

    private CharacterController _cc;
    public CharacterController CC { get { return _cc; } }

    public int clickLayer = 0;

    private playerState _playerState;
    public playerState playerState { get { return _playerState; } }

    private Transform _target;
    public Transform target { get { return _target; } }


    private Animator _anim;
    //public Animator { get { return _anim; } }

    private Camera _sight;
    public Camera sight { get { return _sight; } }

    // 시야의 넓이.
    public int sightaspectRatio = 3;


    public CharacterController _testTarget;
    //public CharacterController testTarget { get { return _testTarget; } }


    private void Start()
    {
        SetState(startState);
        _isinit = true;
    }

    private void Awake()
    {
        // 레이어마스크에서 가져오는 방법.
        clickLayer = (1 << 9) +  (1 << 10); // name 프레이어 라는 함수를 제공해줌.
        

        _marker = GameObject.FindGameObjectWithTag("Marker").transform;
        if (null == _marker)
        {
            GameLib.Log(this, "Marker Error");
            return;
        }

        _cc = GetComponent<CharacterController>();
        _playerState = GetComponent<playerState>();
        // _animation = GetComponentInChildren<Animation>();
        _anim = GetComponentInChildren<Animator>();
        // 카메라로 인한 인식을 위해 설정.
        _sight = GetComponentInChildren<Camera>();
        _sight.aspect = sightaspectRatio;


        PlayerState[] stateValue = (PlayerState[])System.Enum.GetValues(typeof(PlayerState)); // 시스템 정보를 배열형식으로 요청하여 가져옴.
        foreach(var s in stateValue) // var 대신 PlayerState 써도됨. var가 뭔지 알아보기.
        {
            System.Type FSMType = System.Type.GetType("Player" + s.ToString());
            FSMState state = (FSMState)GetComponent(FSMType);
            if (null == state) state = (FSMState)gameObject.AddComponent(FSMType); // 만약에 컴포넌트가 없으면 넣어버린다.
                                                                                   // 예외값 처리인듯
                                                                                   // 미리 NULL인지 아닌지 해주는게 좋기는 함.
            _state.Add(s, state);
            //GameLib.Log(this ,s.ToString("G"));
            state.enabled = false; // 로그를 전부다 꺼준다.
        }
        
      
    }



    public void SetState(PlayerState newState)
    {
        if (_isinit){
            _state[_currentState].enabled = false;
            _state[_currentState].EndState();
            GameLib.Log(this, "False");
        }
        _currentState = newState;
        _state[_currentState].BeginState();
        _state[_currentState].enabled = true; // 지정한 친구만 로그를 보여준다.
        _anim.SetInteger("CurrentState", (int)_currentState);

    }


    public void Update()
    {
        // 마우스 클릭시 상태 변환
        if (Input.GetMouseButtonDown(0))
        {
            // 상태 변동
            //if (CurrntState == PlayerState.IDLE) SetState(PlayerState.Run);
            //else SetState(PlayerState.IDLE);

            // 클릭시 선이 보임
            Ray r = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rayhit;
            

            if (Physics.Raycast(r, out rayhit, 10000.0f, clickLayer))
            {

                if (rayhit.transform.gameObject.layer == 9)
                {
                    _marker.position = rayhit.point;
                    SetState(PlayerState.Run);

                }
                else if (rayhit.transform.gameObject.layer == 10)
                {
                    GameLib.Log(this, "Monster");
                    _target = rayhit.transform;
                    SetState(PlayerState.Chase);
                }
                _marker.position = rayhit.point;
                // 마커를 가져오는 로직.
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(_sight != null)
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
        SetState(PlayerState.IDLE);
    }

    public void SetDeadState()
    {
        SetState(PlayerState.Dead);
    }


}
