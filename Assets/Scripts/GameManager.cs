using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public bool isDie=false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            //DontDestroyOnLoad(gameObject);
        }
        else
        { 
            Destroy(gameObject);
        }
}
    private void Start()
    {
        CursorModeInPopup();
    }

    private void Update()
    {
        if (isDie)
        {
            GameOver();
        }
    }
    private void CursorModeInGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        UIManager.uiManager.ShowPointerUI();
    }

    public void CursorModeInPopup()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UIManager.uiManager.ClosePointerUI();
    }

    public void OnClickStart()
    {
        Debug.Log("게임 시작!");
        isDie = false;
        SceneManager.LoadScene("MainScene");
        CursorModeInGame();


    }

    public void OnClickExit()
    {
#if     UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void OnClickRetry()
    {
        Debug.Log("게임 재시작!");
        isDie = false;
        SceneManager.LoadScene("MainScene");
        CursorModeInGame();
        
    }

    public void OnClickHome()
    {
        Debug.Log("시작화면 돌아가기");
        isDie = false;
        SceneManager.LoadScene("StartScene");
        CursorModeInPopup();
    }

    private void GameOver()
    {
        UIManager.uiManager.ShowGameOverUI();
        CursorModeInPopup();
    }

    

}

   

    
    

   

