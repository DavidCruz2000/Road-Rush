using UnityEngine;

public class DistanceTraveled : MonoBehaviour
{
 private Vector3 _prevPosition;
    private Vector3 _currPosition;
    private float _distanceTraveled;

    // Start is called before the first frame update
    void Start()
    {
        _prevPosition = transform.position;
        _currPosition = transform.position;
        _distanceTraveled = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _currPosition = transform.position;
        float distanceTraveledInFrame = Vector3.Distance(_currPosition, _prevPosition);
        _distanceTraveled += distanceTraveledInFrame;
        _prevPosition = _currPosition;

        // Broadcast distance traveled to other scripts that need this information
        // For organizational purposes, have "PLAYER_DISTANCE_TRAVELED_UPDATED" be part of GameEvent
        // For use with a fuel gauge, have the fuel-decrement method subscribe to this event.
        Messenger<float>.Broadcast("PLAYER_DISTANCE_TRAVELED", distanceTraveledInFrame);
    }

}
