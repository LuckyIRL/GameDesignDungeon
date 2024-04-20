using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    // Player's health variables using UnitHealth class
    public UnitHealth _playerHealth = new UnitHealth(100, 100);
}
