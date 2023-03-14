using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private float SpeedInitial;
    [SerializeField] private GameObject TextScoreDecrease;
    [SerializeField] private Animator Animator;
    [SerializeField] private float LimitLeft;
    [SerializeField] private float LimitRight;
    [SerializeField] private List<Material> Materials;

    private bool Alive = true;
    private float MousePositionX;
    private float PositionX;
    private bool IsHolding = false;
    private readonly int MovementDivider = 80;
    private float Speed;

    public int Amoguses { get; set; }
    public Text AmogusesText;
    public Text AmogusesTextCanvasFinish;

    void Start()
    {
        Animator.SetBool("Running", true);
        transform.GetChild(0).GetComponent<SkinnedMeshRenderer>().material = Materials.Find(m => m.name == PlayerPrefs.GetString("AmogusSelected"));
    }

    void FixedUpdate()
    {
        if (!Alive)
        {
            return;
        }

        transform.Translate(Vector3.forward * Speed);

        if (Input.GetMouseButtonDown(0))
        {
            MousePositionX = Input.mousePosition.x;
            PositionX = transform.position.x;
            IsHolding = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            IsHolding = false;
        }
        if (IsHolding == true)
        {
            transform.position = new Vector3(PositionX + (Input.mousePosition.x - MousePositionX) / MovementDivider, 
                transform.position.y, 
                transform.position.z);
        }

        transform.position = new Vector3(Mathf.Clamp(transform.position.x, LimitLeft, LimitRight), 
            transform.position.y, 
            transform.position.z);

        float speedAdditional = Amoguses / 1000;

        Speed = SpeedInitial + speedAdditional;
    }

    public void Die()
    {
        int amogusesInitial = Amoguses;
        int difference = Amoguses;
        Amoguses /= 2;
        difference -= Amoguses;
        UpdateAmoguses();
        if (amogusesInitial > 0)
        {
            TextScoreDecrease.SetActive(true);
            TextScoreDecrease.GetComponent<Text>().text = "-" + difference.ToString();
            TextScoreDecrease.GetComponent<Animator>().Play(0);
        }
    }

    public void DieBottom()
    {
        Animator.SetBool("Running", false);
        Alive = false;
        GetComponent<Rigidbody>().isKinematic = true;
    }

    public void Finish()
    {
        Animator.applyRootMotion = false;
        Alive = false;
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<BoxCollider>().enabled = false;
        transform.localPosition = new Vector3(0, 0, -3);
        Animator.SetBool("Running", false);
        Animator.SetBool("Finish", true);
        PlayerPrefs.SetInt("Amoguses", PlayerPrefs.GetInt("Amoguses") + Amoguses);
    }

    public void Vent()
    {
        Animator.applyRootMotion = false;
        Animator.SetBool("Venting", true);
        Animator.SetBool("Running", false);
        Alive = false;
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<BoxCollider>().enabled = false;
        transform.position = new Vector3(0, 0, -3);
        StartCoroutine(nameof(UnVent));
    }

    IEnumerator UnVent()
    {
        yield return new WaitForSeconds(1);
        Animator.applyRootMotion = true;
        Animator.SetBool("Venting", false);
        Animator.SetBool("Running", true);
        Alive = true;
        GetComponent<Rigidbody>().useGravity = true;
        GetComponent<BoxCollider>().enabled = true;
    }

    public void UpdateAmoguses()
    {
        AmogusesText.text = Amoguses.ToString();
        AmogusesTextCanvasFinish.text = Amoguses.ToString();
    }

    private void OnTriggerEnter(Collider collider)
    {
        CollectableItem item;
        if (collider.gameObject.TryGetComponent(out item))
        {
            Destroy(collider.gameObject);
            Amoguses++;
            UpdateAmoguses();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out CollectableItem collectableItem))
        {
            Amoguses += 1;
            UpdateAmoguses();
            Destroy(collectableItem);
        }
        else if (collision.gameObject.TryGetComponent(out Impostor impostor))
        {
            Die();
        }
    }
}
