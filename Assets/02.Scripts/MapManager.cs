using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapManager : MonoBehaviour
{
    public static MapManager instance;
    public StageScriptableObject[] stages; // 스크랩터블 오브젝트로 만든 데이터를 담기 위한 변수.

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
        return PlayerPrefs.GetInt("Stage", 1); //PlayerPrefs는 정수, 부동 소수점, 문자열을 저장할 수 있음. 데이터를 키 - 값 쌍으로 저장한다.
    }

    void Start()
    {
        //CreatMap();
        CreateStage();
        goalObject = GameObject.FindWithTag("Goal"); // Goal 오브젝트를 찾아서 대입해준다.
    }

   
    public void CreateStage()
    {
        int currentStageIndex = GetStage();
        currentStageIndex = currentStageIndex % stages.Length;
        StageScriptableObject stage = stages[currentStageIndex];

        CreatMap(stage.maps);
    }


    private void CreatMap(Map[] stageMaps)
    {
        Vector3 mapPosition = Vector3.zero;

        for(int i =0; i<stageMaps.Length; i++)
        {
            Map selectedMap = stageMaps[i];
            if(i>0)
            {
                mapPosition.z += selectedMap.GetComponent<Map>().GetMapSize() / 2; 
            }
            Map nowMap = Instantiate(selectedMap, mapPosition, Quaternion.identity, transform);
            mapPosition.z += nowMap.GetComponent<Map>().GetMapSize() / 2;
        }
    }

    public float GetGoalDistance()
    {
        return goalObject.transform.position.z;
    }


}
