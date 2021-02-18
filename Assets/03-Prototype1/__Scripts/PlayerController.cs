using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Inspector: Spawning")]
    public int deathHeight;
    public Vector3 spawn;

    [Header("Inspector: UI Elements")]
    public TextMeshProUGUI coins;
    private static int count;

    [Header("Inspector: Movement")]
    public float speed = 1;
    public float jumpStrength = 1f;
    private float movementX, movementY;
    private Vector3 jump;
    private bool isGrounded = false;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody>();

        spawn = new Vector3(
            gameObject.transform.position.x, 
            gameObject.transform.position.y, 
            gameObject.transform.position.z);

        jump = new Vector3(0f, 1f, 0f);
    }
    
    private void UpdateGUI() {
        coins.text = count + " Coins";
    }

    public static void ResetCoins() {
        count = 0;
    }

    private void OnCollisionEnter(Collision other) {
        if (other.gameObject.tag == "Ground") { isGrounded = true; }
    }

    private void OnCollisionExit(Collision other) {
        if (other.gameObject.tag == "Ground") { isGrounded = false; }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Coin")) {
            other.gameObject.SetActive(false);
            count++;

            UpdateGUI();
        }
    }

    private void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    private void Update() {
        if (isGrounded && Keyboard.current.spaceKey.IsPressed()) {
            rb.AddForce(jump * jumpStrength, ForceMode.Impulse);
            isGrounded = false;
        }

        if (gameObject.transform.position.y < deathHeight) {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            gameObject.transform.position = spawn;
        }
    }

    void FixedUpdate() {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);

        rb.AddForce(movement * speed);
    }
}
