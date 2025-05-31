using System.Collections.Generic;
using UnityEngine;

public class Jugador
{
    public int plata;
    public int almacenamiento;
    public List<Mina> minas;

    public Jugador(string nombreJugador)
    {
        plata = 150; 
        almacenamiento = 600;
        minas = new List<Mina>();
    }


    public int CalcularGananciaDia()
    {
        int total = 0;
        for (int i = 0; i < minas.Count; i++)
        {
            total += minas[i].GenerarPlata();
        }

        if (total > almacenamiento)
        {
            return almacenamiento;
        }
        return total;
    }
}
