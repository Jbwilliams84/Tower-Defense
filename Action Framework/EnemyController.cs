﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

[
    RequireComponent(typeof(Animator)),
    RequireComponent(typeof(Damageable)),
    RequireComponent(typeof(AutoMovement)),
    RequireComponent(typeof(Targetable)),
    RequireComponent(typeof(Damaging))
]
public class EnemyController : MonoBehaviour
{

    [SerializeField] private string healthAnimatorParam = "Health";
    [SerializeField] private AutoTargeting autoTargetChild;
    [SerializeField] private AudioClip sfxDying;


    
    private Animator animator;
    private Damageable damageableBehaviour;
    private AutoMovement autoMovementBehaviour;
    private Targetable targetableBehaviour;
    private Damaging damagingBehaviour;
    private AudioSource audioSource;

    protected bool dying;

    private void Awake()
    {
        Assert.IsNotNull(autoTargetChild);
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        damageableBehaviour = GetComponent<Damageable>();
        autoMovementBehaviour = GetComponent<AutoMovement>();
        targetableBehaviour = GetComponent<Targetable>();
        damagingBehaviour = GetComponent<Damaging>();
        audioSource = GetComponent<AudioSource>();

        damageableBehaviour.DamageTaken += HandleDamageTaken;
        autoTargetChild.TargetAcquired += OnTargetAcquired;
        autoTargetChild.TargetLost += OnTargetLost;
    }

    private void HandleDamageTaken()
    {
        var health = damageableBehaviour.CurrentHealth;
        animator.SetInteger(healthAnimatorParam, health);
        if (health <= 0)
        {
            autoMovementBehaviour.enabled = false;
            targetableBehaviour.Disable();
            audioSource.PlayOneShot(sfxDying);
            animator.SetTrigger("Dying");

        }
    }

    private void OnTargetAcquired(object sender, EventArgTemplate<GameObject> target)
    {
        damagingBehaviour.StartTargeting(target.Attachment);
        damagingBehaviour.StartAttacking(true);
    }

    private void OnTargetLost()
    {
        damagingBehaviour.StopTargeting();
        damagingBehaviour.StopAttacking();
    }
}
