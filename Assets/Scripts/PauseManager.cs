using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false; // �P�_�O�_�Ȱ�

    public void TogglePause()
    {
        isPaused = !isPaused; // �������A
        Time.timeScale = isPaused ? 0 : 1; // 0 �Ȱ��A1 ��_
        Debug.Log(isPaused ? "�C���Ȱ�" : "�C����_");
    }
}