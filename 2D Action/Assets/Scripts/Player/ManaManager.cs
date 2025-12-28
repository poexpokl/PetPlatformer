using UnityEngine;
using UnityEngine.UI;

public class ManaManager : MonoBehaviour
{
    [SerializeField] private Sprite[] manaSprites;
    private Image manaImage;

    private void Awake()
    {
        manaImage = GetComponent<Image>();
    }

    public void UpdateMana(int mana)
    {
        manaImage.sprite = manaSprites[mana];
    }
}
