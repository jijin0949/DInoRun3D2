using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public bool isGameStart;  // false면 게임 시작 아님, true면 게임 시작

    public GameObject titlePanel;
    public GameObject gamePanel;
    public Slider progressBar;


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

    public void SetDistanceProgressBar()  //  프로그래스바 세팅하기 위한 함수
    {
        if (isGameStart.Equals(false))  // 게임이 시작하기 전에는 실행되지 않게 한다
        {
            return;
        }

        // 전체 거리중 Dino의 위치 거리 비율
        float goalDistance = DinoController.instance.transform.position.z / MapManager.instance.GetGoalDistance();
        //Debug.Log(goalDistance);
        progressBar.value = goalDistance;
    }

    public void GameStart()
    {
        Debug.Log("게임 시작");
        isGameStart = true;
        Time.timeScale = 1f;
        titlePanel.SetActive(false);
        gamePanel.SetActive(true);
    }


    void Start()
    {
        Time.timeScale = 0f;  // 전체 시간을 잠깐 멈춤
        progressBar.value = 0f;  // 간 거리는 0으로 세팅
        titlePanel.SetActive(true); // Title Panel은 활성화
        gamePanel.SetActive(false); // GamePanel은 비활성화
    }

    void Update()
    {
        SetDistanceProgressBar();
    }
}
