using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMovement : MonoBehaviour {

    public float tilt;
    public float smoothing;
    public float dodge;

    public Vector2 startWait;
    public Vector2 maneuverTime;
    public Vector2 maneuverWiat;
    public Boundary boundary;
    public bool isInPosition = false;

    private Vector3 startPoint;
    private Vector3 endPoint;
    private float startTime;
    private float wave;
    private float currentSpeed;
    private float targetManeuver;
    private Rigidbody rb;

    void Start() {
        rb = GetComponent<Rigidbody>();
        currentSpeed = rb.velocity.z;
        startPoint = transform.position;
        endPoint = new Vector3(0.0f, 0.0f, 12.0f);
        startTime = Time.time;
        if (Random.value < 0.5f) {
            StartCoroutine(Evade());
        } else {
            Debug.Log("waving");
            StartCoroutine(Wave());
        }
    }
  
    IEnumerator Evade() {
        yield return new WaitForSeconds(Random.Range(startWait.x, startWait.y));

        while (true) {
            targetManeuver = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            Debug.Log(targetManeuver);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            targetManeuver = 0;
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
        }
    }

    void Update() {
        if (Time.time < 6.0f) {
            transform.position = Vector3.Lerp(startPoint, endPoint, (Time.time - startTime) / 5.0f);
        } else {
            isInPosition = true;
            wave = Mathf.Sin(Time.time * 8) * dodge;
        }
    }

    IEnumerator Wave() {
        float startDirection = Mathf.Sign(transform.position.x);
        while (true) {
            targetManeuver = wave * startDirection;
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
        }
    }

    void FixedUpdate() {
        float newManeuver = Mathf.MoveTowards(rb.velocity.x, targetManeuver, Time.deltaTime * smoothing);
        rb.velocity = new Vector3(newManeuver, 0.0f, currentSpeed);
        rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
        );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, (rb.velocity.x * -tilt));
    }

    Vector3 DiagonalMove() {

        return new Vector3(-2.0f, 0.0f, currentSpeed);

    }
}
