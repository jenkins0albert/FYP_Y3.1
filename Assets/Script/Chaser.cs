using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using Cinemachine;

public class Chaser : MonoBehaviour
{
    private NavMeshAgent agentComponent;
    private Animator animator;
    private ChaserAudioManager audioManager;

    [SerializeField]
    private Transform player;

    [SerializeField]
    private float followDistance = 5.0f;  // distance within which the AI starts to follow

    [SerializeField]
    private float angleThreshold = 30.0f;  // angle threshold to determine if AI is facing the player

    [SerializeField]
    private Volume globalVolume;
    private Vignette vignette;
    [SerializeField]
    private float vignetteIntensity = 0.5f;

    [SerializeField]
    private CinemachineVirtualCamera cinemachineCamera;
    private CinemachineBasicMultiChannelPerlin noise;
    private float targetNoiseAmplitude = 0.0f;
    private float noiseLerpSpeed = 2.0f;

    [SerializeField]
    private float defaultFOV;
    private float targetFOV;
    private float fovLerpSpeed = 2.0f;
    [SerializeField]
    private float chaseFOV = 100.0f; // field of view when being chased

    private void Awake()
    {
        agentComponent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        audioManager = GetComponent<ChaserAudioManager>();

        if (globalVolume.profile.TryGet<Vignette>(out Vignette vignetteComponent))
        {
            vignette = vignetteComponent;
        }
        else
        {
            vignette = globalVolume.profile.Add<Vignette>(true);
        }
        vignette.active = false;
        vignette.intensity.value = vignetteIntensity;

        defaultFOV = cinemachineCamera.m_Lens.FieldOfView;

        // get the cinemachine noise settings
        noise = cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
    }

    private void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);

            if (distanceToPlayer <= followDistance)
            {
                // checks if the AI is facing the player
                Vector3 directionToPlayer = (player.position - transform.position).normalized;
                float angle = Vector3.Angle(transform.forward, directionToPlayer);

                if (angle < angleThreshold)
                {
                    agentComponent.SetDestination(player.position);

                    // set vignette active
                    vignette.active = true;

                    // smooth transition for FOV
                    targetFOV = chaseFOV;
                    cinemachineCamera.m_Lens.FieldOfView = Mathf.Lerp(cinemachineCamera.m_Lens.FieldOfView, targetFOV, Time.deltaTime * fovLerpSpeed);

                    // smooth transition for camera noise amplitude
                    targetNoiseAmplitude = 1.0f;
                    noise.m_AmplitudeGain = Mathf.Lerp(noise.m_AmplitudeGain, targetNoiseAmplitude, Time.deltaTime * noiseLerpSpeed);

                    // Update animator parameters
                    animator.SetBool("isWalking", true);
                    animator.SetFloat("Speed", agentComponent.velocity.magnitude);

                    // Play chasing sound
                    audioManager.PlayChasingSound();
                }
                else
                {
                    agentComponent.ResetPath();  // stop moving if not facing the player

                    // set vignette inactive
                    vignette.active = false;

                    targetFOV = defaultFOV;
                    cinemachineCamera.m_Lens.FieldOfView = Mathf.Lerp(cinemachineCamera.m_Lens.FieldOfView, targetFOV, Time.deltaTime * fovLerpSpeed);

                    targetNoiseAmplitude = 0.0f;
                    noise.m_AmplitudeGain = Mathf.Lerp(noise.m_AmplitudeGain, targetNoiseAmplitude, Time.deltaTime * noiseLerpSpeed);

                    // Update animator parameters
                    animator.SetBool("isWalking", false);
                    animator.SetFloat("Speed", 0f);

                    // plays idle sound
                    audioManager.PlayIdleSound();
                }
            }
            else
            {
                agentComponent.ResetPath();  // stops moving if player is out of range

                vignette.active = false;

                targetFOV = defaultFOV;
                cinemachineCamera.m_Lens.FieldOfView = Mathf.Lerp(cinemachineCamera.m_Lens.FieldOfView, targetFOV, Time.deltaTime * fovLerpSpeed);

                targetNoiseAmplitude = 0.0f;
                noise.m_AmplitudeGain = Mathf.Lerp(noise.m_AmplitudeGain, targetNoiseAmplitude, Time.deltaTime * noiseLerpSpeed);

                // update animator parameters
                animator.SetBool("isWalking", false);
                animator.SetFloat("Speed", 0f);

                // plays idle sound
                audioManager.PlayIdleSound();
            }
        }
        else
        {
            vignette.active = false;

            targetFOV = defaultFOV;
            cinemachineCamera.m_Lens.FieldOfView = Mathf.Lerp(cinemachineCamera.m_Lens.FieldOfView, targetFOV, Time.deltaTime * fovLerpSpeed);

            targetNoiseAmplitude = 0.0f;
            noise.m_AmplitudeGain = Mathf.Lerp(noise.m_AmplitudeGain, targetNoiseAmplitude, Time.deltaTime * noiseLerpSpeed);

            // update animator parameters
            animator.SetBool("isWalking", false);
            animator.SetFloat("Speed", 0f);

            // plays idle sound
            audioManager.PlayIdleSound();
        }
    }
}
