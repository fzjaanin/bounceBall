using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControls : MonoBehaviour
{
    public GameObject circle;
    [SerializeField] private float speed;
    [SerializeField] private float mouseSpeed;
    [SerializeField] private float distance = 5f;
    private Vector2 startPosition, endPositon;
    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnMouseDrag()
    {
        if (!levelManager.finished & levelManager.started) {
            float rot = Input.GetAxis("Mouse X") * mouseSpeed * Mathf.Deg2Rad;
            circle.transform.Rotate(Vector3.up * -rot);
        }
        if (!levelManager.started)
        {
            levelManager.started = true;
            levelManager.DestroyFinger();
        }

    }


    void Update()
    {
        if (!levelManager.finished & levelManager.started)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Began)
                {
                    startPosition = Input.GetTouch(0).position;
                }

                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {
                    endPositon = Input.GetTouch(0).position;
                    if (Mathf.Abs(startPosition.x - endPositon.x) > distance)
                    {
                        float direction = Mathf.Sign(startPosition.x - endPositon.x);

                        circle.transform.Rotate(Vector3.up * -speed * direction * Time.unscaledDeltaTime);
                        startPosition = Input.GetTouch(0).position;
                    }

                }

                if (!levelManager.started)
                {
                    levelManager.started = true;
                    levelManager.DestroyFinger();
                }
            }
        }
      

    }
}
