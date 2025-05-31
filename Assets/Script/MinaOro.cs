using UnityEngine;

public class MinaOro : Mina
{
    private void Awake()
    {
        tipo = "Oro";
        produccion = 50;
        costo = 550;
        estado = "Vacio";
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
