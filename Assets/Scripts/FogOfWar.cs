using UnityEngine;

[RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class FogOfWar : MonoBehaviour
{
    [SerializeField] LayerMask blockingLayerMask;
    [SerializeField] int rayCount = 2;

    [SerializeField] float fov = 90.0f;
    [SerializeField] float angle = 0.0f;
    [SerializeField] float viewDistance = 50.0f;

    private Mesh mesh;

    float angleIncrement = 0.0f;

    void Start()
    {
        mesh = new Mesh();

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;
        angleIncrement = fov / rayCount;
    }

    void LateUpdate()
    {
        Vector3 origin = transform.position;

        Vector3 targetDirection = GetTargetDelta();
        angle = Rotate(targetDirection);

        Vector3[] vertices = new Vector3[rayCount + 2];
        Vector2[] uv = new Vector2[vertices.Length];
        int[] triangles = new int[rayCount * 3];

        vertices[0] = origin;

        int triIndex = 0;
        int vertIndex = 1;
        for (int i = 0; i <= rayCount; i++, vertIndex++)
        {
            float rads = angle * (Mathf.PI / 180.0f);
            Vector3 direction = new Vector3(Mathf.Cos(rads), Mathf.Sin(rads));

            Vector3 reach = direction * viewDistance;
            Vector3 vertex = origin + reach;

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, viewDistance, blockingLayerMask);
            if (hit.collider != null)
            {
                vertex = hit.point;
            }

            vertices[vertIndex] = vertex;

            if (i > 0)
            {
                triangles[triIndex] = 0;
                triangles[triIndex + 1] = vertIndex - 1;
                triangles[triIndex + 2] = vertIndex;

                triIndex += 3;
            }

            angle -= angleIncrement;
        }

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;
    }

    Vector3 GetTargetDelta()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
    }

    float Rotate(Vector2 rotationDelta)
    {
        float directionalAngle = Mathf.Atan2(rotationDelta.y, rotationDelta.x) * Mathf.Rad2Deg + 90.0f;
        return directionalAngle - (fov / 2.0f);
    }
}
