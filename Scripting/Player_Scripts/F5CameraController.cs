using System.Collections;
using UnityEngine;

public class F5CameraController : MonoBehaviour
{
    private Camera mainCamera;
    private Vector3 originalPosition;
    private Quaternion originalRotation;
    private bool isMoving = false;

    void Start()
    {
        mainCamera = Camera.main;
        originalPosition = mainCamera.transform.position;
        originalRotation = mainCamera.transform.rotation;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F5) && !isMoving)
        {
            StartCoroutine(MoveCamera());
        }
    }

    IEnumerator MoveCamera()
    {
        isMoving = true;

        // Calculate new positions and rotations
        Vector3 backPosition = mainCamera.transform.position - mainCamera.transform.forward * 3f;
        Quaternion rotatedRotation = mainCamera.transform.rotation * Quaternion.Euler(0, 180, 0);
        Vector3 forwardPosition = mainCamera.transform.position + mainCamera.transform.forward * 3f;

        // Move back
        yield return MoveToPosition(backPosition);

        // Rotate 180 degrees
        yield return RotateToRotation(rotatedRotation);

        // Move forward
        yield return MoveToPosition(forwardPosition);

        isMoving = false;
    }

    IEnumerator MoveToPosition(Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        float duration = 1f; // Adjust duration as needed

        while (elapsedTime < duration)
        {
            mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, targetPosition, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }

    IEnumerator RotateToRotation(Quaternion targetRotation)
    {
        float elapsedTime = 0f;
        float duration = 1f; // Adjust duration as needed

        while (elapsedTime < duration)
        {
            mainCamera.transform.rotation = Quaternion.Slerp(mainCamera.transform.rotation, targetRotation, elapsedTime / duration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
    }
}