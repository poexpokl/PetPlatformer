using Unity.Cinemachine;
using UnityEngine;

public class SpikesArena : MonoBehaviour
{
    private Vector2 lastPosition;
    [SerializeField] private Vector2 newPosition;
    [SerializeField] Spikes spikes;
    [SerializeField] GameObject platform;
    [SerializeField] Vector3 platformPosition;
    [SerializeField] GameObject[] lastGroundSetters;
    private LastGroundPosiition lastGroundPosiition;
    private void OnEnable()
    {
        lastGroundPosiition = G.player.GetComponent<LastGroundPosiition>();
        lastPosition = lastGroundPosiition.Position;
        lastGroundPosiition.Position = newPosition;
        spikes.PlayerOnSpike += CreatePlatform;
        foreach (GameObject setter in lastGroundSetters)
        {
            setter.SetActive(false);
        }
    }

    private void OnDisable()
    {
        lastGroundPosiition.Position = lastPosition;
        spikes.PlayerOnSpike -= CreatePlatform;
        foreach (GameObject setter in lastGroundSetters)
        {
            setter.SetActive(true);
        }
    }

    private void CreatePlatform()
    {
        Instantiate(platform, platformPosition, Quaternion.identity);
    }
}
