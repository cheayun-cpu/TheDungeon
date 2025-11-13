using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManager : MonoBehaviour
{
    [Header("버튼 연결")]
    [SerializeField] private Button startButton;
    [SerializeField] private Button retryButton;
    [SerializeField] private Button homeButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        

        if (startButton != null)
            startButton.onClick.AddListener(GameManager.Instance.OnClickStart);

        if (retryButton != null)
            retryButton.onClick.AddListener(GameManager.Instance.OnClickRetry);

        if (homeButton != null)
            homeButton.onClick.AddListener(GameManager.Instance.OnClickHome);

        if (exitButton != null)
            exitButton.onClick.AddListener(GameManager.Instance.OnClickExit);

    }
}
