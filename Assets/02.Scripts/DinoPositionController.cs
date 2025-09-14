using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoPositionController : MonoBehaviour
{
    public Transform raptors;      // Raptor���� ������ �θ� ������Ʈ
    public GameObject raptorPrefab;     // �߰��� Raptor ������

    public int visivleRaptorNumber; // �����ְ� ���� ���� ��
    public float initialRadius = 0; //������
    public float radiusGrowth = 0.12f; //������ ������
    public float angleIncrement = 137.508f; //���� ���� ����

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
                raptors.GetChild(i).gameObject.SetActive(false);  //  visivleRaptorNumber���� ���� ������Ʈ���ʹ� ȭ�鿡 ������ �ʰ� ��.
                continue; // �� �Ʒ��� ����� ���� �ʴ´� �� continue �Ʒ��� �ִ� �ڵ���� ������� �ʰ�, �ٷ� ���� ������ �Ѿ
            }
            else 
            {
                if (i < visivleRaptorNumber)  // �����ְ� ���� ����� �Է¹��� ������Ʈ ������ ������ ���
                {
                    // �������� ���� Ŀ��.(�Ǻ���ġ ���� ȿ��)
                    float currentRadius = initialRadius + (radiusGrowth * i);

                    // ������ ���� ����(���������� ��������.)
                    float angle = i * angleIncrement;



                    //������ ���� ������ ��ȯ �� ��ǥ ���
                    float x = Mathf.Cos(angle * Mathf.Deg2Rad) * currentRadius;
                    float z = Mathf.Sin(angle * Mathf.Deg2Rad) * currentRadius;

                    // ���ο� ��ġ�� �ڽ� ������Ʈ�� ��ġ��Ŵ
                    raptors.GetChild(i).localPosition = new Vector3(x, 0, z);
                    raptors.GetChild(i).gameObject.SetActive(true); // visivleRaptorNumber���� ���� ������Ʈ���ʹ� ȭ�鿡 ���̰� ��.
                }
            }
        }

    }

    public void SetDoorCalc(DoorType doorType, int doorNumber)
    {
        if (doorType.Equals(DoorType.Plus))  // ���ϱ�
        {
            PlusRaptor(doorNumber);
        }
        else if (doorType.Equals(DoorType.Minus)) // ����
        {
            MinusRator(doorNumber);
        }
        else if (doorType.Equals(DoorType.Times)) // ���ϱ�
        {
            TimesRaptor(doorNumber);
        }
        else if (doorType.Equals(DoorType.Division)) // ������
        {
            DivisionRaptor(doorNumber);
        }
    }

    private void PlusRaptor(int number)
    {
        for (int i = 0; i < number; i++)
        {
            Instantiate(raptorPrefab, raptors); //�Ű������� ���� number ����ŭ raptorPrefab�� �������� �ݴϴ�.
        }
    }

    private void MinusRator(int number)
    {
        if (number > raptors.childCount) //���� ���� ���� ���� ������ ũ��
        {
            number = raptors.childCount; //���� ���� ���� ���� ���� ���� ����
        }

        int currentCount = raptors.childCount; //���� ���� ���� ��

        for(int i = currentCount - 1; i>=(currentCount - number); i--)
        {
            Destroy(raptors.GetChild(i).gameObject); //�� ������ ������Ʈ���� ����
        }
    }
    private void TimesRaptor(int number)
    {
        int currentCount = raptors.childCount;

        if(number<=1)
        {
            Debug.Log("number�� 2 �̻��̾�� �ǹ̰� �ֽ��ϴ�.");
            return;
        }

        int newCount = currentCount * (number - 1); //���� ������ ���� ��

        for (int i = 0; i < newCount; i++)
        {
            Instantiate(raptorPrefab, raptors); //�Ű������� ���� number ����ŭ raptorPrefab�� �������� �ݴϴ�.
        }
    }
    private void DivisionRaptor(int number)
    {
        int currentCount = raptors.childCount;
        if (number <= 1)
        {
            Debug.Log("number�� 2 �̻��̾�� �ǹ̰� �ֽ��ϴ�.");
            return;
        }

        int newCount = currentCount / number;
        int deleteCount = currentCount - newCount;  // ������ ���� = ��ü - ���� ��

        for (int i = currentCount - 1; i >= (currentCount - deleteCount); i--)
        {
            Destroy(raptors.GetChild(i).gameObject); //�� ������ ������Ʈ���� ����
        }
    }

}
