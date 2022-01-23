using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 dir;
    private Animator anim;
    [SerializeField] private int speed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity;
    [SerializeField] private GameObject losePanel;
    private int lineToMove = 1;
    public float lineDistance = 4;

    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        losePanel.SetActive(false);
        Time.timeScale = 1;
    }

    private void Update()
    {
        if(swipeController.swipeRight)
        {
            if (lineToMove < 2)
                lineToMove++;
        }
        if (swipeController.swipeLeft)
        {
            if (lineToMove > 0)
                lineToMove--;
        }

        if (swipeController.swipeUp)
        {
            if (controller.isGrounded)
                Jump();
        }

        if (controller.isGrounded)
            anim.SetBool("run", true);
        else
            anim.SetBool("run", false);

        Vector3 targetPosistion = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove == 0)
            targetPosistion += Vector3.left * lineDistance;
        else if(lineToMove == 2)
            targetPosistion += Vector3.right * lineDistance;
        if (transform.position == targetPosistion)
            return;
        Vector3 diff = targetPosistion - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);
    }
    private void Jump()
    {
        dir.y = jumpForce;
        anim.SetTrigger("jump");
    }

    void FixedUpdate()
    {
        dir.z = speed;
        dir.y += gravity * Time.fixedDeltaTime;
        controller.Move(dir * Time.fixedDeltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.tag == "obstacle")
        {
            losePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
