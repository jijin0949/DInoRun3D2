using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoController : MonoBehaviour
{
    public float moveSpeed;  // �󸶸�ŭ ������ �����̴��� ���ǵ尪
    public float moveSpeedX;  // x�� �� �¿�� �����̴� ���ǵ尪

    // ��ü�� �߽��� �� ��ġ
    public Vector3 sphereCenter;
    // ��ü�� ������
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

        // �� �������� �����ӿ� ���� ������ �δ� �ڵ� �ۼ�
        transform.position = new Vector3(Mathf.Clamp(transform.position.x, -3.9f, 3.9f), transform.position.y, transform.position.z);
    }

    void DoorCheck()
    {
        // ��ü ���� ���� Collider���� ����
        Collider[] hitColliders = Physics.OverlapSphere(transform.position + sphereCenter, sphereRadius);

        // ������ Collider�� ó��
        foreach (Collider doors in hitColliders)
        {
            if(doors.CompareTag("Goal"))
            {
                Debug.Log("Goal");
                doors.gameObject.GetComponent<BoxCollider>().enabled = false; //���� �ڽ� �ݶ��̴� ����
            }
            else
            {

            // ���⿡�� �浹�� Door�� Ÿ�԰� ���� ���� ���ڸ� �޾ƿͼ�
            int doorNumber = doors.gameObject.GetComponent<SelectDoors>().GetDoorNumber(transform.position.x);
            DoorType doorType = doors.gameObject.GetComponent<SelectDoors>().GetDoorType(transform.position.x);

            doors.gameObject.GetComponent<BoxCollider>().enabled = false; //���� �ڽ� �ݶ��̴� ����

            // DinoPositionController ��ũ��Ʈ���� �����ϰ� ��Ģ���꿡 �°� ��� �ؾ� �Ѵ�.
            dinoPositionController.SetDoorCalc(doorType, doorNumber);
            }
        }
    }

    // ��ü ������ Gizmo�� �ð�ȭ
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + sphereCenter, sphereRadius);
    }

}
