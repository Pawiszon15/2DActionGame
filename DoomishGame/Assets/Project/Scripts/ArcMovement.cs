using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArcMovement : MonoBehaviour
{
    [SerializeField] AnimationCurve curve;
    [SerializeField] float duration;
    [SerializeField] float heightY;
    [SerializeField] Transform endPosTransform;

    private bool curveJumpAvaialable;
    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            rb.gravityScale = 0f;
            StartCoroutine(CurveMovement(transform.position, endPosTransform.position));
        }
    }

    public IEnumerator CurveMovement(Vector3 start, Vector2 target)
    {
        float timePassed = 0f;
        Vector2 end = target;
        Debug.Log("curve jump");

        while(timePassed < duration)
        {
            timePassed += Time.deltaTime;

            float linearT = timePassed / duration;
            float heightT = curve.Evaluate(linearT);

            float height = Mathf.Lerp(0f, heightY, heightT);
            transform.position = Vector2.Lerp(start, end, linearT) + new Vector2(0f, height);

            yield return null;
        }

    }
}
