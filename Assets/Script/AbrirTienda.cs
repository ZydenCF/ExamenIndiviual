using UnityEngine;

public class AbrirTienda : MonoBehaviour
{
    public GameObject panelTienda;

    void Start()
    {
        if (panelTienda != null)
        {
            panelTienda.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            if (panelTienda != null)
            {
                bool estado = panelTienda.activeSelf;
                panelTienda.SetActive(!estado);

            }
        }
    }
}

