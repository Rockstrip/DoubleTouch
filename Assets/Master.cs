using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Master : MonoBehaviour
{
    public MatchEnd matchEnd;
    public TextMeshProUGUI header;
    public Timer timer;
    public Player player1;
    public Player player2;

    [SerializeField] private float timeBetweenModes;
    private List<Mode> _modes;

    private void Awake()
    {
        _modes = FindObjectsOfType<Mode>().ToList();
    }

    public void StartGame()
    {
        StartCoroutine(Routine());
        IEnumerator Routine()
        {
            while (true)
            {
                var randomMode = _modes[Random.Range(0, _modes.Count)];
                header.text = randomMode.modeName;
                yield return randomMode.Run();
                header.text = "...";
                yield return new WaitForSeconds(timeBetweenModes);
            }
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
