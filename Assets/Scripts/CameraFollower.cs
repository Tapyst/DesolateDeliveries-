using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CameraFollow : MonoBehaviour
{
    [Header("Targeting")]
    private GameObject player;
    private Vector3 offset;
    private Vector3 currentVelocity = Vector3.zero;
    private bool isFollowing = false;

    [Header("Movement Settings")]
    [Tooltip("Lower = Faster. Higher = Smoother/Slower.")]
    [SerializeField] private float smoothTime = 0.2f;
    
    [Header("Deadzone Settings")]
    [Tooltip("The area around the center where the camera won't move.")]
    [SerializeField] private Vector2 deadzoneSize = new Vector2(2f, 2f);
    void OnEnable()
    {
    // Subscribe to the event 
    SceneManager.sceneLoaded += OnSceneLoaded;
    }
    private void OnDisable()
    {
    // ALWAYS unsubscribe to prevent MissingReferenceException (and leeks 🥬 [There is no leek emoji ); ])
    SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) {
            Debug.LogError("CameraFollow: No object with tag 'Player' found!");
            return;
        }
        //snap positin to player before creating ofset as that is only of the z direction this needs to change if for some reason you want aditional x/y ofset
        transform.position = new Vector3(player.transform.position.x,player.transform.position.y,gameObject.transform.position.z);
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        // Store the initial Z distance
        offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
        if (player == null) return;
        Vector3 targetPos = player.transform.position + offset;
        //Calculate the distance between current camera pos and target pos
        Vector3 distance = targetPos - transform.position;
        //only move if the player has moved further than the deadzone bounds
        Vector3 desiredPosition = transform.position;

        if (isFollowing || Mathf.Abs(distance.x) > deadzoneSize.x || Mathf.Abs(distance.y) > deadzoneSize.y)
        {
            isFollowing = true;
            if (Mathf.Abs(distance.x) < 0.1f && Mathf.Abs(distance.y) < 0.1f)
            {
                isFollowing = false;
            }
        }
    
        if (!isFollowing) return;
        //calculate how far outside the deadzone the player is
        float xShift = distance.x/* - (Mathf.Sign(distance.x) * deadzoneSize.x)*/;
        desiredPosition.x += xShift;
        float yShift = distance.y/* - (Mathf.Sign(distance.y) * deadzoneSize.y)*/;
        desiredPosition.y += yShift;
        //prevents jitter i hope
        transform.position = Vector3.SmoothDamp(
        transform.position, 
        desiredPosition, 
        ref currentVelocity, 
        smoothTime
        );
    }
}