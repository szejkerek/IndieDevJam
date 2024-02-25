using GordonEssentials;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>   
{
    [SerializeField] Slider durability;
    [SerializeField] Transform startpoint;
    public List<int> unscrewedScrews;

    List<GameObject> trash = new List<GameObject>();
    bool canRestart = false;
    bool isReloading = false;

    bool introDone = false;


    bool progressed = false;


    Vector3 respawnPosition;
    Quaternion respawnRotation;

    protected override void Awake()
    {
        base.Awake();
        if (FindObjectsOfType<GameManager>().Length > 1) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        Transform respawnPoint = GameObject.Find("Respawn").transform;
        respawnPosition = respawnPoint.position;
        respawnRotation = respawnPoint.rotation;

        durability.gameObject.SetActive(false);
        OnIntroCompleted();
    }

    private void Update()
    {
        if(Input.GetKeyUp(KeyCode.R) && canRestart)
        {
            RestartGame();
        }
    }

    IEnumerator ReloadCooldown()
    {
        isReloading = true;
        yield return new WaitForSeconds(2);
        isReloading = false;
    }

    public void OnIntroCompleted()
    {
        Debug.Log("End of intro");
        durability.gameObject.SetActive(true);
        //Player.Instance.TheTool.PlayStartingDialogue(); 
        canRestart = true;
    }

    public void AddObjectToTrash(GameObject obj)
    {
        trash.Add(obj);
    }
    private void ClearTrash()
    {
        for (int i = 0; i < trash.Count; i++)
        {
            Destroy(trash[i]);
        }
        trash.Clear();
    }

    public void SetupAfterRestart()
    {
        SetupScrews();
        if (!introDone) return;
        if (progressed)
        {
            progressed = false;
            Debug.Log("Roll new power");
        }


        SetupPlayerTransform();
        FindObjectOfType<TheToolItem>().OnInteract();
    }

    private void SetupScrews()
    {
        InteractibleScrew [] screws = FindObjectsOfType<InteractibleScrew>();
        List<InteractibleScrew> toDestroy = new List<InteractibleScrew>();
        foreach (InteractibleScrew s in screws)
        {
            if (unscrewedScrews.Contains(s.screwIndex))
            {
                toDestroy.Add(s);
            }
        }

        foreach (InteractibleScrew s in toDestroy)
        {
            Debug.Log(s.screwIndex + " destroyed");
            Destroy(s.gameObject);
        }
    }

    private void SetupPlayerTransform()
    {
        Player.Instance.transform.position = respawnPosition;
        Player.Instance.transform.rotation = respawnRotation;
    }

    public void RestartGame()
    {
        /*
        Transform player = FindObjectOfType<Player>().transform;

        FadeScreen.Instance.FadeAction(() =>
        {
            ClearTrash();
            player.transform.position = startpoint.position;
        });
        */
        //DialogueManager.Instance.ClearAndStop();

        if (isReloading) return;
        StartCoroutine(ReloadCooldown());
        

        FadeScreen.Instance.FadeAction(() =>
        {
            string currentSceneName = SceneManager.GetActiveScene().name;
            DialogueManager.Instance.ClearAndStop();
            SceneManager.LoadScene(currentSceneName);
        });

       // Invoke(nameof(DialogueManager.Instance.PlayDefaultLine), 2);
    }

    public void OnUnscrewEndgame()
    {
        Debug.Log("Unscrew");
        DialogueManager.Instance.PlayMadLine();
    }


}
