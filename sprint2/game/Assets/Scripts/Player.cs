using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    private PlayerController controller;

    private float health;

    public enum PlayerState
    {
        Casting,
        Resting,
        Moving,
        Jumping
    }

    private PlayerState state;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<PlayerController>();
        state = PlayerState.Resting;
        health = 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (health <= 0.0f)
        {
            controller.Die();
        }
    }

    public void setHealth(float _health)
    {
        Debug.Log("set health");
        health = _health;
    }

    public float getHealth()
    {
        return health;
    }

    public void SetState(PlayerState _state)
    {
        state = _state;
    }

    public PlayerState getState()
    {
        return state;
    }
}