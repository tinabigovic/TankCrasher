using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvents : MonoBehaviour
{
    private PlayerController playerController;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        anim = GetComponent<Animator>();
    }

// ovaj metod ce se poznati na 58.frame-u animacije za punjenje energije
    void ResetShooting()
    {
        playerController.canShoot = true;
        anim.Play("Idle");
    }
}
