using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    float distance = 2.5f;
    private bool wasGrounded =true;

    private float Height;
    private float old_Height;
    private float Height_Difference;

    FMOD.Studio.EventInstance Footsteps;
    FMOD.Studio.EventInstance Jump;
    FMOD.Studio.EventInstance Land;
    FMOD.Studio.EventInstance AttackMelee;
    FMOD.Studio.EventInstance AttackImpact;

    void Start()
    {
        Footsteps = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Huitzil/Footsteps");
        Jump = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Huitzil/Jump");
        Land = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Huitzil/Land");
        AttackMelee = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Huitzil/Attack_Melee");
        AttackImpact = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Huitzil/Weapon_Impact");
    }

    void FixedUpdate()
    {
        //Debug.DrawRay(transform.position, Vector2.down * distance, Color.blue);

        PlayerLanded();
        wasGrounded = IsGrounded();
        IsGrounded();
        //Debug.Log(IsGrounded());

        PlayerFallingCheck();
    }

    void PlayerFootsteps()
    {
        Footsteps.start();
    }

    void PlayerJump()
    {
        if (IsGrounded())
            Jump.start();
    }

    void PlayerAttackMelee()
    {
        AttackMelee.start();
    }

    void PlayerAttackImpact()
    {
        if (IsGrounded())
            AttackImpact.start();
    }

    void PlayerLanded()
        {
            if (IsGrounded() && !wasGrounded)
            {
                FMOD.Studio.EventInstance Land = FMODUnity.RuntimeManager.CreateInstance("event:/SFX/Huitzil/Land");
                Land.start();
                Land.release();
                //Debug.Log(Height_Difference);
            }
        }

    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, distance, 1 << 6);
    }

    void PlayerFallingCheck()
    {
        old_Height = Height;

        Height = transform.position.y;

        Height_Difference = Height - old_Height;

        if (Height_Difference > 0)
            Height_Difference = Height_Difference * -1;
    }

    void OnDestroy()
    {
        Footsteps.release();
        Jump.release();
        AttackMelee.release();
        AttackImpact.release();
    }

}
