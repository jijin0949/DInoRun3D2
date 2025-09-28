using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    enum State
    {
        Idle, //대기 상태에서는 애니메이션 멈춤
        Run // 랩터에게 달려오는 상태
    }

    public float moveSpeed;//달려오는 속도
    public float detectRadius;//감지되는 범위의 반지름
    private State state;//적의 상태를 나타낼 변수
    private Transform targetRaptor;//우리의 랩터

    void Start()
    {
        GetComponent<Animator>().speed = 0f;//애니메이션 시간을 0으로 세팅해줘 멈춰있게 함
    }

    void Update()
    {
        SetState();
    }

    private void SetState()
    {
        switch(state)
        {
            case State.Idle:
                DetectDino();
                break;

            case State.Run:
                GoTODino();
                break;

        }
    }

    private void DetectDino() //디노를 찾고 있는 함수. 업데이트에서 항상 작동 중
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, detectRadius);

        foreach(Collider colls in hitColliders) // 감지된 Collider들을 감지
        {
           //검색된 곳에 디노가 있다면
            if(colls.gameObject.GetComponent<Raptor>()!=null)
            {
                if (colls.gameObject.GetComponent<Raptor>().IsTarget())
                    continue;

                    colls.gameObject.GetComponent<Raptor>().SetTarget();//충돌 오브젝트에 타겟으로 지정됐다고 스위치 키기

                    targetRaptor = colls.gameObject.transform; //충돌 오브젝트를 targetRaptor로 지정해줌
                    StartGoTODino(); //상태 바꿔주는 함수 실행
                
            }
        }
    }

    private void StartGoTODino()//찾았을 때 작동하는 함수
    {
        state = State.Run;
        GetComponent<Animator>().speed = 1f;
    }

    private void GoTODino()//찾고난 후 디노에게 달려가는 함수
    {
        if(targetRaptor == null)
        {
            return;
        }

        transform.position = Vector3.MoveTowards(transform.position, targetRaptor.position, Time.deltaTime * moveSpeed);

        if(Vector3.Distance(transform.position, targetRaptor.position) < 0.1f)
        {
            Destroy(targetRaptor.gameObject);
            Destroy(this.gameObject);
        }
    }
}
