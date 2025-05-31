using UnityEngine;
using System;
using System.Collections.Generic;

public class UltraObrero : ITrabajador
{
    public int CalcularProduccion(int produccionMina)
    {
        return produccionMina * 3;
    }
}
