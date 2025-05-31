using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TiendaController : MonoBehaviour
{
    [System.Serializable]
    public class BotonesTienda
    {
        public Button botonMinaHierro;
        public Button botonMinaOro;
        public Button botonMinaDiamante;
        public Button botonMejorarAlmacen;
        public Button botonObrero;
        public Button botonSuperObrero;
        public Button botonUltraObrero;
    }

    public BotonesTienda botones;
    public TextMeshProUGUI textoMinaSeleccionada;
    private int minaSeleccionada = 0;

    private void Start()
    {
        ConfigurarBotones();
    }

    private void ConfigurarBotones()
    {
        if (botones.botonMinaHierro != null)
            botones.botonMinaHierro.onClick.AddListener(() => ComprarMina("Hierro"));
        if (botones.botonMinaOro != null)
            botones.botonMinaOro.onClick.AddListener(() => ComprarMina("Oro"));
        if (botones.botonMinaDiamante != null)
            botones.botonMinaDiamante.onClick.AddListener(() => ComprarMina("Diamante"));
        if (botones.botonMejorarAlmacen != null)
            botones.botonMejorarAlmacen.onClick.AddListener(MejorarAlmacenamiento);
        if (botones.botonObrero != null)
            botones.botonObrero.onClick.AddListener(() => ContratarTrabajador("Obrero"));
        if (botones.botonSuperObrero != null)
            botones.botonSuperObrero.onClick.AddListener(() => ContratarTrabajador("SuperObrero"));
        if (botones.botonUltraObrero != null)
            botones.botonUltraObrero.onClick.AddListener(() => ContratarTrabajador("UltraObrero"));
    }

    public void ComprarMina(string tipo)
    {
        if (GameManager.instancia != null)
            GameManager.instancia.ComprarMina(tipo);
    }

    public void MejorarAlmacenamiento()
    {
        if (GameManager.instancia != null)
            GameManager.instancia.MejorarAlmacenamiento();
    }

    public void ContratarTrabajador(string tipo)
    {
        if (GameManager.instancia != null)
            GameManager.instancia.ContratarTrabajador(minaSeleccionada, tipo);
    }

    public void SeleccionarMina(int indice)
    {
        minaSeleccionada = indice;
        ActualizarTextoMina();
    }

    public void SiguienteMina()
    {
        if (GameManager.instancia?.jugador?.minas != null)
        {
            minaSeleccionada++;
            if (minaSeleccionada >= GameManager.instancia.jugador.minas.Count)
                minaSeleccionada = 0;
            ActualizarTextoMina();
        }
    }

    public void AnteriorMina()
    {
        if (GameManager.instancia?.jugador?.minas != null)
        {
            minaSeleccionada--;
            if (minaSeleccionada < 0)
                minaSeleccionada = GameManager.instancia.jugador.minas.Count - 1;
            ActualizarTextoMina();
        }
    }

    private void ActualizarTextoMina()
    {
        if (textoMinaSeleccionada != null && GameManager.instancia?.jugador?.minas != null)
        {
            if (minaSeleccionada < GameManager.instancia.jugador.minas.Count)
            {
                Mina mina = GameManager.instancia.jugador.minas[minaSeleccionada];
                textoMinaSeleccionada.text = $"Mina {minaSeleccionada}: {mina.tipo} ({mina.estado})";
            }
        }
    }
}