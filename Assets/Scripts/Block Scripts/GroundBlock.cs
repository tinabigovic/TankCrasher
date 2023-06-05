using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundBlock : MonoBehaviour
{
  
    public Transform otherBlock;
    // pola duzine od jednog bloka
    public float halfLength = 100f;
    private Transform player;
    private float endOffset = 10f;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        MoveGround();
    }

    void MoveGround() {
        // ako je tenk ispred poda:
        // uzimam poziciju tenka na z osi i ako je ta vrednost - endOffset(jos malo-da prodje prvi blok)
        // veca od pozicije na z koju zauzima prvi blok
        if (player.transform.position.z - endOffset > transform.position.z + halfLength) {
            // repozicioniraj pod, tj stavi tekuci ispred sledeceg bloka
            transform.position = new Vector3(otherBlock.position.x, otherBlock.position.y, 
                                            otherBlock.position.z + halfLength * 2);

        }
    }

}
