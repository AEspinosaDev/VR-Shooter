using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int _currentLife = 100;
    [SerializeField] private TextMeshPro _lifeUI;
    [SerializeField] private GameObject _deathMenu;
    [SerializeField] private GameObject _pistol;
    [SerializeField] private GameObject _lintern;
    [SerializeField] private GameObject _dmgUI;


    [SerializeField] private GameManager _manager;

    [HideInInspector] private int _kills = 0;
    [SerializeField] private TextMeshPro _killsUI;



    private void Start()
    {
        _lifeUI.text = _currentLife.ToString();
    }
    public void DecreaseLife(int life)
    {
        _currentLife -= life;
        _lifeUI.text = _currentLife.ToString();

        if (_currentLife <= 0) Die(); else StartCoroutine(Damage());
    }

    public void IncreaseKills()
    {
        _kills++;
        _killsUI.text = _kills.ToString();
    }
    private void Die()
    {
        _lifeUI.text = "0";
        _deathMenu.SetActive(true);
        _pistol.SetActive(false);
        _lintern.SetActive(false);

        StartCoroutine(ReturnToMainMenu());
    }
    IEnumerator ReturnToMainMenu()
    {
        yield return new WaitForSeconds(5.0f);
        this.enabled = false;
        _manager.ExitGame();
    }
    IEnumerator Damage()
    {
        _dmgUI.SetActive(true);
        yield return new WaitForSeconds(1.0f);
        _dmgUI.SetActive(false);

    }
}
