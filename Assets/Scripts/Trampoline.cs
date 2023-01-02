using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trampoline : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        AudioManager.Instance.PlaySound(SoundType.BabyJump);
    }
}
