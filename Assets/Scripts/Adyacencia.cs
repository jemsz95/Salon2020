using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

class Adyacencia : List<List<int>>
{
    // Matriz 2D que contiene las adyacencias entre los nodos. (Estos son los "Arcos").
    List<List<int>> adyacencias = new List<List<int>>();

    public void AddNode(int size)
    {
        // Iteramos a traves de la matriz para hacerla crecer en cada renglón para que se mantenga como un arreglo bidimensional
        foreach (List<int> list in adyacencias) 
        {
            list.Add(0);
        }

        adyacencias.Add(new List<int>(new int[size]));
    }

    public void RemoveNode(int index)
    {
        adyacencias.RemoveAt(index);

        for (int i = 0; i < adyacencias.Count; i++)
        {
            adyacencias[i].RemoveAt(index);
        }
    }

    public int this[int i, int j]
    {
        get { return adyacencias[i][j]; }
        set { adyacencias[i][j] = value; }
    }

    override public string ToString()
    {
        string result = "";

        foreach (List<int> list in adyacencias)
        {
            foreach (int item in list)
            {
                result += item + " ";
            }
            result += "\n";
        }

        return result;
    }
}

