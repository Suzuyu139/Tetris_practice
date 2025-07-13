using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] bool canRotate = true;

    void Move(Vector3 moveDirection)
    {
        transform.position += moveDirection;
    }

    public void MoveLeft()
    {
        Move(Vector3.left);
    }
    public void MoveRight()
    {
        Move(Vector3.right);
    }
    public void MoveUp()
    {
        Move(Vector3.up);
    }
    public void MoveDown()
    {
        Move(Vector3.down);
    }

    public void RotateRight()
    {
        if (!canRotate)
        {
            return;
        }

        transform.Rotate(0.0f, 0.0f, -90.0f);
    }
    public void RotateLeft()
    {
        if (!canRotate)
        {
            return;
        }

        transform.Rotate(0.0f, 0.0f, 90.0f);
    }
}
