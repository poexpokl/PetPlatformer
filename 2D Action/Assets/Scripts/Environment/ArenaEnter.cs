using System;
using UnityEngine;

public class ArenaEnter : MonoBehaviour
{
    public event Action OnArena; //статичное событие?
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //проверка? 
            OnArena?.Invoke();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;//???
            //выключить объект?
        }
    }
}
