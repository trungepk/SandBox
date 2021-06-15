using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource audioSource;
    public ParticleSystem explosion;
    public ParticleSystem dirt;
    public AudioClip jumpSound;
    public AudioClip crashSound;
    public float jumpForce = 10;
    public float gravityModifier = 10;
    public bool isOnGround = true;
    public int maxJumpCount = 2;
    public int jumpCount;
    public bool gameStart = false;
    public bool gameOver = false;

    public Text scoreTxt;
    public GameObject gameOverText;

    private float score;

    Vector3 startPos;
    Vector3 fromPos;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        //Physics.gravity *= gravityModifier;

        startPos = transform.position;
        fromPos = new Vector3(-6, 0, 0);
        transform.position = fromPos;
        scoreTxt.text = "Score: " + 0;
        gameOver = false;
        gameOverText.SetActive(false);
        StartCoroutine(MoveOverSeconds(gameObject, startPos, 2f));
    }

    public IEnumerator MoveOverSeconds(GameObject objectToMove, Vector3 end, float seconds)
    {
        float elapsedTime = 0;
        Vector3 startingPos = objectToMove.transform.position;
        while (elapsedTime < seconds)
        {
            objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
            elapsedTime += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        objectToMove.transform.position = end;
        gameStart = true;
    }

    void Update()
    {
        if (!gameStart)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space) && !gameOver && jumpCount < maxJumpCount)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
            playerAnim.SetTrigger("Jump_trig");
            dirt.Stop();
            audioSource.PlayOneShot(jumpSound, 1f);
            jumpCount++;
        }
        if (Input.GetKey(KeyCode.LeftShift))
        {
            Time.timeScale = 2;
        }else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            Time.timeScale = 1;
        }

        if (!gameOver)
        {
            score += Time.deltaTime;
            scoreTxt.text = "Score: " + (int)(score * 100);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            jumpCount = 0;
            dirt.Play();
        }
        else if (collision.gameObject.CompareTag("Obstacle") && !gameOver)
        {
            Debug.LogError("Game Over");
            gameOver = true;
            SaveHighestScoreData();
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            explosion.Play();
            dirt.Stop();
            audioSource.PlayOneShot(crashSound, 1f);
            gameOverText.SetActive(true);
        }
    }

    private void SaveHighestScoreData()
    {
        if (MainManager.Instance)
        {
            if ((int)(score * 100) > MainManager.Instance.highestScore)
            {
                MainManager.Instance.highestScore = (int)(score * 100);
                MainManager.Instance.topPlayerName = MainManager.Instance.curPlayerName;
                MainManager.Instance.SavePlayerAndScore();
            }
        }
    }
}
