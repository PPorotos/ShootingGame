using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtroll : MonoBehaviour
{

    public float speed = 10f;
    public float h = 0.0f;
    public float v = 0.0f;

    private Animator weaponAnimator;
    public CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        weaponAnimator = GameObject.Find("WeaponRifle").GetComponent<Animator>();
        weaponAnimator.enabled = false;
    }
    private void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");

        if(h == 1 || h == -1 || v == 1 || v == -1)
        {
            weaponAnimator.enabled = true;
        }
        else
        {
            weaponAnimator.enabled = false;
        }

        Vector3 cameraVectorForward = Camera.main.transform.TransformDirection(Vector3.forward);
        Vector3 cameraVectorRight = Camera.main.transform.TransformDirection(Vector3.right);
        Vector3 move = (cameraVectorForward * v) + (cameraVectorRight * h);
        characterController.SimpleMove(move * speed);
        
    }
}
