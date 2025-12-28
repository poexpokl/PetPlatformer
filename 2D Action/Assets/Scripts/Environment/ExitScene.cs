using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitScene : MonoBehaviour
{
    [SerializeField] int nextSceneNumber;
    [SerializeField] Vector3 newPosition; //куда это?
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            int hp;
            int mana;
            collision.gameObject.GetComponent<ResourcesManager>().GetResources(out hp,out mana);
            bool flipX = collision.gameObject.GetComponent<SpriteRenderer>().flipX;
            SaveLoad.Instance.Load(newPosition, hp, mana, nextSceneNumber, flipX);
            //мб сделать гг неу€звимым
        }
    }
}
