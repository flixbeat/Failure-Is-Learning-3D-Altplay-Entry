using UnityEngine;

public class Goal : MonoBehaviour
{
    // collides with player
    private void OnCollisionEnter(Collision other)
    {
        Player player = other.gameObject.GetComponent<Player>();
        GameManager.changeLevel = true;
        player.Out();
    }
}
