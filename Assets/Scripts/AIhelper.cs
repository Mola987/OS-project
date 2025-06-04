using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
public class AIhelper : MonoBehaviour
{
    [SerializeField] private GameObject helpUI; 

    public void ToggleHelpUI()
    {
        if (helpUI != null)
        {
            // ���� Help UI ���ҥΪ��A
            helpUI.SetActive(!helpUI.activeSelf);
        }
        else
        {
            Debug.LogWarning("Help UI is not assigned in the Inspector!");
        }
    }
}
