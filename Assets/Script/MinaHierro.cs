using UnityEngine;
using System;
using System.Collections.Generic;

public class MinaHierro : Mina
{
   private void Awake()
   {
        tipo = "Hierro";
        produccion = 20;
        costo = 100;
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


