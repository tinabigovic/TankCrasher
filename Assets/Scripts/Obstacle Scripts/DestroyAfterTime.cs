using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAfterTime : MonoBehaviour
{

    public float timer = 3f;

    void Start() {
        // postavljamo vreme koliko trake deaktivacija objekta i na taj nacin ogranicavamo
        // trajanje eksplozije
        Invoke("DeactivateGameObject", timer);
    }


    void DeactivateGameObject() {
        gameObject.SetActive(false);
    }
}
