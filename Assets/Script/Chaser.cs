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
    private PlayerInteraction player;

    [SerializeField]
    private float followDistance = 5.0f;  // distance within which the AI starts to follow

    [SerializeField]
    private float angleThreshold = 30.0f;  // angle threshold to determine if AI is facing the player

    [SerializeField]
    private Volume globalVolume;
    private Vignette vignette;
    [SerializeField]
    private float vignetteIntensity = 0.3f;

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

    
    [SerializeField]
    private float changeDirectionInterval = 2.0f; // Time before changing direction while patrolling

    private Vector3 patrolDirection;
    private float patrolTimer;
    private Rigidbody rb; // The animals's rigidbody

    [SerializeField]
    private bool hasStopped = false;
    private void Start()
    {
        player = FindObjectOfType<PlayerInteraction>();
        cinemachineCamera = FindObjectOfType<CinemachineVirtualCamera>();
        rb = GetComponent<Rigidbody>();
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
        //vignette.active = false;
        vignette.intensity.value = vignetteIntensity;
        
        defaultFOV = cinemachineCamera.m_Lens.FieldOfView;

        // get the cinemachine noise settings
        noise = cinemachineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

        
    }

    private void Update()
    {
        vignette.intensity.value = vignetteIntensity;

        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= followDistance)
            {
                // checks if the AI is facing the player
                Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
                float angle = Vector3.Angle(transform.forward, directionToPlayer);

                if (angle < angleThreshold)
                {
                    agentComponent.SetDestination(player.transform.position);

                    // set vignette active
                    //vignette.active = true;
                    vignetteIntensity = 0.5f;

                    // smooth transition for FOV
                    targetFOV = chaseFOV;
                    cinemachineCamera.m_Lens.FieldOfView = Mathf.Lerp(cinemachineCamera.m_Lens.FieldOfView, targetFOV, Time.deltaTime * fovLerpSpeed);

                    // smooth transition for camera noise amplitude
                    targetNoiseAmplitude = 1.0f;
                    noise.m_AmplitudeGain = Mathf.Lerp(noise.m_AmplitudeGain, targetNoiseAmplitude, Time.deltaTime * noiseLerpSpeed);

                    // Update animator parameters
                    animator.SetBool("isWalking", true);
                    animator.SetFloat("Speed", agentComponent.velocity.magnitude);
                    animator.speed = 1f;

                    // Play chasing sound
                    audioManager.PlayChasingSound();
                }
                else
                {
                    //agentComponent.ResetPath();  // n  if not facing the player

                    // set vignette inactive
                    //vignette.active = false;
                    vignetteIntensity = 0.3f;
                    targetFOV = defaultFOV;
                    cinemachineCamera.m_Lens.FieldOfView = Mathf.Lerp(cinemachineCamera.m_Lens.FieldOfView, targetFOV, Time.deltaTime * fovLerpSpeed);

                    targetNoiseAmplitude = 0.0f;
                    noise.m_AmplitudeGain = Mathf.Lerp(noise.m_AmplitudeGain, targetNoiseAmplitude, Time.deltaTime * noiseLerpSpeed);

                    StartCoroutine(Patrol());
                    /*
                    // Update animator parameters
                    
                    animator.SetBool("isWalking", false);
                    animator.SetFloat("Speed", 0f);

                    // plays idle sound
                    audioManager.PlayIdleSound();
                    */
                }
            }
            else
            {
                //agentComponent.ResetPath();  // stops moving if player is out of range

                //vignette.active = false;
                vignetteIntensity = 0.3f;

                targetFOV = defaultFOV;
                cinemachineCamera.m_Lens.FieldOfView = Mathf.Lerp(cinemachineCamera.m_Lens.FieldOfView, targetFOV, Time.deltaTime * fovLerpSpeed);

                targetNoiseAmplitude = 0.0f;
                noise.m_AmplitudeGain = Mathf.Lerp(noise.m_AmplitudeGain, targetNoiseAmplitude, Time.deltaTime * noiseLerpSpeed);


                StartCoroutine(Patrol());
                /*
                // update animator parameters
                animator.SetBool("isWalking", false);
                animator.SetFloat("Speed", 0f);

                // plays idle sound
                audioManager.PlayIdleSound();
                */




            }
        }
        else
        {
            //vignette.active = false;


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
        Debug.Log(animator.GetFloat("Speed"));
        Debug.Log(animator.GetBool("isWalking"));

    }
    private IEnumerator Patrol()
    {

        
        Vector3 direction;


        if (hasStopped == false)
        {
            yield return StartCoroutine(CurrentlyPatrolling());
        }

            // plays idle sound
            audioManager.PlayIdleSound();
        
        


        // Move in the current patrol direction
        direction = patrolDirection;


        if (hasStopped == true)
        {
            yield return StartCoroutine(PatrolThenStop());
        }
        
;        /*
        // Change patrol direction at intervals
        patrolTimer += Time.deltaTime;
        if (patrolTimer > changeDirectionInterval)
        {
            animator.SetBool("isWalking", false);
            animator.SetFloat("Speed", 0f);

            SetNewPatrolDirection();
            patrolTimer = 0;
        }
        */
        


        // Rotate towards the movement direction
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1f * Time.deltaTime);

        transform.position += direction * animator.GetFloat("Speed") * Time.deltaTime;
        
    }
    private IEnumerator CurrentlyPatrolling()
    {
        yield return null;

        SetNewPatrolDirection();

        // update animator parameters
        animator.SetBool("isWalking", true);
        animator.SetFloat("Speed", 3f);
        animator.speed = 0.5f;
        yield return new WaitForSeconds(7.0f);

        hasStopped = true;
    }

    private IEnumerator PatrolThenStop()
    {


        yield return null;
        
        Debug.Log("now moving");
        patrolTimer += Time.deltaTime;
        
        if (patrolTimer > changeDirectionInterval)
        {
            
            animator.SetBool("isWalking", false);
            animator.SetFloat("Speed", 0f);

            yield return new WaitForSeconds(7.0f);
            
            patrolTimer = 0;
            hasStopped = false;
            

        }

        yield return  null;





    }
    private void SetNewPatrolDirection()
    {
        // Randomly set a new direction for patrolling
        Vector3 randomDirection = Random.insideUnitSphere;
        //randomDirection.y = 0; // Keep movement on the XZ plane
        patrolDirection.x = randomDirection.x;
        patrolDirection.z = randomDirection.z;
        
    }

}
