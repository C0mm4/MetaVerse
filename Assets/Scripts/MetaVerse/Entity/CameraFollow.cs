using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;           
    public SpriteRenderer background;     
    public float smoothSpeed = 0.125f;  

    private Vector3 minBounds;
    private Vector3 maxBounds;
    private float halfHeight;
    private float halfWidth;

    void Start()
    {
        if (background == null)
        {
            Debug.LogError("Background SpriteRenderer�� �������� �ʾҽ��ϴ�.");
            return;
        }

        // ī�޶� ũ�� ���ϱ�
        Camera cam = Camera.main;
        halfHeight = cam.orthographicSize;
        halfWidth = halfHeight * cam.aspect;

        // ����� ���� ��� ���
        Bounds bounds = background.bounds;
        minBounds = bounds.min;
        maxBounds = bounds.max;
    }

    void LateUpdate()
    {
        if (target == null) return;

        Vector3 desiredPosition = target.position;

        // ��� �������� �̵��ϵ��� ����
        float clampedX = Mathf.Clamp(desiredPosition.x, minBounds.x + halfWidth, maxBounds.x - halfWidth);
        float clampedY = Mathf.Clamp(desiredPosition.y, minBounds.y + halfHeight, maxBounds.y - halfHeight);

        Vector3 smoothPosition = Vector3.Lerp(transform.position, new Vector3(clampedX, clampedY, transform.position.z), smoothSpeed);
        transform.position = smoothPosition;
    }
}