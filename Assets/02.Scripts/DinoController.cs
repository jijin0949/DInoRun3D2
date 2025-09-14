using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoController : MonoBehaviour
{
    public float moveSpeed;  // 얼마만큼 빠르게 움직이는지 스피드값
    public float moveSpeedX;  // x축 즉 좌우로 움직이는 스피드값

    // 구체의 중심이 될 위치
    public Vector3 sphereCenter;
    // 구체의 반지름
    public float sphereRadius = 0.5f;

    public DinoPositionController dinoPositionController;

    void Start()
    {
        
    }

    void Update()
    {
        DinoMove();
        DoorCheck();
    }

    private void DinoMove()
    {
        transform.position += Vector3.forward * Time.deltaTime * moveSpeed;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Translate(-moveSpeedX * Time.deltaTime, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Translate(moveSpeedX * Time.deltaTime, 0, 0);
        }

        // 맨 마지막에 움직임에 대한 제한을 두는 코드 작성
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.9f, 3.9f), transform.position.y, transform.position.z);
    }

    void DoorCheck()
    {
        // 구체 영역 내의 Collider들을 감지
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + sphereCenter, sphereRadius);

        // 감지된 Collider들 처리
        foreach (Collider doors in hitColliders)
        {
            if(doors.CompareTag("Goal"))
            {
                Debug.Log("Goal");
                doors.gameObject.GetComponent<BoxCollider>().enabled = false; //문의 박스 콜라이더 끄기
            }
            else
            {

            // 여기에서 충돌한 Door의 타입과 문에 써진 숫자를 받아와서
            int doorNumber = doors.gameObject.GetComponent<SelectDoors>().GetDoorNumber(transform.position.x);
            DoorType doorType = doors.gameObject.GetComponent<SelectDoors>().GetDoorType(transform.position.x);

            doors.gameObject.GetComponent<BoxCollider>().enabled = false; //문의 박스 콜라이더 끄기

            // DinoPositionController 스크립트에서 적절하게 사칙연산에 맞게 계산 해야 한다.
            dinoPositionController.SetDoorCalc(doorType, doorNumber);
            }
        }
    }

    // 구체 영역을 Gizmo로 시각화
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + sphereCenter, sphereRadius);
    }

}
