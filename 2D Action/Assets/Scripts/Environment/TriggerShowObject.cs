using UnityEngine;

public class TriggerShowObject : MonoBehaviour //переименовать
{
    [SerializeField] private GameObject objectToShow; //переименовать
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
            objectToShow.SetActive(true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            objectToShow.SetActive(false);
    }
}
