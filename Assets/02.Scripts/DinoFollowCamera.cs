using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoFollowCamera : MonoBehaviour
{
    public Transform target; // ���� ������Ʈ (Dino ������Ʈ)
    public Vector3 offset; // ī�޶��� ������ ��ġ (���� ���õ� ���ͺ� ī�޶� ��ġ)

    void Start()
    {
        offset = target.position - transform.position;  // Ÿ�ٰ� ī�޶� ������ �Ÿ�
    }
    void LateUpdate()
    {
        if (target != null)
        {
            // ī�޶��� ���ο� ��ġ ��� (Dino�� �� ��� �������� Z�ุ ����)
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, target.position.z - offset.z);

            // ī�޶� ��ġ�� ������Ʈ
            transform.position = newPosition;
        }
    }
}
