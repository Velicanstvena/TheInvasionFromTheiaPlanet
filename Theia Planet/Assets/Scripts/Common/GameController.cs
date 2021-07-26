using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class GameController : MonoBehaviour
{
    private bool alive = true;
    private bool finished = false;

    [SerializeField] private GameObject winScreen;
    [SerializeField] private GameObject loseScreen;

    public void Win()
    {
        finished = true;
        StartCoroutine(ToggleWinScreen());
    }

    public void Die()
    {
        if (!alive)
        {
            return;
        }

        alive = false;
        finished = true;
        StartCoroutine(ToggleLoseScreen());
    }

    IEnumerator ToggleWinScreen()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0.2f;
        winScreen.SetActive(true);
    }

    IEnumerator ToggleLoseScreen()
    {
        yield return new WaitForSeconds(0.5f);
        Time.timeScale = 0.2f;
        loseScreen.SetActive(true);
    }

    public bool IsAlive()
    {
        return alive;
    }

    public bool IsFinished()
    {
        return finished;
    }

    public void Leave()
    {
        if (PlayerMovement.LocalPlayerInstance.GetPhotonView().IsMine)
        {
            StartCoroutine(DisconnectAndLoad());
        }
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.LeaveRoom();
        while (PhotonNetwork.InRoom)
        {
            yield return null;
        }

        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    private void ResetVars()
    {
        alive = true;
        finished = false;
        Time.timeScale = 1;
    }
}
