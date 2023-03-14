using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private Animator Animator;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player player))
        {
            player.transform.parent = transform;
            player.Finish();
            Animator.SetBool("Opening", true);
            FindObjectOfType<GameManager>().Finish();
        }
    }
}
