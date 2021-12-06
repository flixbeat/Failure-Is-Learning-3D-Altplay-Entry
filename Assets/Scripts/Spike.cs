using UnityEngine;

public class Spike : MonoBehaviour
{
    // collides with player
    private void OnCollisionEnter(Collision other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        player.Out();
    }
}
