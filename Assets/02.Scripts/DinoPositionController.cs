using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoPositionController : MonoBehaviour
{
    public Transform raptors;      // Raptor들을 관리할 부모 오브젝트
    public GameObject raptorPrefab;     // 추가할 Raptor 프리팹

    public int visibleRaptorNumber;  // 보여주고 싶은 Raptor의 수를 입력
    public float initialRadius = 0f; // 첫 오브젝트의 반지름
    public float radiusGrowth = 0.12f;  // 오브젝트 간 반지름 증가량
    public float angleIncrement = 137.508f;  // 각도 증가 비율 (보통 골든 앵글 사용)


    void Update()
    {
        SetDinoPosition();
    }

    private void SetDinoPosition()
    {
        for (int i = 0; i < raptors.childCount; i++)
        {
            if (i > visibleRaptorNumber - 1)  // 보여주고 싶은 개수를 입력받은오브젝트보다 크면 화면에 안보이게 함 (i는 0부터 시작되므로 입력받은 수에서 1을 빼줘야 한다)
            {
                raptors.GetChild(i).gameObject.SetActive(false); // visibleRaptorNumber 보다 큰 오브젝트부터는 화면에 보이지 않게 함
                continue; // 이 아래의 계산은 하지 않는다 즉 continue 아래에 있는 코드들은 실행되지 않고, 바로 다음 루프(iteration)로 넘어감
            }
            else
            {
                if (i < visibleRaptorNumber)        // 보여주고 싶은 개수를 입력받은 오브젝트까지만 배치 각도를 계산
                {
                    // 반지름이 점점 커짐 피보나치 수열 효과
                    float currentRadius = initialRadius + (radiusGrowth * i);

                    // 각도가 점점 증가 (오브젝트가 계속 나선형으로 퍼져나감)
                    float angle = i * angleIncrement;

                    // 각도를 라디안 단위로 변환 후 좌표 계산
                    float x = Mathf.Cos(angle * Mathf.Deg2Rad) * currentRadius;
                    float z = Mathf.Sin(angle * Mathf.Deg2Rad) * currentRadius;

                    // 위치 설정
                    raptors.GetChild(i).localPosition = new Vector3(x, 0, z);
                    raptors.GetChild(i).gameObject.SetActive(true); // visibleRaptorNumber보다 작은 오브젝트부터는 화면에 보이게 함
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
            MinusRaptor(doorNumber);
        }
        else if (doorType.Equals(DoorType.Times)) // 곱하기
        {
            int raptorNum = (raptors.childCount * (doorNumber - 1));
            PlusRaptor(raptorNum);
        }
        else if (doorType.Equals(DoorType.Division)) // 나누기
        {
            int raptorNum = raptors.childCount - (raptors.childCount / doorNumber);
            MinusRaptor(raptorNum);
        }
    }

    private void PlusRaptor(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(raptorPrefab, raptors); //매개변수로 받은 number 수만큼 raptorPrefab을 생성시켜 줍니다.
        }
    }

    private void MinusRaptor(int number)
    {
        // 빼는 숫자가 현재 나의 Raptor숫자보다 더 크면
        if (number > raptors.childCount)
        {
            // 빼는 숫자를 현재 나의 Raptor수로 세팅해준다. ( 어차피 0이 될 것이므로 , 나중에 0이 되면 게임오버 시킬 것임 )
            number = raptors.childCount;
        }

        int raptorNum = raptors.childCount; // 현재 나의 Raptor숫자를 구하고

        // 맨 마지막 Raptor오브젝트부터 시작해서 전체 Raptor에서 문에 써져있는 숫자만큼 뺀 값보다 같거나 클때 까지 점점 i를 줄이면서
        for (int i = raptorNum - 1; i >= (raptorNum - number); i--)
        {
            Destroy(raptors.GetChild(i).gameObject);  // 맨 마지막 오브젝트부터 삭제 시킵니다.
        }
    }

}
