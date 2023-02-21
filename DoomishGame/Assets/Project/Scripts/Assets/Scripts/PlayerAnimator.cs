using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private PlayerMovement mov;
    private Animator anim;
    private SpriteRenderer spriteRend;
    public DeflectAbility deflectAbility;
    private Player player;

    private DemoManager demoManager;
    [Header("References")]
    [SerializeField] PlayerPogJump playerPogJump;
    [SerializeField] GameObject SwordGroundSlam;
    [SerializeField] GameObject dashCollider;
    [SerializeField] GameObject slashParticle;
    [SerializeField] Transform whereToSpawnSlash;

    [Header("Ground Slam")]
    [SerializeField] GameObject SwordMelee;
    [SerializeField] GameObject playerBullet;

    [Header("Movement Tilt")]
    [SerializeField] private float maxTilt;
    [SerializeField][Range(0, 1)] private float tiltSpeed;

    [Header("Particle FX")]
    [SerializeField] private GameObject jumpFX;
    [SerializeField] private GameObject landFX;
    private ParticleSystem _jumpParticle;
    private ParticleSystem _landParticle;
    private bool shouldGroundSlam;
    private float timeFromLastDash;

    public bool startedJumping { private get; set; }
    public bool justLanded { private get; set; }
    public bool isWallSliding { private get; set; }

    public float currentVelY;

    private void Start()
    {
        mov = GetComponentInParent<PlayerMovement>();
        spriteRend = GetComponent<SpriteRenderer>();
        anim = spriteRend.GetComponent<Animator>();
        player = GetComponent<Player>();
        demoManager = FindObjectOfType<DemoManager>();
        deflectAbility = GetComponentInParent<DeflectAbility>();

        _jumpParticle = jumpFX.GetComponent<ParticleSystem>();
        _landParticle = landFX.GetComponent<ParticleSystem>();
    }

    private void LateUpdate()
    {
        timeFromLastDash += Time.deltaTime;
        #region Tilt
        float tiltProgress;

        int mult = -1;

        if (mov.IsSliding)
        {
            tiltProgress = 0.25f;
        }
        else
        {
            tiltProgress = Mathf.InverseLerp(-mov.Data.runMaxSpeed, mov.Data.runMaxSpeed, mov.RB.velocity.x);
            mult = (mov.IsFacingRight) ? 1 : -1;
        }

        float newRot = ((tiltProgress * maxTilt * 2) - maxTilt);
        float rot = Mathf.LerpAngle(spriteRend.transform.localRotation.eulerAngles.z * mult, newRot, tiltSpeed);
        spriteRend.transform.localRotation = Quaternion.Euler(0, 0, rot * mult);
        #endregion

        CheckAnimationState();

        //    ParticleSystem.MainModule jumpPSettings = _jumpParticle.main;
        //    jumpPSettings.startColor = new ParticleSystem.MinMaxGradient(demoManager.SceneData.foregroundColor);
        //    ParticleSystem.MainModule landPSettings = _landParticle.main;
        //    landPSettings.startColor = new ParticleSystem.MinMaxGradient(demoManager.SceneData.foregroundColor);
        //
    }

    private void CheckAnimationState()
    {
        if (startedJumping)
        {
            //anim.SetBool("isGrounded", false);
            GameObject obj = Instantiate(jumpFX, transform.position - (Vector3.up * transform.localScale.y / 2), Quaternion.Euler(-90, 0, 0));
            Destroy(obj, 1);
            startedJumping = false;
            return;
        }

        if (justLanded)
        {
            //anim.SetBool("isGrounded", true);
            GameObject obj = Instantiate(landFX, transform.position - (Vector3.up * transform.localScale.y / 1.5f), Quaternion.Euler(-90, 0, 0));
            Destroy(obj, 1);
            justLanded = false;

            if (shouldGroundSlam)
            {
                anim.SetTrigger("isGroundSlamming");
                shouldGroundSlam = false;
            }

            return;
        }

        else
        {
            anim.SetBool("isWallSliding", false);
        }

        anim.SetBool("isWallSliding", mov.IsSliding);
        anim.SetFloat("velocityX", mov.RB.velocity.x);
        anim.SetFloat("velocityY", mov.RB.velocity.y);

        if (mov.RB.velocity.y < -35)
        {
            shouldGroundSlam = true;
        }
        
        else if(mov.RB.velocity.y > 0)
        {
            shouldGroundSlam = false;
        }

        if (timeFromLastDash > 0.22f && dashCollider.activeSelf)
        {
            StopDeflectingLogic();
        }

        anim.SetBool("isGrounded", mov.isGrounded);
    }

    #region ROLLING
    public void StartRollingAnimation()
    {
        anim.SetTrigger("isRolling");
    }



    #endregion

    #region DASH

    public void StartDashAnimation()
    {
        shouldGroundSlam = false;
        anim.SetTrigger("isDashing");
/*        GameObject objectSpawned = Instantiate(slashParticle, whereToSpawnSlash.position, whereToSpawnSlash.rotation, whereToSpawnSlash);
        Destroy(objectSpawned, 0.5f);*/
    }

    private void StartDeflectingLogic()
    {
        timeFromLastDash = 0;
        dashCollider.SetActive(true);

    }

    private void StopDeflectingLogic()
    {
        dashCollider.SetActive(false);
    }
    #endregion

    #region POG JUMP
    public void StartPogJumpAnimation()
    {
        anim.SetTrigger("pogJump");
    }

    private void StartPogJump()
    {
        playerPogJump.TurnOnCollision();
    }

    private void EndPogTime()
    {
        playerPogJump.TurnOffCollision();
    }
    #endregion 

    #region GROUND SLAM
    public void StartedGroundSlammingAnimation()
    {
        Debug.Log("sthsthsth");

        mov.canPlayerMove = false;
        mov.isGroundSlamming = true;
        anim.SetTrigger("isGroundSlamming");
    }

    private void StopGroundSlamingAnimation()
    {
        mov.canPlayerMove = true;
        mov.isGroundSlamming = false;
    }

    private void AttackOnGroundSlam()
    {
        Debug.Log("sthsthsth");
        GameObject playerSpawnedBullet = Instantiate(playerBullet, SwordMelee.transform.position, Quaternion.identity);
    }
    #endregion
}
