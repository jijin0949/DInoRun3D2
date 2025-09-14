using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public enum DoorType
{
    Plus,
    Minus,
    Times,
    Division
}


public class SelectDoors : MonoBehaviour
{

    public SpriteRenderer rightDoorSpriteRD;  // ������ ���� ���� ������ ����
    public SpriteRenderer leftDoorSpriteRD;   // ���� ���� ���� ������ ����
    public TextMeshPro rightDoorText;   //������ ���� Text�� ������ ����
    public TextMeshPro leftDoorText;    //���� ���� Text�� ������ ����

    [SerializeField]
    private DoorType rightDoorType;     // ������ ���� ���¸� ������ ����
    public int rightDoorNumber;        // ������ ������ ���� ���� ����
    [SerializeField]
    private DoorType leftDoorType;      // ���� ���� ���¸� ������ ����
    public int leftDoorNumber;         // ���� ������ ���� ���� ����

    public Color goodColor;             // ������ ���� ����
    public Color badColor;              // ������ ���� ����

    void Start()
    {
        SettingDoors();
    }

    void Update()
    {

    }

    public void SettingDoors()
    {
        // ������ ��
        if (rightDoorType.Equals(DoorType.Plus))
        {
            rightDoorSpriteRD.color = goodColor;
            rightDoorText.text = "+" + rightDoorNumber;
        }
        else if(rightDoorType.Equals(DoorType.Minus))
        {
            rightDoorSpriteRD.color = badColor;
            rightDoorText.text = "-" + rightDoorNumber;
        }
        else if (rightDoorType.Equals(DoorType.Times)) // ���ϱ�
        {
            rightDoorSpriteRD.color = goodColor;
            rightDoorText.text = "x" + rightDoorNumber;
        }
        else if (rightDoorType.Equals(DoorType.Division)) // ������
        {
            rightDoorSpriteRD.color = badColor;
            rightDoorText.text = "/ " + rightDoorNumber;
        }


        // ���� �� ����
        if (leftDoorType.Equals(DoorType.Plus))
        {
            leftDoorSpriteRD.color = goodColor;
            leftDoorText.text = "+" + leftDoorNumber;
        }
        else if (leftDoorType.Equals(DoorType.Minus))
        {
            leftDoorSpriteRD.color = badColor;
            leftDoorText.text = "-" + leftDoorNumber;
        }
        else if (leftDoorType.Equals(DoorType.Times)) // ���ϱ�
        {
            leftDoorSpriteRD.color = goodColor;
            leftDoorText.text = "x" + leftDoorNumber;
        }
        else if (leftDoorType.Equals(DoorType.Division)) // ������
        {
            leftDoorSpriteRD.color = badColor;
            leftDoorText.text = "/ " + leftDoorNumber;
        }
    }

    public DoorType GetDoorType(float xPos)
    {
        if (xPos > 0) //Dino�� ��ġ���� 0���� ũ�� 
        {
            return rightDoorType;  // ������ �� Ÿ�� ��ȯ
        }
        else
        {
            return leftDoorType;   // ���� �� Ÿ�� ��ȯ
        }
    }

    public int GetDoorNumber(float xPos)
    {
        if (xPos > 0) //Dino�� ��ġ���� 0���� ũ�� 
        {
            return rightDoorNumber; //������ ���� �� ��ȯ 
        }
        else
        {
            return leftDoorNumber;  //���� ���� �� ��ȯ
        }
    }

}
