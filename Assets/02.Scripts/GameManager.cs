using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameStart; //거짓이면 게임시작 아님, 참이면 게임시작

    public GameObject titlePanel;
    public GameObject gamePanel;
    public Slider progressBar;
    private void Awake()
    {
        if(instance!= null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void SetDistanceProgressBar()
    {
        if(isGameStart.Equals(false)) //게임시작 전에 실행되지 않도록 한다.
        {
            return;
        }
        float goalDistance = DinoController.instance.transform.position.z / MapManager.instance.GetGoalDistance(); //전체 거리 중에 공룡의 위치 거리 비율.
        progressBar.value = goalDistance;
    }

    public void GameStart()
    {
        Debug.Log("게임 시작");
        isGameStart = true;
        Time.timeScale = 1f;
        titlePanel.SetActive(false); //버튼 비활성화
        gamePanel.SetActive(true);
    }
    void Start()
    {
        Time.timeScale = 0f;//전체 시간을 멈춤.
        progressBar.value = 0f; // 간 거리는 0으로 세팅
        titlePanel.SetActive(true);
        gamePanel.SetActive(false);
    }

    
    void Update()
    {
        SetDistanceProgressBar();
    }
}
