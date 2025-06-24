using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Abyss : MonoBehaviour
{
    public ControlCharacter character;
    private void Awake()
    {
        character = FindAnyObjectByType<ControlCharacter>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Abyss")
        {
            character.isLose = true;
            character.animator.SetBool("Abyss", true);
        }
    }
}
