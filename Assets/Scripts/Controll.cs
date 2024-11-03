using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Controll : MonoBehaviour
{
    public static Controll control { get; private set; }
    public UnityEvent<Vector2> direction = new UnityEvent<Vector2>();
    public UnityEvent interact = new UnityEvent();
    private void Awake()
    {
        if(control == null)
        {
            control = this;
        }
        else if(control == this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        if (x!=0 || y != 0)
        {
            direction.Invoke(new Vector2(x, y)*Time.deltaTime);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            interact.Invoke();
        }
    }
}
