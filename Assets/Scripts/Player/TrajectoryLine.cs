using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private Vector3[] linePoints = new Vector3[2];

    private void Start()
    {
        // Initialize
        lineRenderer = FindObjectOfType<LineRenderer>();
        lineRenderer.positionCount = linePoints.Length;

        linePoints[0] = transform.position;
    }

    // Shows the trajectory line
    public void ShowTrajectoryLine(Vector2 startPoint, Vector2 direction) {
        // Update number of points for line
        lineRenderer.positionCount = linePoints.Length;

        // Get player position
        linePoints[0] = transform.position;

        // Get mouse position
        Camera c = Camera.main;
        Vector3 p = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        linePoints[1] = p;

        // Update LineRenderer
        lineRenderer.SetPositions(linePoints);
    }

    public void ClearLine() {
        lineRenderer.positionCount = 0;
    }
}
