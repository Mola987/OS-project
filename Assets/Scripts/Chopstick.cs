using UnityEngine;



public class Chopstick : MonoBehaviour

{

    public SpriteRenderer spriteRenderer;

    public bool isUsed = true;

    private void Update()

    {

        if (isUsed)
        {
            spriteRenderer.color = Color.red;

        }

        else if (!isUsed)

        {

            spriteRenderer.color = Color.black;

        }

    }

}