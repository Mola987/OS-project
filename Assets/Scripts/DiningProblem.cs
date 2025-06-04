using TMPro;
using UnityEngine;
using System.Collections.Generic;

public class DiningProblem : MonoBehaviour
{
    [SerializeField] private GameObject table; // 圓形桌子
    [SerializeField] private GameObject philosopherPrefab; // 哲學家的預製體
    [SerializeField] private int philosopherCount; // 哲學家的數量
    [SerializeField] private float tableRadius; // 圓形桌子的半徑
    [SerializeField] private TMP_InputField philosopherCountInputField; // 用於輸入哲學家數量
    [SerializeField] private TMP_InputField passSpeedInputField; // 用於輸入傳遞筷子的速度

    private float timer = 2;
    private float initualtime = 2f;
    private GameObject[] philosophers; // 儲存哲學家的物件
    private List<GameObject> chopstick2List = new List<GameObject>(); // 管理所有 chopstick2 的列表
    private int activeChopstickIndex = 0; // 當前啟用的 chopstick2 索引

    void Start()
    {
        SpawnPhilosophers();
        InitializeChopsticks();

        // 添加 TMP_InputField 監聽事件
        if (philosopherCountInputField != null)
        {
            philosopherCountInputField.onEndEdit.AddListener(UpdatePhilosopherCount);
        }

        if (passSpeedInputField != null)
        {
            passSpeedInputField.onEndEdit.AddListener(UpdatePassSpeed);
        }
    }

    private void Update()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        else if (timer <= 0)
        {
            PassChopstick();
            timer = initualtime;
        }
    }

    private void SpawnPhilosophers()
    {
        if (table == null)
        {
            Debug.LogError("Table object is not assigned!");
            return;
        }

        Vector3 tablePosition = table.transform.position; // 桌子的位置
        philosophers = new GameObject[philosopherCount];

        for (int i = 0; i < philosopherCount; i++)
        {
            float angle = i * Mathf.PI * 2 / philosopherCount;
            Vector3 position = new Vector3(
                tablePosition.x + tableRadius * Mathf.Cos(angle),
                tablePosition.y + tableRadius * Mathf.Sin(angle),
                tablePosition.z
            );

            GameObject philosopher = Instantiate(philosopherPrefab, position, Quaternion.identity, transform);

            Vector3 directionToCenter = (tablePosition - position).normalized;
            float angleToCenter = Mathf.Atan2(directionToCenter.y, directionToCenter.x) * Mathf.Rad2Deg;
            philosopher.transform.rotation = Quaternion.Euler(0, 0, angleToCenter + 90);

            philosophers[i] = philosopher;

            Transform chopstick2 = philosopher.transform.Find("chopstick2");
            if (chopstick2 != null)
            {
                chopstick2List.Add(chopstick2.gameObject);
            }
        }
    }

    private void InitializeChopsticks()
    {
        for (int i = 0; i < chopstick2List.Count; i++)
        {
            chopstick2List[i].SetActive(false);
        }

        if (chopstick2List.Count > 0)
        {
            chopstick2List[activeChopstickIndex].SetActive(true);
        }
    }

    public void PassChopstick()
    {
        chopstick2List[activeChopstickIndex].SetActive(false);
        activeChopstickIndex = (activeChopstickIndex + 1) % philosopherCount;
        chopstick2List[activeChopstickIndex].SetActive(true);
    }

    private void UpdatePhilosopherCount(string input)
    {
        if (int.TryParse(input, out int newCount) && newCount > 0)
        {
            philosopherCount = newCount;

            foreach (var philosopher in philosophers)
            {
                if (philosopher != null)
                {
                    Destroy(philosopher);
                }
            }
            chopstick2List.Clear();
            SpawnPhilosophers();
            InitializeChopsticks();
        }
        else
        {
            Debug.LogWarning("Invalid philosopher count entered.");
        }
    }

    private void UpdatePassSpeed(string input)
    {
        if (float.TryParse(input, out float newSpeed) && newSpeed > 0)
        {
            initualtime = newSpeed;
            timer = newSpeed;
        }
        else
        {
            Debug.LogWarning("Invalid pass speed entered.");
        }
    }
}