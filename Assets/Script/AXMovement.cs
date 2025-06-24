using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AXMovement : MonoBehaviour
{
    private ControlCharacter character;
    public int damage;
    private void Start()
    {
        character = FindAnyObjectByType<ControlCharacter>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Shop")
        {
            character.conectShop = true;
        }
        if (collision.gameObject.CompareTag("Rock"))
        {
            character.isDig = true;     
        }
    }
    public void DigRock()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Rock"))
            {
                Rock rock = collider.GetComponent<Rock>();
                rock.debrisParticle = collider.GetComponent<ParticleSystem>();
                if (rock != null)
                {
                    rock.debrisParticle.Play();
                    rock.TakeDamage(damage);
                }
            }
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Shop")
        {
            character.conectShop = false;
        }
    }
}
