using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public GameObject[] mapPrefabs;

    void Start()
    {
        Vector3 mapPosition = Vector3.zero;  // �ʱ� ���� ��ġ�� �������� �Ѵ�.

        for(int i = 0; i < mapPrefabs.Length; i++)
        {
            GameObject selectedMap;
            if (i ==0)
            {
                selectedMap = mapPrefabs[0]; // ù ��° ���� ����
            }
            else
            {
                selectedMap = mapPrefabs[Random.Range(0, mapPrefabs.Length)]; // �������� ����
                mapPosition.z += selectedMap.GetComponent<Map>().GetMapSize() / 2;  // 2��° Map�������� ������ Map�� ũ���� ���� �����ش�.

            }
            GameObject nowMap = Instantiate(selectedMap, mapPosition, Quaternion.identity); // ���� ���� ���� �����Ѵ�.
            mapPosition.z += nowMap.GetComponent<Map>().GetMapSize() / 2;    //���� ������ Map�� ������ ���� ���Ѵ�.
        }
    }
}
