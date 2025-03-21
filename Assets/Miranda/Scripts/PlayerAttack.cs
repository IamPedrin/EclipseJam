using UnityEngine;
using UnityEngine.InputSystem.Interactions;
using UnityEngine.Rendering;

public class PlayerAttack : MonoBehaviour
{
    private GameObject attackAreaVertical = default;
    private GameObject attackArea = default;

    Animator animator;
    private bool attacking = false;


    private float timeToAttack = 0.25f;
    private float timer = 0f;

    void Start()
    {
        attackArea = transform.GetChild(0).gameObject;
        attackAreaVertical = transform.GetChild(1).gameObject;
        animator = GetComponent<Animator>();
    }

    void Update()
    {

        if (Input.GetMouseButton(0)) Attack();
        if (Input.GetKeyDown(KeyCode.W)) AttackVertical();


        if (attacking)
        {

            timer += Time.deltaTime;

            if (timer >= timeToAttack)
            {
                timer = 0;
                attacking = false;
                AudioManager.Instance.PlaySFX("Sword");
                attackArea.SetActive(attacking);
                attackAreaVertical.SetActive(attacking);

            }
        }
        else
        {
            animator.SetBool("isVerticalAttacking", false);
            animator.SetBool("isHorizontalAttacking", false);
        }

    }

    private void Attack()
    {
        animator.SetBool("isHorizontalAttacking", true);
        attacking = true;
        attackArea.SetActive(attacking);
    }

    private void AttackVertical()
    {
        animator.SetBool("isVerticalAttacking", true);
        attacking = true;
        attackAreaVertical.SetActive(attacking);
    }
}
