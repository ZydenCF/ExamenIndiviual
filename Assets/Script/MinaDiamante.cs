using UnityEngine;

public class MinaDiamante : Mina
{
    private void Awake()
    {
        tipo = "Diamante";
        produccion = 120;
        costo = 1000;
        estado = "Ninguno";

    }

    public override int GenerarPlata()
    {
        if (trabajador != null)
        {
            return trabajador.CalcularProduccion(produccion);
        }
        return 0;
    }
}
