using UnityEngine;

public class Bottom : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.DieBottom();
        }
    }
}
