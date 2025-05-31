using UnityEngine;
using System;
using System.Collections.Generic;

public class SuperObrero : ITrabajador
{
    public int CalcularProduccion(int produccionMina)
    {

        return produccionMina * 2;
    }
}