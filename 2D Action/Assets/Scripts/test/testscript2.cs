using UnityEditor;
using UnityEngine;

public class testscript2 : MonoBehaviour
{
    private BoxCollider2D col;
    void Start()
    {
        col = GameObject.FindGameObjectWithTag("Player").GetComponent<BoxCollider2D>();
    }

    public void asd()
    {
        col.size = new Vector2(col.size.x, col.size.y * 0.9f);
    }
}
