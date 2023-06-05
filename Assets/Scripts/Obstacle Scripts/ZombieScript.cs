using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public GameObject bloodFXPrefab;
    public float speed = 1f;

    private Rigidbody myBody;
    private bool isAlive = true;

    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody>();

    }

    void Update()
    {
        if (isAlive) {
            myBody.velocity = new Vector3(0f, 0f, -speed);
        }
        // prilikom prebacivanja ploce poda od nazad ka napred (radi efekta beskonacnog nivoa),
        // zombiji koji nisu ubijeni propadaju dole po y osi, ne vide se u igri, ali trose memoriju
        // zato proverimo da li su pali po y osi i ako da - deaktiviramo ih
        if (transform.position.y < -10f) {
            gameObject.SetActive(false);
        }
    }

    void Die() {
        isAlive = false;
        // prestace hodati
        myBody.velocity = Vector3.zero;
        // prestace detektovati kolizije
        GetComponent<Collider>().enabled = false;
        // pusti idle animaciju
        GetComponentInChildren<Animator>().Play("Idle"); 
        // rotiraju se kao da padaju
        transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        transform.localScale = new Vector3(1f, 1f, 0.5f);
        // postavlja se malo iznad zemlje
        transform.position = new Vector3(transform.position.x, 0.2f, transform.position.z);
    }

// poziva se ako ga pogodi tenk ili metak
    void DeactivateGameObject() {
        gameObject.SetActive(false);
    }

// kada zombi udari tenk ili pogodi metak
    void OnCollisionEnter(Collision target) {
        // ako se zombi dotakao za tenkom ili metkom
        if (target.gameObject.tag == "Player" || target.gameObject.tag == "Bullet") {
            // instanciraj krv na poziciji gde je zombi sa rotacion 0,0,0
            Instantiate(bloodFXPrefab, transform.position, Quaternion.identity);
            Invoke("DeactivateGameObject", 3f);

            // Povecaj zombie score tenku
            GameplayController.instance.IncreaseScore();

            Die(); 
        }
    }
}
