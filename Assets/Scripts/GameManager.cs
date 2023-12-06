using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public  static GameManager Instance;
    public TextMeshProUGUI StagescoreText;
    public TextMeshProUGUI TotalscoreText;
    

    public bool isGameover = false;
    
    public  TextMeshProUGUI gameoverUI;

    private int Stagescore = 0;
    private int Totalscore = 0;
    void Awake() {
    
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
       
        }
        else {
            Destroy(gameObject);
        }

    }

    void OnEnable() {
    
        SceneManager.sceneLoaded += OnSceneLoaded;
       
    }

    void OnDisable() {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) {

        StagescoreText = GameObject.Find("Stage").GetComponent<TextMeshProUGUI>();
        TotalscoreText = GameObject.Find("Total").GetComponent<TextMeshProUGUI>();
       
   
        UpdateUI();
    }



    void Update() {
        if (isGameover && Input.GetMouseButtonDown(0)) {

            SceneManager.LoadScene(0);
            isGameover = false;
            Res();
         
        }

    }

    public void AddScore(int newScore) {
        if (!isGameover) {
            Stagescore += newScore;
            StagescoreText.text = "Stage: " + Stagescore;
        }
      
    }

    public void TotalAddScore(int newScore) {   
            Totalscore += newScore;
            TotalscoreText.text = "Total: " + Totalscore;
            UpdateUI();
    }

    public void OnPlayerDead() {
        isGameover = true;

        GameObject.Find("Canvas").transform.Find("GameOver").gameObject.SetActive(true);
    }
    
    public int GetScore() {
        return Stagescore;
    }

    public void LoadNextScene() {
       
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
  
        int nextSceneIndex = currentSceneIndex + 1;

        SceneManager.LoadScene(nextSceneIndex);
    
        Stagescore = 0;
    }

    void UpdateUI() {
        StagescoreText.text = "Stage: " + Stagescore;
        TotalscoreText.text = "Total: " + Totalscore;
   
    }

    public void Res() {
        
        Totalscore = 0;
        Stagescore = 0;
    }
    public void Res2() {

        Totalscore = Totalscore - Stagescore;
        Stagescore = 0;
    }
}
