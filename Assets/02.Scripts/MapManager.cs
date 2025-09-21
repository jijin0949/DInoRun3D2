using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    public GameObject[] mapPrefabs;

    public GameObject[] testMapPrefabs;  // 테스트 위한 변수

    public GameObject goalObject; // 거리를 구하기 위한 오브젝트를 담기 위한 변수.

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
        //CreatMap();
        CtreatTestMap();
        goalObject = GameObject.FindWithTag("Goal"); // Goal 오브젝트를 찾아서 대입해준다.
    }

    private void CtreatTestMap()
    {
        Vector3 mapPosition = Vector3.zero;

        for (int i = 0; i < testMapPrefabs.Length; i++)
        {
            GameObject selectedMap = testMapPrefabs[i]; // 만들 Map을 순서대로 선택한다.
            if (i > 0)
            {
                // 2번째 Map에서부터 이전의 Map의 크기의 반을 더해준다.
                mapPosition.z += selectedMap.GetComponent<Map>().GetMapSize() / 2;
            }
            GameObject nowMap = Instantiate(selectedMap, mapPosition, Quaternion.identity);
            mapPosition.z += nowMap.GetComponent<Map>().GetMapSize() / 2;    //현재 선택된 Map의 길이의 반을 더한다.
        }
    }



    private void CreatMap()
    {
        Vector3 mapPosition = Vector3.zero;

        for (int i = 0; i < 5; i++)
        {
            GameObject selectedMap;     // 만들 Map을 선택한다.
            if (i > 0)
            {
                selectedMap = mapPrefabs[Random.Range(1, mapPrefabs.Length)]; ;
                // 2번째 Map에서부터 이전의 Map의 크기의 반을 더해준다.
                mapPosition.z += selectedMap.GetComponent<Map>().GetMapSize() / 2;
            }
            else
            {
                selectedMap = mapPrefabs[0];
            }
            GameObject nowMap = Instantiate(selectedMap, mapPosition, Quaternion.identity);
            mapPosition.z += nowMap.GetComponent<Map>().GetMapSize() / 2;    //현재 선택된 Map의 길이의 반을 더한다.
        }
    }

    public float GetGoalDistance()
    {
        return goalObject.transform.position.z;
    }

    public int GetStage()
    {
        return PlayerPrefs.GetInt("Stage", 1); //PlayerPrefs는 정수, 부동 소수점, 문자열을 저장할 수 있음. 데이터를 키 - 값 쌍으로 저장한다.
    }

}
