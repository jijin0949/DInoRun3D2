using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    public StageScriptableObject[] stages; // 스크립터블 오브젝트로 만든 Data를 담기 위한 변수

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

    public int GetStage()
    {
        return PlayerPrefs.GetInt("Stage", 1);
    }


    void Start()
    {
        //CreatMap();
        CreateStage();
        goalObject = GameObject.FindWithTag("Goal"); // Goal 오브젝트를 찾아서 대입해준다.
    }

    private void CreateStage()
    {
        int currentStageIndex = GetStage();
        currentStageIndex = currentStageIndex % stages.Length; // 이렇게 하면 stages의 범위를 벗어나는 경우가 없을 것이다.
        StageScriptableObject stage = stages[currentStageIndex];

        CtreatMap(stage.maps);
    }

    private void CtreatMap(Map[] stageMaps)
    {
        Vector3 mapPosition = Vector3.zero;

        for (int i = 0; i < stageMaps.Length; i++)
        {
            Map selectedMap = stageMaps[i]; // 만들 Map을 순서대로 선택한다.
            if (i > 0)
            {
                // 2번째 Map에서부터 이전의 Map의 크기의 반을 더해준다.
                mapPosition.z += selectedMap.GetComponent<Map>().GetMapSize() / 2;
            }
            Map nowMap = Instantiate(selectedMap, mapPosition, Quaternion.identity, transform);
            mapPosition.z += nowMap.GetComponent<Map>().GetMapSize() / 2;    //현재 선택된 Map의 길이의 반을 더한다.
        }
    }

    public float GetGoalDistance()
    {
        return goalObject.transform.position.z;
    }
}
