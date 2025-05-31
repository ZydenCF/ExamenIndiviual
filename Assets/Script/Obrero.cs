using UnityEngine;
using System;
using System.Collections.Generic;

public class Obrero : ITrabajador
{
    public int CalcularProduccion(int produccionMina)
    {
        return produccionMina;
    }
}