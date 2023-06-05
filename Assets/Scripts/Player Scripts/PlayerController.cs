using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : BaseController
{

    private Rigidbody myBody;

    public Transform bullet_StartPoint;
    public GameObject bullet_Prefab;
    public ParticleSystem shootFX;

    private Animator shootSliderAnim;

    [HideInInspector]
    public bool canShoot;

    void Start()
    {
        myBody = GetComponent<Rigidbody>();

        shootSliderAnim = GameObject.Find("Fire Bar").GetComponent<Animator>();

        GameObject.Find("ShootBtn").GetComponent<Button>().onClick.AddListener(ShootingControl);
        canShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        ControlMovementWithKeyboard();
        ChangeRotation();
    }

    void FixedUpdate() {
        MoveTank();
    }

    void MoveTank() {
        // pomeram komponentu -> uzimam trenutnu poziciju, dodajem brzinu iz 
        // nadklase koja je inicijalizacijom postavljena na kretanje pravo, 
        myBody.MovePosition(myBody.position + speed * Time.deltaTime);
    }

    void ControlMovementWithKeyboard() {
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) {
            // idemo na levo
            MoveLeft();
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) {
            // idemo na desno
            MoveRight();
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) {
            // idemo brze
            MoveFast();
        }
         if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) {
            // idemo sporije
            MoveSlow();
        }
        // ako se otpusti dugme za levo:
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) {
            // idi pravo
            MoveStraight();
        }
        // ako se otpusti dugme za desno:
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) {
            // idi pravo
            MoveStraight();
        }
        // ako se otpusti dugme za ubrzavanje:
        if (Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W)) {
            // idi normalnom brzinom
            MoveNormal();
        }
        // ako se otpusti dugme za usporavanje:
        if (Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S)) {
            // idi normalnom brzinom
            MoveNormal();
        }

    }

    // rotiranje tenka pri promeni pravca kretanja
    void ChangeRotation() {
        if (speed.x > 0) {
            // slerp -> sfericno ce se pomeriti od trenutne rotacione pozicije
            // do prosledjene (rotiramo samo po y osi) za vreme
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.Euler(0f, maxAngle, 0f), Time.deltaTime * rotationSpeed);
        } else if (speed.x < 0) {
            // otisli smo u levo    
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.Euler(0f, -maxAngle, 0f), Time.deltaTime * rotationSpeed);
        } else {
            transform.rotation = Quaternion.Slerp(transform.rotation,
                Quaternion.Euler(0f, 0f, 0f), Time.deltaTime * rotationSpeed);
        }
    }

    public void ShootingControl() {
        // proveravam da li je igrica pauzirana
        if (Time.timeScale != 0) {
            if (canShoot) {
                GameObject bullet = Instantiate(bullet_Prefab, bullet_StartPoint.position, Quaternion.identity);
                bullet.GetComponent<BulletScript>().Move(2000f);
                shootFX.Play();

                canShoot = false;
                // poziv animacije za fade in slidera za fire
                shootSliderAnim.Play("Fill");
            }
        }
    }

}
