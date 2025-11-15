using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ParticleSelector : MonoBehaviour
{
    [SerializeField]
    GameObject[] Particles;
    [SerializeField]
    TMP_Text particleNames;
    int currentParticleIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Particles[currentParticleIndex].SetActive(true);
        UpdateName();

    }

    // Update is called once per frame
    void Update()
    {

    }
    void UpdateName()
    { 

        particleNames.text = Particles[currentParticleIndex].name;
    }

    public void Next()
    {
        Particles[currentParticleIndex].SetActive(false);
        currentParticleIndex++;
        if (currentParticleIndex >= Particles.Length)
            currentParticleIndex = 0;
        Particles[currentParticleIndex].SetActive(true);
        UpdateName();
    }

    public void Previous()
    {
        Particles[currentParticleIndex].SetActive(false);
        currentParticleIndex--;
        if (currentParticleIndex < 0)
            currentParticleIndex = Particles.Length - 1;
        Particles[currentParticleIndex].SetActive(true);
        UpdateName();
    }
}
