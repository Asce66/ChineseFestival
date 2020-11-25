using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutScene_redEnvelope : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager_redEnvelope.Instance.DestroyGameObject(collision.gameObject);
    }
}
