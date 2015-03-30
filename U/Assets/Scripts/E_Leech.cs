using UnityEngine;
using System.Collections;

public class E_Leech : MonoBehaviour {


    public static float movementSpeed;
    public float rotationSpeed;
    public static float leechDamage;
    public float leechDamageDelay;

    private Movement _playerMovement;
    private Vector2 _playerPosition;
    private Vector2 _rotationDirection;

    public float nextDamageTime;
    public float damageTickDelay;

	void Awake ()
    {
        nextDamageTime = Time.time;
        _playerMovement = Movement.instance;
    }


    void FixedUpdate ()
    {
        _playerPosition = _playerMovement.transform.position;
        Vector2 temp = transform.position;
        _rotationDirection = temp - _playerPosition;
        _rotationDirection.Normalize();
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(Vector3.forward, _rotationDirection), Time.deltaTime * rotationSpeed);

        transform.Translate(-Vector2.up * movementSpeed * Time.deltaTime, Space.Self);

    }

    public void SetNewDamageTick()
    {
        nextDamageTime = Time.time + damageTickDelay;
    }
}
