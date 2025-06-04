using UnityEngine;

public class PauseManager : MonoBehaviour
{
    private bool isPaused = false; // §PÂ_¬O§_¼È°±

    public void TogglePause()
    {
        isPaused = !isPaused; // ¤Á´«ª¬ºA
        Time.timeScale = isPaused ? 0 : 1; // 0 ¼È°±¡A1 «ì´_
        Debug.Log(isPaused ? "¹CÀ¸¼È°±" : "¹CÀ¸«ì´_");
    }
}