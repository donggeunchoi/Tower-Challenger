using UnityEngine;

public class MapLayer : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int layerOne = LayerMask.NameToLayer("Layer 1");
        int layerTow = LayerMask.NameToLayer("Layer 2");

        if (collision.CompareTag("Player"))
        {
            if (this.gameObject.layer == layerOne)
            {
                collision.gameObject.layer = layerOne;
            }
            else if (this.gameObject.layer == layerTow)
            {
                collision.gameObject.layer = layerTow;
            }
        }
        else if (collision.CompareTag("Boss"))
        {
            if (this.gameObject.layer == layerOne)
            {
                collision.gameObject.layer = layerOne;
            }
            else if (this.gameObject.layer == layerTow)
            {
                collision.gameObject.layer = layerTow;
            }
        }
    }
}
