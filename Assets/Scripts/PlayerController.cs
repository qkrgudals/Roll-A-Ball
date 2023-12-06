using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour {
    /*************************
     1.Rigidbody 정보를 불러와서 상하좌우 키보드 방향대로 Player를 움직이도록 한다.
     2.점프키(Space키)가 눌리면 점프
    **************/
    GameObject PD;
    private Rigidbody prigidbody;
    public float speed = 8f;
    public float jumpForce = 600f;
    private int jumpCount = 0;
    private bool isGrounded = true;

    void Start() {
        prigidbody = GetComponent<Rigidbody>();
        PD = GameObject.Find("Player");
    }

    void Update() {
        float xSpeed = Input.GetAxis("Horizontal") * speed;
        float zSpeed = Input.GetAxis("Vertical") * speed;
        Vector3 newVelocity = new Vector3(xSpeed, prigidbody.velocity.y, zSpeed);

        if (Input.GetKeyDown(KeyCode.Space) && (jumpCount < 2)) {     
                prigidbody.velocity = new Vector3(prigidbody.velocity.x, 0, prigidbody.velocity.z);
                prigidbody.AddForce(Vector3.up * jumpForce);
                isGrounded = false;
                jumpCount++;
     
        }

        prigidbody.velocity = newVelocity;
    }

    private void OnCollisionEnter(Collision collision) {
       if (collision.contacts[0].normal.y > 0.7f){
        isGrounded = true;
        jumpCount = 0;
      }
   }
    private void OnCollisionExit(Collision collision) {
        isGrounded = false;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag=="DeadZone") {
            //PD.gameObject.SetActive(false);
            GameManager.Instance.OnPlayerDead();
            
        }

        if (other.gameObject.tag == "Item") {
            other.gameObject.SetActive(false);
            GameManager.Instance.AddScore(1);
            GameManager.Instance.TotalAddScore(1);

        }
        if (other.gameObject.tag == "Target") {
          
            if (GameManager.Instance.GetScore() < 5) {      
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                GameManager.Instance.Res2();
         
            }
            else {
               
                GameManager.Instance.LoadNextScene();
               
            }
        }

    }
}

