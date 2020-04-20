using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] RectTransform winScreen, loseScreen;

    [SerializeField] int componentCount = 7;

    Door[] doors;
    Searchable[] searchables;

    PowerTerminal powerTerminal;
    PlayerController player;
    Timer timer;
    Patient patient;

    public static bool hasEnded = false;
    public static bool hasPower = false;

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        timer = FindObjectOfType<Timer>();
        powerTerminal = FindObjectOfType<PowerTerminal>();
        patient = FindObjectOfType<Patient>();
        doors = FindObjectsOfType<Door>();
        searchables = FindObjectsOfType<Searchable>();

        timer.timesUp.AddListener(EndGame);
        powerTerminal.powerOn.AddListener(PowerOn);
        patient.winGame.AddListener(WinGame);

        GameObject[] objs = GameObject.FindGameObjectsWithTag("GameController");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }

        DontDestroyOnLoad(this.gameObject);
        Restart();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            Restart();
        }
    }

    public void WinGame()
    {
        //TODO
        Debug.Log("WON");
        hasEnded = true;
        winScreen.gameObject.SetActive(true);
    }

    public void EndGame()
    {
        //TODO
        Debug.Log("END CALLED");
        hasEnded = true;
        loseScreen.gameObject.SetActive(true);
    }

    public void Restart()
    {
        loseScreen.gameObject.SetActive(false);
        winScreen.gameObject.SetActive(false);

        player.Reset();
        timer.Reset();
        powerTerminal.Reset();

        for (int i = 0; i < doors.Length; i++)
        {
            doors[i].Reset();
        }

        int deployedComponentCount = 0;
        for (int i = 0; i < searchables.Length; i++)
        {
            searchables[i].Reset();
            if (deployedComponentCount < componentCount)
            {
                searchables[i].hasComponent = (3 <= Random.Range(0, 10));
                deployedComponentCount++;
            }            
        }

        hasPower = false;
        hasEnded = false;
    }

    public void PowerOn()
    {
        hasPower = true;
    }
}
