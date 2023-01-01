using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Baby")
        {
            AudioManager.Instance.PlaySound(SoundType.BabyJump);

        }
    }
}
