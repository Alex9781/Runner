using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    private void Start()
    {
        GameManager gameManager = FindObjectOfType<GameManager>();
        transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().materials[0] = gameManager.ItemMaterial;
    }
}
