using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI textoPlata;
    public TextMeshProUGUI textoVictoria;
    public GameObject panelVictoria;

    private void Start()
    {
        if (GameManager.instancia != null)
        {
            GameManager.instancia.AlCambiarPlata += ActualizarPlata;
            GameManager.instancia.AlGanarJuego += MostrarVictoria;
        }

        if (panelVictoria != null)
        {
            panelVictoria.SetActive(false);
        }
    }

    public void ActualizarPlata(int nuevaPlata)
    {
        if (GameManager.instancia != null && GameManager.instancia.jugador != null)
        {
            textoPlata.text = "Plata: " + nuevaPlata + "/" +
                           GameManager.instancia.jugador.almacenamiento;
        }
    }

    public void MostrarVictoria()
    {
        if (panelVictoria != null)
        {
            panelVictoria.SetActive(true);
        }
        if (textoVictoria != null)
        {
            textoVictoria.text = "¡FELICIDADES!¡HAS GANADO EL JUEGO!Llegaste a 10,000 de plata";
        }
    }

    private void OnDestroy()
    {
        if (GameManager.instancia != null)
        {
            GameManager.instancia.AlCambiarPlata -= ActualizarPlata;
            GameManager.instancia.AlGanarJuego -= MostrarVictoria;
        }
    }
}