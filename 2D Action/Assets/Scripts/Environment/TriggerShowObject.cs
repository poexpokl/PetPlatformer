using UnityEngine;

public class TriggerShowObject : MonoBehaviour
{
    [SerializeField] private GameObject objectToShow;
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
