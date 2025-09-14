using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoPositionController : MonoBehaviour
{
    public Transform raptors;      // Raptor들을 관리할 부모 오브젝트
    public GameObject raptorPrefab;     // 추가할 Raptor 프리팹

    public int visivleRaptorNumber; // 보여주고 싶은 랩터 수
    public float initialRadius = 0; //반지름
    public float radiusGrowth = 0.12f; //반지름 증가량
    public float angleIncrement = 137.508f; //각도 증가 비율

    void Update()
    {
        SetDinoPosition();
    }

    private void SetDinoPosition()
    {
        for (int i = 0; i < raptors.childCount; i++)
        {
            if (i > visivleRaptorNumber - 1)
            {
                raptors.GetChild(i).gameObject.SetActive(false);  //  visivleRaptorNumber보다 작은 오브젝트부터는 화면에 보이지 않게 함.
                continue; // 이 아래의 계산은 하지 않는다 즉 continue 아래에 있는 코드들은 실행되지 않고, 바로 다음 루프로 넘어감
            }
            else 
            {
                if (i < visivleRaptorNumber)  // 보여주고 싶은 계수를 입력받은 오브젝트 까지만 각도를 계산
                {
                    // 반지름이 점점 커짐.(피보나치 수열 효과)
                    float currentRadius = initialRadius + (radiusGrowth * i);

                    // 각도가 점점 증가(나선형으로 퍼져나감.)
                    float angle = i * angleIncrement;



                    //각도를 라디안 단위로 변환 후 좌표 계산
                    float x = Mathf.Cos(angle * Mathf.Deg2Rad) * currentRadius;
                    float z = Mathf.Sin(angle * Mathf.Deg2Rad) * currentRadius;

                    // 새로운 위치로 자식 오브젝트를 위치시킴
                    raptors.GetChild(i).localPosition = new Vector3(x, 0, z);
                    raptors.GetChild(i).gameObject.SetActive(true); // visivleRaptorNumber보다 작은 오브젝트부터는 화면에 보이게 함.
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
            MinusRator(doorNumber);
        }
        else if (doorType.Equals(DoorType.Times)) // 곱하기
        {
            TimesRaptor(doorNumber);
        }
        else if (doorType.Equals(DoorType.Division)) // 나누기
        {
            DivisionRaptor(doorNumber);
        }
    }

    private void PlusRaptor(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(raptorPrefab, raptors); //매개변수로 받은 number 수만큼 raptorPrefab을 생성시켜 줍니다.
        }
    }

    private void MinusRator(int number)
    {
        if (number > raptors.childCount) //빼는 수가 현재 랩터 수보다 크면
        {
            number = raptors.childCount; //빼는 수를 현재 나의 랩터 수로 조정
        }

        int currentCount = raptors.childCount; //현재 나의 랩터 수

        for(int i = currentCount - 1; i>=(currentCount - number); i--)
        {
            Destroy(raptors.GetChild(i).gameObject); //맨 마지막 오브제트부터 삭제
        }
    }
    private void TimesRaptor(int number)
    {
        int currentCount = raptors.childCount;

        if(number<=1)
        {
            Debug.Log("number는 2 이상이어야 의미가 있습니다.");
            return;
        }

        int newCount = currentCount * (number - 1); //새로 생성할 랩터 수

        for (int i = 0; i < newCount; i++)
        {
            Instantiate(raptorPrefab, raptors); //매개변수로 받은 number 수만큼 raptorPrefab을 생성시켜 줍니다.
        }
    }
    private void DivisionRaptor(int number)
    {
        int currentCount = raptors.childCount;
        if (number <= 1)
        {
            Debug.Log("number는 2 이상이어야 의미가 있습니다.");
            return;
        }

        int newCount = currentCount / number;
        int deleteCount = currentCount - newCount;  // 삭제할 개수 = 전체 - 남길 수

        for (int i = currentCount - 1; i >= (currentCount - deleteCount); i--)
        {
            Destroy(raptors.GetChild(i).gameObject); //맨 마지막 오브제트부터 삭제
        }
    }

}
