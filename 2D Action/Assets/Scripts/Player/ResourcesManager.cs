using UnityEngine;

public class ResourcesManager : MonoBehaviour
{
    public int hp { get; private set; } = 5;
    //public int damage { get; } = 5; //Переименовать класс в StatsManager
    private int maxHp = 5;
    public int mana { get; private set; } = 0;
    private int maxMana = 9;
    [SerializeField] HeartsManager hearthsManager;
    [SerializeField] ManaManager manaManager;
    public void ChangeHp(int hpDifference)
    {
        hp += hpDifference;
        if (hp > maxHp)
            hp = maxHp;
        hearthsManager.UpdateHearts(hp);
    }
    public void ChangeMana(int manaDifference)
    {
        mana += manaDifference;
        if (mana > maxMana)
            mana = maxMana;
        manaManager.UpdateMana(mana);
    }

    public bool CanUseMana(int usedMana)//нейминг?
    {
        return mana >= usedMana;
    }

    public void ChangeHp() //удалить
    {
        hp -= 1;
        if (hp > maxHp)
            maxHp = hp;
        if(hp < 0)
            hp = 0;
        hearthsManager.UpdateHearts(hp);
    }

    public void ChangeMana() //удалить
    {
        mana += 1;
        if (mana > maxMana)
            mana = maxMana;
        manaManager.UpdateMana(mana);
    }

    public void GetResources(out int hp, out int mana)
    {
        hp = this.hp;
        mana = this.mana;
    }
}
