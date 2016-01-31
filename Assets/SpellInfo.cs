using UnityEngine;
using System.Collections;
using InControl;

public class SpellInfo : MonoBehaviour
{

    public CharacterController characterController;
    Vector2 castDirection;
    public float spellSpeed;
    public float timeToLive;
    public InputDevice device;
    public LayerMask layerMask;
    // Use this for initialization

    void Start()
    {
        //getting the character's direction for the direction of the spell
        //characterController = GameObject.FindGameObjectWithTag("Character").GetComponent<CharacterController>();
        castDirection = characterController.targetDirection;
        //Debug.Log(castDirection);
        transform.position = characterController.transform.position + ((Vector3)(castDirection));
        Invoke("NotoriousRIP", timeToLive);
    }

    void FixedUpdate()
    {
        //adding a velocity to the spell
        transform.position = transform.position + ((Vector3)castDirection * spellSpeed) * Time.fixedDeltaTime;
    }

    //shit that happens based on the layer mask that it touches
    void OnCollisionEnter2D()
    {
        if (GetComponent<Collider2D>().IsTouchingLayers(layerMask))
        {

        }

    }

    void NotoriousRIP()
    {
        Destroy(gameObject);
    }
}
