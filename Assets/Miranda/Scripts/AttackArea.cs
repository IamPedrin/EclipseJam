using System.Runtime.CompilerServices;
using UnityEngine;

public class AreaDeAtaque : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Enemy")) 
        {
            EclipseController.Instance.OnEnemyKilled();
            Destroy(collider.gameObject);
        }
    }


}

