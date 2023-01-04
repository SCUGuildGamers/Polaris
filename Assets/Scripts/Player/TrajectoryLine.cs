using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private int lineSegments = 60;

    [SerializeField]
    private float timeOfTheFlight = 5;

    // Start is called before the first frame update
    public void ShowTrajectoryLine(Vector2 startPoint, Vector2 startVelocity) {
        Vector3[] lineRendererPoints = CalculateTrajectoryLine(startPoint, startVelocity);

        lineRenderer.positionCount = lineSegments;
        lineRenderer.SetPositions(lineRendererPoints);
    }

    private Vector3[] CalculateTrajectoryLine(Vector2 startPoint, Vector2 direction) {
        RaycastHit2D hit = Physics2D.Raycast(startPoint, direction);
        if (hit.collider != null) {
            Debug.Log(hit.distance);
        }

        Vector3[] lineRendererPoints = new Vector3[lineSegments];

        lineRendererPoints[0] = startPoint;
        for (int i = 1; i < lineSegments; i++) {
            Vector3 newPosition = new Vector3(1,1,1);
            lineRendererPoints[i] = newPosition;
        }

        return lineRendererPoints;
    }
}
