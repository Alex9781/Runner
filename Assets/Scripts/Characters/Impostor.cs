using UnityEngine;

public class Impostor : MonoBehaviour
{
    [SerializeField] private GameObject Ground;
    [SerializeField] private bool IsMoving;
    [SerializeField] private float MovementStart = 0;
    [SerializeField] private float MovementEnd = 0;

    private bool Direction = true;
    private float Speed = 0.5f / 10;

    void Start()
    {
        Speed = Random.Range(0.5f / 10, 0.5f / 5);
        Direction = Random.Range(0, 2) == 1;
        transform.localRotation = Direction == true ? Quaternion.Euler(0, -90, 0) : Quaternion.Euler(0, 90, 0);
        if (MovementStart == 0 && MovementEnd == 0)
        {
            MovementStart = -((Ground.transform.localScale.x / 2) - 0.25f);
            MovementEnd = (Ground.transform.localScale.x / 2) - 0.25f;
        }
        GameManager gameManager = FindObjectOfType<GameManager>();
        transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = gameManager.ImpostorMaterial;
    }

    void FixedUpdate()
    {
        if (IsMoving)
        {
            if (transform.localPosition.x <= MovementStart)
            {
                Direction = false;
                transform.localRotation = Quaternion.Euler(0, -90, 0);
            }
            if (transform.localPosition.x >= MovementEnd)
            {
                Direction = true;
                transform.localRotation = Quaternion.Euler(0, 90, 0);
            }
            transform.Translate((Direction ? Vector3.back : Vector3.back) * Speed);
        }
    }
}
