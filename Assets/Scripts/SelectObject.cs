using UnityEngine;

public class SelectObject : MonoBehaviour
{
    [SerializeField] float searchTime = 5.0f;
    float currentSearchTime = 0.0f;

    void Update()
    {
        MouseInput();
    }

    void MouseInput()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 raycastPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(raycastPos, Vector2.zero);

            if (hit.collider == null) { return; }

            Searchable searchable = hit.collider.gameObject.GetComponent<Searchable>();

            if (searchable == null || searchable.isSearched == false) { return; }

            currentSearchTime += Time.deltaTime;

            if (currentSearchTime >= searchTime) { searchable.Search(); }
        }
        else
        {
            currentSearchTime = 0.0f;
        }
    }
}