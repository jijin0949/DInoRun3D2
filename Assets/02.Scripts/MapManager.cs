using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    public GameObject[] mapPrefabs;
    public GameObject[] testMApPrefabs;
    public GameObject goalObject; //거리를 구하기 위한 오븢ㄷ트를 담기 위한 변수.

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    void Start()
    {
        CreatTestMap();
        //CreatMap();
        goalObject = GameObject.FindWithTag("Goal"); //Goal 오브젝트를 찾아서 대입해준다.
    }

    private void CreatTestMap()
    {
        Vector3 mapPosition = Vector3.zero;  // 초기 생성 위치는 원점으로 한다.

        for (int i = 0; i < testMApPrefabs.Length; i++)
        {
            GameObject selectedMap = testMApPrefabs[i];
            
            if (i > 0)
            {

                mapPosition.z += selectedMap.GetComponent<Map>().GetMapSize() / 2;  // 2번째 Map에서부터 이전의 Map의 크기의 반을 더해준다.
            }
            GameObject nowMap = Instantiate(selectedMap, mapPosition, Quaternion.identity); // 현재 만들 맵을 생성한다.
            mapPosition.z += nowMap.GetComponent<Map>().GetMapSize() / 2;    //현재 생성된 Map의 길이의 반을 더한다.
        }
    }

    private void CreatMap()
    {
        Vector3 mapPosition = Vector3.zero;  // 초기 생성 위치는 원점으로 한다.

        for(int i = 0; i < mapPrefabs.Length; i++)
        {
            GameObject selectedMap;
            if (i > 0)
            {
                selectedMap = mapPrefabs[Random.Range(0, mapPrefabs.Length)]; // 나머지는 랜덤
                mapPosition.z += selectedMap.GetComponent<Map>().GetMapSize() / 2;  // 2번째 Map에서부터 이전의 Map의 크기의 반을 더해준다.
            }
            else
            {
                selectedMap = mapPrefabs[0]; // 첫 번째 맵은 고정

            }
            GameObject nowMap = Instantiate(selectedMap, mapPosition, Quaternion.identity); // 현재 만들 맵을 생성한다.
            mapPosition.z += nowMap.GetComponent<Map>().GetMapSize() / 2;    //현재 생성된 Map의 길이의 반을 더한다.
        }
    }

    public float GetGoalDistance()
    {
        return goalObject.transform.position.z;
    }

}

