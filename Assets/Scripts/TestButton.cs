using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestButton : MonoBehaviour
{
   public void Funthion1()
    {
        GameManager.Instance.OnClickRetry();
    }

    public void Funthion2()
    {
        GameManager.Instance.OnClickHome();
    }
}
