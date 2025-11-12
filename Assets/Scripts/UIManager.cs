using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UIManager;


public class UIManager : MonoBehaviour
{
    public GameObject BloodScreenUI;
    public GameObject DescriptionUI;
    public GameObject GameOverUI;
    public GameObject GameStartUI;
    public GameObject PointerUI;
    public GameObject HealthUI3;
    public GameObject HealthUI2;
    public GameObject CantGetUI;
    


    [SerializeField] private TMP_Text descriptionUI;

    static public UIManager uiManager;
    
     
          
        


void Awake()
{
        uiManager = this;
}
    private void CursorModeInGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    public void CursorModeInPopup()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private IEnumerator HideAfterSeconds(GameObject uiObject, float delay)//ui 시간차 두고 켰다 껐다 할 수 있게 하기
    {
        yield return new WaitForSeconds(delay);
        uiObject.SetActive(false);
    }

    public void ShowBloodScreen() //피격을 받았을 때   
    {
        Debug.Log("혈흔 UI 표시");
        BloodScreenUI.SetActive(true);
        StartCoroutine(HideAfterSeconds(BloodScreenUI, 2f));
    }
    
    public void ShowDescriptionUI(string description)
    {
        DescriptionUI.SetActive(true);
        descriptionUI.text = description;

    }

    public void CloseDescriptionUI()
    {
        DescriptionUI.SetActive(false);
    }

    public void ShowGameOverUI()//유저의 체력이 0이 됬을 때
    {
       // Debug.Log("게임오버 UI 표시");
        GameOverUI.SetActive(true);
    }


    

    public void ShowPointerUI()
    {
        PointerUI.SetActive(true);
    }

    public void ClosePointerUI()
    {
        PointerUI.SetActive(false);
    }

    public void ChangeHealthUI(int curHP)
    {
        if(curHP==2)
        {
            HealthUI3.SetActive(false);
        }

        else if (curHP == 1)
        {
            HealthUI3.SetActive(false);
            HealthUI2.SetActive(false);
        }

    }
    public void CloseGameOverUI()
    {
        GameOverUI.SetActive(false);
    }


    public void CantGetItem()
    {
        CantGetUI.SetActive(true);
        StartCoroutine(HideAfterSeconds(CantGetUI, 2f));
    }
}

