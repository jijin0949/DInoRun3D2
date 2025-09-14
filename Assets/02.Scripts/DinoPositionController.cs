using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoPositionController : MonoBehaviour
{
    public Transform raptors;      // Raptor들을 관리할 부모 오브젝트
    public GameObject raptorPrefab;     // 추가할 Raptor 프리팹

    public float radius = 1f;      // 원의 반지름
    public float ratio = 0.1f;      // 배치 간격 비율 (작을수록 촘촘)

    void Update()
    {
        SetDinoPosition();
    }

    private void SetDinoPosition()
    {
        for (int i = 0; i < raptors.childCount; i++)
        {
            if (i > 8) // Raptor오브젝트가 10개째부터는 화면에 보여지지 않게 함 (i는 0부터 시작되므로 9가 10번째이다.)
            {
                raptors.GetChild(i).gameObject.SetActive(false);  // 10번째 오브젝트부터는 화면에 보이지 않게 함
                continue; // 이 아래의 계산은 하지 않는다 즉 continue 아래에 있는 코드들은 실행되지 않고, 바로 다음 루프로 넘어감
            }
            else // Raptor의 개수가 9개 이하일 때는 각도 계산해서 배치해줌
            {
                if (raptors.childCount < 10)  // 9개의 오브젝트까지만 배치 각도를 계산
                {
                    // 360도 각도 계산을 위한 각도 증가값
                    float angleStep = 360f / (raptors.childCount * ratio);

                    // 각 오브젝트의 배치 각도 계산
                    float angle = i * angleStep;

                    // 각도를 라디안으로 변환
                    float angleRad = angle * Mathf.Deg2Rad;

                    // X와 Z 좌표를 원형으로 계산
                    float x = Mathf.Cos(angleRad) * radius;
                    float z = Mathf.Sin(angleRad) * radius;

                    // 새로운 위치로 자식 오브젝트를 위치시킴
                    raptors.GetChild(i).localPosition = new Vector3(x, 0, z);
                }
            }
        }

    }

    public void SetDoorCalc(DoorType doorType, int doorNumber)
    {
        if (doorType.Equals(DoorType.Plus))  // 더하기
        {
            PlusRaptor(doorNumber);
        }
        else if (doorType.Equals(DoorType.Minus)) // 빼기
        {

        }
        else if (doorType.Equals(DoorType.Times)) // 곱하기
        {

        }
        else if (doorType.Equals(DoorType.Division)) // 나누기
        {

        }
    }

    private void PlusRaptor(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(raptorPrefab, raptors); //매개변수로 받은 number 수만큼 raptorPrefab을 생성시켜 줍니다.
        }
    }


}
