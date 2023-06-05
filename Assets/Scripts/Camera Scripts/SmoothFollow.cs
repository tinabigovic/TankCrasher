using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmoothFollow : MonoBehaviour
{
    // player - tank
    public Transform target;
    // koliko ce kamera biti daleko od tanka
    public float distance = 6.3f;
    // koliko ce kamera biti iznad tanka:
    public float height = 3.5f;

    public float height_Damping = 3.25f;
    public float rotation_Damping = 0.27f;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update() {

    }

//  update koji se poziva nakon sto se update zavrsi
    void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer() {

        float wanted_Rotation_Angle = target.eulerAngles.y;
        // uzimamo zisinu tanka i dodajemo jos malo iznad njega
        float wanted_Height = target.position.y + height;

        float current_Rotation_Angle = transform.eulerAngles.y;
        float current_Height = transform.position.y;

        // pomeri kameru od pocetne pozicije do zeljene pozicije za odredjeno vreme
        current_Rotation_Angle = Mathf.LerpAngle (current_Rotation_Angle, wanted_Rotation_Angle, rotation_Damping * Time.deltaTime);

        current_Height = Mathf.Lerp(current_Height, wanted_Height, height_Damping * Time.deltaTime);

        Quaternion current_Rotation = Quaternion.Euler (0f, current_Rotation_Angle, 0f);

        transform.position = target.position;
        // postavljamo distancu kamere od tanka
        transform.position -= current_Rotation * Vector3.forward * distance;

        transform.position = new Vector3 (transform.position.x, current_Height, transform.position.z);

    }
}
