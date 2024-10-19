using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Character_Movement
{
    private float moveX, moveY;

    private Camera mainCam;

    private Vector2 mousePosition;
    private Vector2 direction;
    private Vector3 tempScale;

    private Animator anim;

    private PlayerWeaponManager playerWeaponManager;

    private bool canMove = true;

    protected override void Awake()
    {
        base.Awake();
        mainCam = Camera.main;
        anim = GetComponent<Animator>();

        playerWeaponManager = GetComponent<PlayerWeaponManager>();
    }


    private void FixedUpdate()
    {
        if (canMove)
        {
            moveX = Input.GetAxisRaw(TagManager.HORIZONTAL_AXIS);
            moveY = Input.GetAxisRaw(TagManager.VERTICAL_AXIS);

            HandlePlayerTurning();

            HandleMovement(moveX, moveY);
        }    
        

        

    }

    void HandlePlayerTurning()
    {
        mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);

        direction = new Vector2(mousePosition.x - transform.position.x,
            mousePosition.y - transform.position.y).normalized;

        //Vector �������� 2 ��������� ���������� - ����� � ������������ � ��������.
        //�������� - ��� ����� �����, ������������ ����� (0, 0, 0) �
        //������ � ������������.���� �� "normalized" ������, ����������� ����� �����, �������
        //���������� � (0, 0, 0) � "points" �� ���� �������� ����� � ������������.
        //���� �� �� ����� ����� ����� "points", ��� ���� �� ����� 1 ������� �����

        HandlePlayerAnimation(direction.x, direction.y);
    }

    void HandlePlayerAnimation(float x, float y)
    {
        x = Mathf.RoundToInt(x);
        y = Mathf.RoundToInt(y);

        tempScale = new Vector3(1f, 1f, 1f);
        tempScale = transform.localScale;

        if(x>0)
            tempScale.x = Mathf.Abs(tempScale.x);
        else if (x<0)
            tempScale.x = -Mathf.Abs(tempScale.x);

        transform.localScale = tempScale;

        x = Mathf.Abs(x);

        anim.SetFloat(TagManager.FACE_X_ANIMATION_PARAMETER, x);
        anim.SetFloat(TagManager.FACE_Y_ANIMATION_PARAMETER, y);

        ActivateWeaponForSide(x, y);
    }

    void ActivateWeaponForSide(float x, float y)
    {
        //side
        if (x == 1f && y == 0f)
            playerWeaponManager.ActivateGun(0);

        //up
        if (x == 0f && y == 1f)
            playerWeaponManager.ActivateGun(1);

        //down
        if (x == 0f && y == -1f)
            playerWeaponManager.ActivateGun(2);

        //side up
        if (x == 1f && y == 1f)
            playerWeaponManager.ActivateGun(3);

        //side down
        if (x == 1f && y == -1f)
            playerWeaponManager.ActivateGun(4);
    }

    // ����� ��� ��������� �������� ������
    public void DisableMovement()
    {
        canMove = false;
    }

    // ����� ��� �������������� �������� ������
    public void EnableMovement()
    {
        canMove = true;
    }
}
