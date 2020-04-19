using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class FieldOfView : MonoBehaviour
{
    [SerializeField] LayerMask blockingLayerMask;
    [SerializeField] int rayCount = 10;
    [SerializeField] float fov = 90.0f;
    [SerializeField] float viewDistance = 50.0f;

    Mesh mesh;
    Vector3 origin;
    float startingAngle;

    private void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
    }

    private void Update()
    {
        float angle = startingAngle;
        float angleIncrement = fov / rayCount;

        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int vertexIndex = 1;
        int triangleIndex = 0;
        for (int i = 0; i <= rayCount; i++)
        {
            Vector3 vertex;

            RaycastHit2D hit = Physics2D.Raycast(origin, GetVector3FromAngle(angle), viewDistance, blockingLayerMask);
            if (hit.collider == null)
            {
                vertex = origin + GetVector3FromAngle(angle) * viewDistance;
            }
            else
            {
                vertex = hit.point;
            }

            vertices[vertexIndex] = vertex;

            if (i > 0)
            {
                triangles[triangleIndex] = 0;
                triangles[triangleIndex + 1] = vertexIndex - 1;
                triangles[triangleIndex + 2] = vertexIndex;

                triangleIndex += 3;
            }

            vertexIndex++;
            angle -= angleIncrement;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    static Vector3 GetVector3FromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI / 180.0f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    static float GetAngleFromVector3(Vector3 aimDirection)
    {
        aimDirection = aimDirection.normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;

        if (angle < 0) { angle += 360; }

        return angle;
    }

    public void SetOrigin(Vector3 origin)
    {
        this.origin = origin;
    }

    public void SetAimDirection(Vector3 aimDirection)
    {
        this.startingAngle = GetAngleFromVector3(aimDirection) + (fov / 2.0f);
    }
}
