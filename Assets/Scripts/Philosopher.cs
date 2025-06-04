using UnityEngine;
using UnityEngine.UI;
public class Philosopher : MonoBehaviour
{
    public enum State { Thinking, Hungry, Eating }
    public State currentState = State.Thinking;
    public SpriteRenderer spriteRenderer;
    public Chopstick leftChopstick;
    public Chopstick rightChopstick;
    public Text warningText;
    public GameObject warningTextUI;
    private float thinkTime;
    private float eatTime;
    private static Philosopher[] allPhilosophers;
    private static float deadlockCheckInterval = 5f; // �����ˬd���j
    private static float timeSinceLastCheck = 0f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        thinkTime = Random.Range(3, 7);
        eatTime = Random.Range(2, 5);

        if (allPhilosophers == null)
        {
            allPhilosophers = FindObjectsByType<Philosopher>(FindObjectsSortMode.None);
        }
    }

    void Update()
    {
        switch (currentState)
        {
            case State.Thinking:
                spriteRenderer.color = Color.blue;
                thinkTime -= Time.deltaTime;
                if (thinkTime <= 0)
                {
                    currentState = State.Hungry;
                    thinkTime = Random.Range(3, 7);
                }
                break;
            case State.Hungry:
                spriteRenderer.color = Color.red;
                if (!leftChopstick.isUsed && !rightChopstick.isUsed)
                {
                    leftChopstick.isUsed = true;
                    rightChopstick.isUsed = true;
                    currentState = State.Eating;
                    eatTime = Random.Range(2, 5);
                }
                break;
            case State.Eating:
                spriteRenderer.color = Color.green;
                eatTime -= Time.deltaTime;
                if (eatTime <= 0)
                {
                    leftChopstick.isUsed = false;
                    rightChopstick.isUsed = false;
                    currentState = State.Thinking;
                }
                break;
        }

        // �ˬd���ꪬ�A
        timeSinceLastCheck += Time.deltaTime;
        if (timeSinceLastCheck >= deadlockCheckInterval)
        {
            CheckAndResolveDeadlock();
            timeSinceLastCheck = 0f;
        }
    }

    private void CheckAndResolveDeadlock()
    {
        // �ˬd�O�_�Ҧ����Ǯa���B�� Hungry ���A
        bool isDeadlock = true;
        foreach (var philosopher in allPhilosophers)
        {
            if (philosopher.currentState != State.Hungry)
            {
                isDeadlock = false;
                break;
            }
        }

        if (isDeadlock)
        {
            warningTextUI.SetActive(true);
            warningText.text = "����o�͡A���ի�_�I";
            Debug.Log("����o�͡A���ի�_�I");
            // ���Ҧ����Ǯa��U�_�l
            foreach (var philosopher in allPhilosophers)
            {
                philosopher.leftChopstick.isUsed = false;
                philosopher.rightChopstick.isUsed = false;
            }
            warningText.text = "�Ҧ����Ǯa��U�_�l�A����Ѱ��C";
            warningTextUI.SetActive(false);
            Debug.Log("�Ҧ����Ǯa��U�_�l�A����Ѱ��C");
        }
    }
}
