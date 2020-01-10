using UnityEngine;

public class CharacterScript : MonoBehaviour
{
    public float speed = 6.0f;
    public float jumpSpeed = 0f;
    public float gravity = 1.0f;

    Animator animation;
    private Vector3 moveDirection = Vector3.zero;
    private CharacterController controller;

    int stoneCount = 5;

    int score = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        // let the gameObject fall down
        gameObject.transform.position = new Vector3(0, 0.01f, 20);
        animation = GetComponent<Animator>();
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                animation.SetInteger("condition", 1);
            else if (Input.GetKeyDown(KeyCode.Z))
            {
                animation.SetInteger("condition", 1);
                CreateHitObject(HitObjectScript.HitObjectType.Coal);
            }
            else if (Input.GetKeyDown(KeyCode.X) && stoneCount != 0)
            {
                animation.SetInteger("condition", 1);
                CreateHitObject(HitObjectScript.HitObjectType.Stone);

                stoneCount--;
            }
            else 
                animation.SetInteger("condition", 0);
        }

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical"));
        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection = moveDirection * speed;

        // Apply gravity
        moveDirection.y = moveDirection.y - (gravity * Time.deltaTime);

        //// Move the controller
        controller.Move(moveDirection * Time.deltaTime);
    }

    void CreateHitObject(HitObjectScript.HitObjectType hitObjectType)
    {
        var hitObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);

        hitObject.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.5f);
        hitObject.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);

        hitObject.AddComponent<Rigidbody>();
        hitObject.AddComponent<HitObjectScript>();

        hitObject.GetComponent<HitObjectScript>().SetHitObjectType(hitObjectType);
    }

    public void Score()
    {
        score++;
        if (score == 1)
        {
            
        }
    }
}

