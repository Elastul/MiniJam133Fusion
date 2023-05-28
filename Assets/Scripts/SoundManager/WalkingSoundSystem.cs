using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSoundSystem : MonoBehaviour
{
     public AudioClip[] stepSounds;
    public float stepInterval = 0.5f;
    private AudioSource audioSource;

    private int currentStepIndex = 0;
    private float nextStepTime = 0f;

    [SerializeField] private GroundChecker _groundChecker;
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if(!_groundChecker.IsGrounded())
            return;
            
        if (InputController.MovementAxis.magnitude > 0)
        {
            if (Time.time >= nextStepTime)
            {
                PlayNextStepSound();
                nextStepTime = Time.time + stepInterval;
            }
        }
    }

    private void PlayNextStepSound()
    {
        audioSource.PlayOneShot(stepSounds[currentStepIndex]);

        currentStepIndex++;

        if (currentStepIndex >= stepSounds.Length)
        {
            currentStepIndex = 0;
        }
    }
}
