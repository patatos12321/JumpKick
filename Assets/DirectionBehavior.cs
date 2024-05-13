using UnityEngine;

public enum Direction { Left, Right}
public class DirectionBehavior : MonoBehaviour
{
    private static bool LeftRight = false;
    public float MaxX;
    public float MinX;
    public int NumberOfPositions;

    private float movementSize;

    public int NbSkippedUpdates;

    private int nbCurrentlySkipped = 0;
    private Direction CurrentDirection;
    private Vector3 RigidBodyPosition => gameObject.transform.position;

    // Start is called before the first frame update
    void Start()
    {
        movementSize = (MaxX - MinX) / NumberOfPositions;

        SetDirection();
    }

    private void SetDirection()
    {
        LeftRight = !LeftRight;
        CurrentDirection = LeftRight ? Direction.Left : Direction.Right;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (nbCurrentlySkipped >= NbSkippedUpdates)
        {
            nbCurrentlySkipped = 0;
            if (CurrentDirection == Direction.Right)
            {

                gameObject.transform.position = new Vector3(RigidBodyPosition.x + movementSize, RigidBodyPosition.y, RigidBodyPosition.z);
                if (RigidBodyPosition.x >= MaxX) { CurrentDirection = Direction.Left; }
            }
            else //left
            {
                gameObject.transform.position = new Vector3(RigidBodyPosition.x - movementSize, RigidBodyPosition.y, RigidBodyPosition.z);
                if (RigidBodyPosition.x <= MinX) { CurrentDirection = Direction.Right; }
            }
        }
        else
        {
            nbCurrentlySkipped++;
        }
    }

    public float GetPosition()
    {
        if (RigidBodyPosition.x > 0)
        {
            return RigidBodyPosition.x / MaxX;
        }
        else
        {
            return -(RigidBodyPosition.x / MinX);
        }
    }
}
