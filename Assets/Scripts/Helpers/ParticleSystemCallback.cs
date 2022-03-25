using UnityEngine;

public class ParticleSystemCallback : MonoBehaviour
{
    public void OnParticleSystemStopped()
    {
        gameObject.Recycle();
    }
}