using UnityEngine;
using System;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;
    public Jugador jugador;
    public UIController uiController;
    public TiendaController tiendaController;

    public GameObject jugadorPrefab;
    public GameObject minaHierroPrefab;
    public GameObject minaOroPrefab;
    public GameObject minaDiamantePrefab;
    public GameObject obreroPrefab;
    public GameObject superObreroPrefab;
    public GameObject ultraObreroPrefab;

    public Transform puntoInicioJugador;
    public Transform[] puntosSpawnMinas;

    private int indicePuntoMina = 0;

    public event Action<int> AlCambiarPlata;
    public event Action AlGanarJuego;

    private void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InicializarJuego();
    }

    private void InicializarJuego()
    {
        string nombreJugador = "Jugador";
        jugador = new Jugador(nombreJugador);
        if (AlCambiarPlata != null)
            AlCambiarPlata(jugador.plata);
    }

    private Transform ObtenerSiguientePuntoMina()
    {
        if (puntosSpawnMinas != null && puntosSpawnMinas.Length > 0)
        {
            Transform punto = puntosSpawnMinas[indicePuntoMina];
            indicePuntoMina = (indicePuntoMina + 1) % puntosSpawnMinas.Length;
            return punto;
        }
        return null;
    }

    private Mina CrearMinaEnEscena(string tipo, Vector3 posicion)
    {
        GameObject prefabAUsar = null;

        switch (tipo)
        {
            case "Hierro":
                prefabAUsar = minaHierroPrefab;
                break;
            case "Oro":
                prefabAUsar = minaOroPrefab;
                break;
            case "Diamante":
                prefabAUsar = minaDiamantePrefab;
                break;
        }

        if (prefabAUsar == null)
            return null;

        GameObject minaObj = Instantiate(prefabAUsar, posicion, Quaternion.identity);

        Mina componente = minaObj.GetComponent<Mina>();

        if (componente == null)
        {
            switch (tipo)
            {
                case "Hierro":
                    componente = minaObj.AddComponent<MinaHierro>();
                    break;
                case "Oro":
                    componente = minaObj.AddComponent<MinaOro>();
                    break;
                case "Diamante":
                    componente = minaObj.AddComponent<MinaDiamante>();
                    break;
            }
        }

        return componente;
    }

    public void ComprarMina(string tipo)
    {
        int costo = 0;

        switch (tipo)
        {
            case "Hierro":
                costo = 100;
                break;
            case "Oro":
                costo = 550;
                break;
            case "Diamante":
                costo = 1000;
                break;
        }

        if (jugador.plata >= costo)
        {
            Transform puntoSpawn = ObtenerSiguientePuntoMina();
            Vector3 posicion = puntoSpawn != null ? puntoSpawn.position : new Vector3(jugador.minas.Count * 3, 0, 0);

            Mina nuevaMina = CrearMinaEnEscena(tipo, posicion);

            jugador.plata -= costo;
            jugador.minas.Add(nuevaMina);

            if (AlCambiarPlata != null)
                AlCambiarPlata(jugador.plata);
        }
    }

    public void MejorarAlmacenamiento()
    {
        int costo = jugador.almacenamiento / 2;
        if (jugador.plata >= costo)
        {
            jugador.plata -= costo;
            jugador.almacenamiento += 300;

            if (AlCambiarPlata != null)
                AlCambiarPlata(jugador.plata);
        }
    }

    public void ContratarTrabajador(int indiceMina, string tipoTrabajador)
    {
        if (indiceMina >= 0 && indiceMina < jugador.minas.Count)
        {
            int costo = 0;
            ITrabajador nuevoTrabajador = null;
            GameObject prefabTrabajador = null;

            switch (tipoTrabajador)
            {
                case "Obrero":
                    costo = 50;
                    nuevoTrabajador = new Obrero();
                    prefabTrabajador = obreroPrefab;
                    break;
                case "SuperObrero":
                    costo = 200;
                    nuevoTrabajador = new SuperObrero();
                    prefabTrabajador = superObreroPrefab;
                    break;
                case "UltraObrero":
                    costo = 500;
                    nuevoTrabajador = new UltraObrero();
                    prefabTrabajador = ultraObreroPrefab;
                    break;
            }

            if (jugador.plata >= costo && nuevoTrabajador != null)
            {
                jugador.plata -= costo;
                jugador.minas[indiceMina].AsignarTrabajador(nuevoTrabajador, tipoTrabajador, prefabTrabajador);

                if (AlCambiarPlata != null)
                    AlCambiarPlata(jugador.plata);
            }
        }
    }

    public void AgregarPlata(int cantidad)
    {
        int nuevaPlata = jugador.plata + cantidad;
        jugador.plata = (nuevaPlata > jugador.almacenamiento) ? jugador.almacenamiento : nuevaPlata;

        if (AlCambiarPlata != null)
            AlCambiarPlata(jugador.plata);

        if (jugador.plata >= 10000)
        {
            GanarJuego();
        }
    }

    private void GanarJuego()
    {
        if (AlGanarJuego != null)
            AlGanarJuego();
        Time.timeScale = 0f;
    }
}