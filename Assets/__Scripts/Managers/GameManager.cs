using GordonEssentials;
using GordonEssentials.Types;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>   
{
    public Slider durability;
    [SerializeField] Transform startpoint;
    public List<int> unscrewedScrews;

    List<GameObject> trash = new List<GameObject>();
    public List<Power> possiblePowers;
    bool canRestart = false;
    bool isReloading = false;

    public bool introDone = false;


    [SerializeField] bool progressed = false;

    protected override void Awake()
    {
        base.Awake();
        if (FindObjectsOfType<GameManager>().Length > 1) Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        possiblePowers.Shuffle();
        durability.gameObject.SetActive(false);

        Player.Instance.TheTool.possiblePowers = possiblePowers;
        Player.Instance.TheTool.InitializePowers();

        SetupPlayerTransform();


        StartCoroutine(ForcePlayerPos());
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
        yield return new WaitForSeconds(1);
        isReloading = false;
    }

    IEnumerator ForcePlayerPos()
    {
        isReloading = true;
        yield return new WaitForSeconds(0.5f);
        SetupPlayerTransform();
        isReloading = false;
    }

    public void OnIntroCompleted()
    {
        Debug.Log("End of intro");
        Player.Instance.TheTool.PlayStartingDialogue(); 
        canRestart = true;
        introDone = true;
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
        Debug.Log(Player.Instance.transform.position);
        SetupScrews();
        if (!introDone) return;
        if (progressed)
        {
            possiblePowers.RemoveAt(0);
            if (possiblePowers.Count == 0) Debug.Log("++++++++++++++ GAME WON ++++++++++++++++++++");
            progressed = false;
            Debug.Log("Roll new power");
        }


        SetupPlayerTransform();
        SetupTheTool();
        Player.Instance.TheTool.PlayStartingDialogue();
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
        if (!introDone)
        {
            if (Player.Instance == null) return;
            Transform respawnPoint = GameObject.Find("Respawn").transform;
            Player.Instance.gameObject.transform.position = respawnPoint.position;
            Player.Instance.gameObject.transform.rotation = respawnPoint.rotation;
        }
    }

    private void SetupTheTool()
    {
        if (FindObjectOfType<TheToolItem>() != null)
        {
            FindObjectOfType<TheToolItem>().OnInteract();
        }
        if (FindObjectOfType<TheTool>() != null)
        {
            TheTool tool = FindObjectOfType<TheTool>();
            tool.possiblePowers = possiblePowers;
            tool.InitializePowers();
        }

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
        StartCoroutine(WaitForVictoryDialogueAndFinish());
    }

    private IEnumerator WaitForVictoryDialogueAndFinish()
    {
        while (DialogueManager.Instance.madLinePlayed)
            yield return null;
        progressed = true;
        DialogueManager.Instance.madLinePlayed = false;
        RestartGame();
        yield return null;
    }

}
