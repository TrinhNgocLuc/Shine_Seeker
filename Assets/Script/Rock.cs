using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    public int healRock;
    public GameObject diamond;
    public ParticleSystem debrisParticle;
    public void TakeDamage(int damage)
    {
        healRock -= damage;
        if (healRock <= 0)
        {
            Destroy(gameObject);
            diamond.SetActive(true);
        }
    }
}
