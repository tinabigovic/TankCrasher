using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : MonoBehaviour
{

//  pokazatelj trenutne brzine
    public Vector3 speed;

//  x_Speed je brzina kretanja desno-levo, a z_speed napred
    public float  x_Speed = 8f, z_Speed = 15f;

// ubrzavanje i usporavanje
    public float accelerated = 15, deccelerated = 10f;

//  brzina rotacije
    protected float rotationSpeed = 10f;

//  maximalni ugao za rotaciju
    protected float maxAngle = 10f;

    public float low_Sound_Pitch, normal_Sound_Pitch, high_Sound_Pitch;

    public AudioClip engine_On_Sound, engine_Off_sound;

    private bool is_Slow;

    private AudioSource soundManager;

    // Start is called before the first frame update
    void Awake()
    {
        // setuj zvuk iz sourcea
        soundManager = GetComponent<AudioSource>();
        // odmah na pocetku, tenk se krece pravo
        speed = new Vector3(0f, 0f, z_Speed);   
    }

    protected void MoveLeft() {
        // kretanje u levo -> negativan x-speed podeljen na dva jer ne zelimo da ide prebrzo
        // prosledjujemo i speed.z da bi se nastavio kretati pravo
        speed = new Vector3(-x_Speed / 2f, 0f, speed.z);
    }

    protected void MoveRight() {
        // analogno 
        speed = new Vector3(x_Speed / 2f, 0f, speed.z);
    }

    protected void MoveStraight() {
        speed = new Vector3(0f, 0f, speed.z);
    }

    protected void MoveNormal() {
        if (is_Slow) {
            is_Slow = false;

            soundManager.Stop();
            soundManager.clip = engine_On_Sound;
            soundManager.volume = 0.3f;
            soundManager.Play();
        }
        speed = new Vector3(speed.x, 0f, z_Speed);
    }

//  usporavanje tenka
      protected void MoveSlow() {
        // ako vec ne ide sporo
        if (!is_Slow) {
            is_Slow = true;

            soundManager.Stop();
            // pusti zvuk gasenja motora
            soundManager.clip = engine_Off_sound;
            soundManager.volume = 0.5f;
            soundManager.Play();
        }
        // uspori brzinu po z tj, napred-nazad uspori
        speed = new Vector3(speed.x, 0f, deccelerated);
    }

    //  ubrzavanje tenka
      protected void MoveFast() {
        // povecaj brzinu po z tj, napred-nazad uspori
        speed = new Vector3(speed.x, 0f, accelerated);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
