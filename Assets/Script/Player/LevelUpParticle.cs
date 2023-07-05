using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpParticle : MonoBehaviour
{
  
    public ParticleSystem[] levelupParticles;

    public void LevelUpEffect()
    {
        for(int i=0;i<levelupParticles.Length;i++)
        {
            levelupParticles[i].Play();
        }
    }
}
