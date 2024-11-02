using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    private void Awake()
    {
        Controll.control.direction.AddListener(PlayerMove);
    }
    private void PlayerMove(Vector2 direction)
    {
        transform.Translate(direction);
    }
}
