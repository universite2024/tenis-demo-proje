using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class player_01 : MonoBehaviour
{
    public Transform aimTarget;

    float speed = 3f;

    bool hitting;

    public Transform ball;
    Animator animator;

    Vector3 aimTargetInitialPosition;

    ShotManager shotManager;
    Shot currentShot;

    [SerializeField] Transform serveRight;
    [SerializeField] Transform serveLeft;

    bool servedRight = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        aimTargetInitialPosition = aimTarget.position;
        shotManager = GetComponent<ShotManager>();
        currentShot = shotManager.topSpin;
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.F))
        {
            hitting = true;
            currentShot = shotManager.topSpin;
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            hitting = false;
        }

        // ------------- //

        if (Input.GetKeyDown(KeyCode.E))
        {
            hitting = true;
            currentShot = shotManager.flat;
        }
        else if (Input.GetKeyUp(KeyCode.E))
        {
            hitting = false;
        }

        // ------------- //

        if (Input.GetKeyDown(KeyCode.R))
        {
            hitting = true;
            currentShot = shotManager.flatServe;
            GetComponent<BoxCollider>().enabled = false;
            animator.Play("serve-prepare");
        }

        // ------------- //

        if (Input.GetKeyDown(KeyCode.T))
        {
            hitting = true;
            currentShot = shotManager.kickServe;
            GetComponent<BoxCollider>().enabled = false;
            animator.Play("serve-prepare");
        }

        // ------------- //

        if (Input.GetKeyUp(KeyCode.R) || Input.GetKeyUp(KeyCode.T))
        {
            hitting = false;
            GetComponent<BoxCollider>().enabled = true;
            ball.transform.position = transform.position + new Vector3(0.2f, 1, 0);

            Vector3 dir = aimTarget.position - transform.position;
            ball.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.hitForce + new Vector3(0, 0, currentShot.upForce);
            animator.Play("serve");

            ball.GetComponent<top_01>().hitter = "player_obje_01";
            ball.GetComponent<top_01>().playing = true;

        }

        if (hitting)
        {
            aimTarget.Translate(new Vector3(0, 0, h) * speed * Time.deltaTime);
        }

        if ((h != 0 || v != 0) && !hitting)
        {
            transform.Translate(new Vector3(-v, 0, h) * speed * Time.deltaTime);
        }

        Vector3 ballDir = ball.position - transform.position;

        if (ballDir.x >= 0)
        {

            Debug.Log("vurus_animation");
        }
        else
        {

            Debug.Log("vurus_animation_b");
        }
        Debug.DrawRay(transform.position, ballDir);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("top"))
        {
            Vector3 dir = aimTarget.position - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * currentShot.hitForce + new Vector3(0, currentShot.upForce, 0);

            Vector3 ballDir = ball.position - transform.position;

            if (ballDir.x >= 0)
            {

                animator.Play("vurus_animation");
                Debug.Log("vurus_animation");
            }
            else
            {

                animator.Play("vurus_animation_b");
                Debug.Log("vurus_animation_b");
            }

            ball.GetComponent<top_01>().hitter = "player_obje_01";
            aimTarget.position = aimTargetInitialPosition;
        }
    }

    public void Reset()
    {
        if(servedRight)
            transform.position = serveLeft.position;
        else
            transform.position = serveRight.position;

        servedRight = !servedRight;
    }

}
