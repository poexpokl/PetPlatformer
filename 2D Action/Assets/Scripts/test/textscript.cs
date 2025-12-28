using UnityEngine;

public class textscript : MonoBehaviour
{
    public PlayerController playerController;

    public void Damage()
    {
        playerController.GetDamage(Vector2.zero);
    }
}
