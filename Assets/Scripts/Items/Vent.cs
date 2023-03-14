using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vent : MonoBehaviour
{
    public Animator Animator;
    [SerializeField] GameManager GameManager;
    [SerializeField] GameObject VentPair;
    Player Player;
    Transform PlayerParent;

    void Start()
    {
        GameManager = GameObject.FindGameObjectWithTag(nameof(GameManager)).GetComponent<GameManager>();
    }

    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Player) && VentPair)
        {
            Animator.SetBool("Venting", true);
            GameManager.Vent(VentPair.transform.GetChild(2).gameObject);
            VentPair.GetComponent<Vent>().Animator.SetBool("Venting", true);
            Player.Vent();
            PlayerParent = Player.transform.parent;
            Player.transform.parent = VentPair.transform;
            Player.transform.position = new Vector3(VentPair.transform.position.x, Player.transform.position.y, VentPair.transform.position.z);
            StartCoroutine(nameof(UnVent));
        }
    }

    IEnumerator UnVent()
    {
        yield return new WaitForSeconds(1);
        VentPair.GetComponent<Vent>().Animator.SetBool("Venting", false);
        Player.transform.parent = PlayerParent;
        Player.transform.rotation = Quaternion.identity;
    }
}
