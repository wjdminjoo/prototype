using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ShowLogError] // 상속 받을 수 있게 만듦.
[ExecuteInEditMode] // 에디터 모드.
                    // 시작시 이 클래스를 시작으로 시작된다. (Awake같은것.)
                    // 시작을 눌러주지 않아도 실행이 됨.
public class sample : MonoBehaviour {

    // C++ Style 속성
    private int x;
    public int getX() { return x; }
    public int setX(int x) { return this.x = x; }

    [SerializeField] // 프라이빗 함수도 속성으로 빼겠다.
    // C# Style 속성
    private int private_x;
    public int public_x
    {
        get { return private_x; }
        set {if (value < 0)
                private_x = 0;
               else private_x = value; }
    }

    // AUTO 자동 구현 속성
    private string Name { get; set; }

    private void Update()
    {
        
    }

    // Use this for initialization
    void Start () {
        int x;
        AddVal(out x);
        
        RaycastHit hit;
        if(Physics.Raycast(Vector3.zero, transform.forward, out hit)){
            // structure(스트럭쳐) 스택에 들어가기 때문에 크기가 크면안된다.
            // 스트럭쳐는 간결하면서 빠르게 끝내야하는 용어를 사용할 때 사용한다.
            // ex ) Vector.
        }
	}
	
    private static void AddVal(out int v)
    {
        v = 10;
    }



    private void Awake()
    {
        GameLib.Log(this, "Awake");
    }
}
