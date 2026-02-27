using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PinLauncher : MonoBehaviour
{
    [SerializeField] private GameObject pinObject;
    private Pin _currPin;
    void Start()
    {
        PreparePin();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _currPin != null && !GameManager.Instance.isGameOver)
        {
            _currPin.Launch();
            _currPin = null;
            Invoke(nameof(PreparePin), 0.1f);
        }
    }

    void PreparePin()
    {
        if (!GameManager.Instance.isGameOver)
        {
            GameObject pin = Instantiate(pinObject, transform.position, Quaternion.identity);
            _currPin = pin.GetComponent<Pin>();
            pin.transform.rotation = Quaternion.Euler(0, 0, 90);
        }
    }
}
