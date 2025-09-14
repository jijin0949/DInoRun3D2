using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DinoFollowCamera : MonoBehaviour
{
    public Transform target; // 따라갈 오브젝트 (Dino 오브젝트)
    public Vector3 offset; // 카메라의 고정된 위치 (현재 세팅된 쿼터뷰 카메라 위치)

    void Start()
    {
        offset = target.position - transform.position;  // 타겟과 카메라 사이의 거리
    }
    void LateUpdate()
    {
        if (target != null)
        {
            // 카메라의 새로운 위치 계산 (Dino가 좌 우로 움직여도 Z축만 따라감)
            Vector3 newPosition = new Vector3(transform.position.x, transform.position.y, target.position.z - offset.z);

            // 카메라 위치를 업데이트
            transform.position = newPosition;
        }
    }
}
