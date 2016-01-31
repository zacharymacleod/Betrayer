using UnityEngine;
using System.Collections;
using InControl;

public class CharacterController : MonoBehaviour
{
    public InputDevice device;

    public bool ready;
    public bool conrtolEnabled;
    private Animator animator;
    public Rigidbody2D rigidbody2D;
    public Vector2 movement;
    public Vector2 spellCooldown;
    public Vector2 targetDirection;
    public float moveSpeed;
    int playerNumber;


    public GameObject spell;
    public float castMoveSpeed;
    bool spellOffCooldown;
    bool castingSpell;
    public float currentCooldownTime;
    public float spellCooldownPeriod;
    public float castTime;
    private float totalCastingTime;
    public bool isBetrayer;

    public enum voteChoice { red, green, blue, yellow };

    // Use this for initialization
    void Start()
    {
        moveSpeed = 8;
        castMoveSpeed = 2;
        castTime = 0.6f;
        animator = (Animator)this.GetComponent(typeof(Animator));
    }

    //animations for the movement of the character when you arent using a spell
    void ManageMovement(float horizontal, float vertical)
    {

        if (horizontal != 0f || vertical != 0f)
        {
            animator.SetBool("moving", true); animateWalk(horizontal, vertical);
        }
        else
        {
            animator.SetBool("moving", false);
        }

        movement = new Vector2(horizontal, vertical);
        rigidbody2D.velocity = movement;
    }
    //animations for when you are using a spell -------- NEED TO ADD ANIMATIONS
    void CastingMovement(float horizontal, float vertical)
    {

        if (horizontal != 0f || vertical != 0f)
        {
            animator.SetBool("moving", true); animateWalk(horizontal, vertical);
        }
        else
        {
            animator.SetBool("moving", false);
        }

        movement = new Vector2(horizontal, vertical);
        rigidbody2D.velocity = movement;
    }
    void FixedUpdate()
    {
        if (ready && conrtolEnabled)
        {
            if (spellOffCooldown)
            {
                if (device.Action1.IsPressed)
                {
                    //adding a cast time for casting the spells
                    castingSpell = true;

                    castTime -= Time.fixedDeltaTime;
                    if (castTime <= 0)
                    {
                        CastSpell();
                        castingSpell = false;
                        castTime = 0.6f;
                    }
                }
                else if (device.Action1.WasReleased)
                {
                    castingSpell = false;
                    castTime = 0.6f;
                }
            }

            //if you are casting a spell you are slower
            if (!castingSpell)
            {
                float v = device.LeftStickY * moveSpeed;
                float h = device.LeftStickX * moveSpeed;

                ManageMovement(h, v);
            }
            if (castingSpell)
            {
                float v = device.LeftStickY * castMoveSpeed;
                float h = device.LeftStickX * castMoveSpeed;
                //print("dicks");
                CastingMovement(h, v);
            }

            //setting the direction of the spell to be a proper direction, even while not moving
            if ((movement.magnitude) > 4)
            {
                targetDirection = movement;
                targetDirection.Normalize();
            }

            //spell cooldowns
            if (currentCooldownTime == spellCooldownPeriod)
            {
                currentCooldownTime = currentCooldownTime - Time.fixedDeltaTime;
            }
            else if (currentCooldownTime >= 0)
            {
                currentCooldownTime = currentCooldownTime - Time.fixedDeltaTime;
            }
            else if (!spellOffCooldown)
            {
                spellOffCooldown = true;
            }
        }
    }
    void animateWalk(float h, float v)
    {

        //animation directions
        if (animator)
        {
            if ((v > 0) && (v > h))
            {
                animator.SetInteger("Direction", 1);
            }
            if ((h > 0) && (v < h))
            {
                animator.SetInteger("Direction", 2);
            }
            if ((v < 0) && (v < h))
            {
                animator.SetInteger("Direction", 3);
            }
            if ((h < 0) && (v > h))
            {
                animator.SetInteger("Direction", 4);
            }
        }
    }
    void CastSpell()
    {
        GameObject spellThing = Instantiate(spell, this.transform.position, this.transform.rotation) as GameObject;
        spellThing.GetComponent<SpellInfo>().characterController = this;
        currentCooldownTime = spellCooldownPeriod;

        //having a cast animation for the cast time duration, once animation is complete you are back to normal running speed
        /*if (this.animator.GetCurrentAnimatorStateInfo(0).IsName("CastSpellAnimation"))
        {
            castingSpell = false;
        }
        */

        spellOffCooldown = false;
    }
}