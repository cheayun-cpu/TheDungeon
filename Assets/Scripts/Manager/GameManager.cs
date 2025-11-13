using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
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
        
        SceneManager.LoadScene("MainScene");
        UIManager.uiManager.CloseGameOverUI();
        CursorModeInGame();
        
    }

    public void OnClickHome()
    {
        Debug.Log("시작화면 돌아가기");
        SceneManager.LoadScene("StartScene");
        CursorModeInPopup();
    }

    public void GameOver()
    {
        UIManager.uiManager.ShowGameOverUI();
        CursorModeInPopup();
    }
}

   

    
    

   

