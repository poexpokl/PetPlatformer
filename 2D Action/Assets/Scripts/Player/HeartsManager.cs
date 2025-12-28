using UnityEngine;
using UnityEngine.UI;

public class HeartsManager : MonoBehaviour
{
    [SerializeField] private Sprite[] heartSprites;
    [SerializeField] private Image[] hearts;
    private int maxHearts = 5;
    public void UpdateHearts(int wholeHearts)
    {
        if (wholeHearts < 0)
            wholeHearts = 0;
        for (int i = 0; i < maxHearts; i++)
        {
            if (i < wholeHearts)
                hearts[i].sprite = heartSprites[0];
            else
                hearts[i].sprite = heartSprites[1];
        }
    }
}
