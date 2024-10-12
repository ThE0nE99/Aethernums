using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particle : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public ParticleSystem particleSystem2;
    public ParticleSystem particleSystem3;
    public ParticleSystem particleSystem4;
    public ParticleSystem particleSystem5;
    public ParticleSystem particleSystem6;
    void Update()
    {
        // Detectar si se presiona la tecla P
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Verificar si el sistema de partículas está asignado
            if (particleSystem != null)
            {
                // Activar el sistema de partículas
                particleSystem.Play();
                particleSystem2.Play(); 
            }
            else
            {
                Debug.LogWarning("No se ha asignado un sistema de partículas.");
            }
        }
    }
}
