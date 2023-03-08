using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryLine : MonoBehaviour
{
    [SerializeField]
    private LineRenderer lineRenderer;

    [SerializeField]
    private int lineSegments = 60;

    // Shows the trajectory line
    public void ShowTrajectoryLine(Vector2 startPoint, Vector2 direction) {
        Vector3[] lineRendererPoints = CalculateTrajectoryLine(startPoint, direction);

        lineRenderer.positionCount = lineSegments;
        
        // Ensure that the array is not null to prevent error
        if(lineRendererPoints != null)
            lineRenderer.SetPositions(lineRendererPoints);
    }

    // Cast a ray in a direction, get the hitpoint, and calculate the direction/increment needed to create the trajectory line
    private Vector3[] CalculateTrajectoryLine(Vector2 startPoint, Vector2 direction) {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction);
        if (hit) {
            Vector2 move_path = hit.point - startPoint;
            float x_increment = move_path.x / lineSegments;
            float y_increment = move_path.y / lineSegments;

            Vector3[] lineRendererPoints = new Vector3[lineSegments];

            Vector3 current_position = new Vector3(startPoint.x, startPoint.y, 0);
            lineRendererPoints[0] = current_position;
            for (int i = 1; i < lineSegments; i++)
            {
                current_position = current_position + new Vector3(x_increment, y_increment);
                lineRendererPoints[i] = current_position;
            }

            return lineRendererPoints;
        }

        return null;
    }

    public void ClearLine() {
        lineRenderer.positionCount = 0;
    }
}
