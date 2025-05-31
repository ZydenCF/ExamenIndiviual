using UnityEngine;
using System.Collections;

public abstract class Mina : MonoBehaviour
{
    public string tipo;
    public int produccion;
    public int costo;
    public string estado;
    public ITrabajador trabajador;
    public Transform puntoSpawnTrabajador;
    private bool produciendo = false;
    private GameObject trabajadorVisual;

    public abstract int GenerarPlata();

    protected virtual void Start()
    {
        if (puntoSpawnTrabajador == null)
        {
            Transform punto = transform.Find("PuntoTrabajador");
            if (punto != null)
            {
                puntoSpawnTrabajador = punto;
            }
        }
    }

    public void AsignarTrabajador(ITrabajador nuevoTrabajador, string tipoTrabajador, GameObject prefabTrabajador = null)
    {
        trabajador = nuevoTrabajador;
        estado = "Con " + tipoTrabajador;
        CrearTrabajadorVisual(tipoTrabajador, prefabTrabajador);

        if (!produciendo)
        {
            StartCoroutine(ProducirPlata());
        }
    }

    private void CrearTrabajadorVisual(string tipoTrabajador, GameObject prefabTrabajador)
    {
        if (trabajadorVisual != null)
            Destroy(trabajadorVisual);

        Vector3 posicionTrabajador = puntoSpawnTrabajador != null ?
            puntoSpawnTrabajador.position :
            transform.position + new Vector3(0, 1.5f, 0);

        if (prefabTrabajador != null)
        {
            trabajadorVisual = Instantiate(prefabTrabajador, posicionTrabajador, Quaternion.identity);
            trabajadorVisual.transform.SetParent(transform);
        }
    }

    private IEnumerator ProducirPlata()
    {
        produciendo = true;

        while (trabajador != null)
        {
            yield return new WaitForSeconds(5f);

            int ganancia = GenerarPlata();
            if (GameManager.instancia != null)
            {
                GameManager.instancia.AgregarPlata(ganancia);
            }
        }

        produciendo = false;
    }
}