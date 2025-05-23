using System;
using System.Collections.Generic;
using System.Linq;

class Node<T> where T : IComparable<T>
{
    public T Data;
    public Node<T> Prev;
    public Node<T> Next;

    public Node(T data)
    {
        Data = data;
        Prev = null;
        Next = null;
    }
}

class DoublyLinkedList<T> where T : IComparable<T>
{
    private Node<T> head;

    public void AddInOrder(T data)
    {
        Node<T> newNode = new Node<T>(data);

        if (head == null || data.CompareTo(head.Data) < 0)
        {
            newNode.Next = head;
            if (head != null) head.Prev = newNode;
            head = newNode;
            return;
        }

        Node<T> current = head;
        while (current.Next != null && current.Next.Data.CompareTo(data) <= 0)
            current = current.Next;

        newNode.Next = current.Next;
        if (current.Next != null)
            current.Next.Prev = newNode;

        current.Next = newNode;
        newNode.Prev = current;
    }

    public void ShowForward()
    {
        Node<T> current = head;
        while (current != null)
        {
            Console.Write(current.Data + " ");
            current = current.Next;
        }
        Console.WriteLine();
    }

    public void ShowBackward()
    {
        Node<T> current = head;
        if (current == null) return;

        while (current.Next != null)
            current = current.Next;

        while (current != null)
        {
            Console.Write(current.Data + " ");
            current = current.Prev;
        }
        Console.WriteLine();
    }

    public void SortDescending()
    {
        List<T> items = new List<T>();
        Node<T> current = head;
        while (current != null)
        {
            items.Add(current.Data);
            current = current.Next;
        }

        items.Sort();
        items.Reverse();

        head = null;
        foreach (T item in items)
            AddInOrderDescending(item);
    }

    private void AddInOrderDescending(T data)
    {
        Node<T> newNode = new Node<T>(data);

        if (head == null || data.CompareTo(head.Data) > 0)
        {
            newNode.Next = head;
            if (head != null) head.Prev = newNode;
            head = newNode;
            return;
        }

        Node<T> current = head;
        while (current.Next != null && current.Next.Data.CompareTo(data) >= 0)
            current = current.Next;

        newNode.Next = current.Next;
        if (current.Next != null)
            current.Next.Prev = newNode;

        current.Next = newNode;
        newNode.Prev = current;
    }

    public void ShowMode()
    {
        Dictionary<T, int> freq = new Dictionary<T, int>();
        Node<T> current = head;
        while (current != null)
        {
            if (freq.ContainsKey(current.Data))
                freq[current.Data]++;
            else
                freq[current.Data] = 1;

            current = current.Next;
        }

        if (freq.Count == 0)
        {
            Console.WriteLine("Lista vacía.");
            return;
        }

        int maxFreq = freq.Values.Max();
        var modes = freq.Where(p => p.Value == maxFreq).Select(p => p.Key);

        Console.WriteLine("Moda(s): " + string.Join(", ", modes));
    }

    public void ShowGraph()
    {
        Dictionary<T, int> freq = new Dictionary<T, int>();
        Node<T> current = head;
        while (current != null)
        {
            if (freq.ContainsKey(current.Data))
                freq[current.Data]++;
            else
                freq[current.Data] = 1;

            current = current.Next;
        }

        foreach (var item in freq.OrderBy(p => p.Key))
        {
            Console.WriteLine($"{item.Key} {new string('*', item.Value)}");
        }
    }

    public void Exists(T value)
    {
        Node<T> current = head;
        while (current != null)
        {
            if (current.Data.CompareTo(value) == 0)
            {
                Console.WriteLine($"El elemento '{value}' existe en la lista.");
                return;
            }
            current = current.Next;
        }
        Console.WriteLine($"El elemento '{value}' no se encuentra en la lista.");
    }

    public void RemoveOne(T value)
    {
        Node<T> current = head;

        while (current != null)
        {
            if (current.Data.CompareTo(value) == 0)
            {
                if (current.Prev != null)
                    current.Prev.Next = current.Next;
                else
                    head = current.Next;

                if (current.Next != null)
                    current.Next.Prev = current.Prev;

                Console.WriteLine($"Se eliminó una ocurrencia de: {value}");
                return;
            }
            current = current.Next;
        }

        Console.WriteLine($"No se encontró el elemento: {value}");
    }

    public void RemoveAll(T value)
    {
        Node<T> current = head;
        bool found = false;

        while (current != null)
        {
            if (current.Data.CompareTo(value) == 0)
            {
                Node<T> toRemove = current;
                if (toRemove.Prev != null)
                    toRemove.Prev.Next = toRemove.Next;
                else
                    head = toRemove.Next;

                if (toRemove.Next != null)
                    toRemove.Next.Prev = toRemove.Prev;

                found = true;
            }
            current = current.Next;
        }

        if (found)
            Console.WriteLine($"Se eliminaron todas las ocurrencias de: {value}");
        else
            Console.WriteLine($"No se encontró el elemento: {value}");
    }
}

class Program
{
    static void Main()
    {
        DoublyLinkedList<string> list = new DoublyLinkedList<string>();

        while (true)
        {
            Console.WriteLine("\n--- MENÚ ---");
            Console.WriteLine("1. Añadir ");
            Console.WriteLine("2. Presentar hacia adelante");
            Console.WriteLine("3. Presentar hacia atrás");
            Console.WriteLine("4. Ordenar de forma desendente");
            Console.WriteLine("6. Mostrar gráfico");
            Console.WriteLine("7. Existe");
            Console.WriteLine("8. Eliminar una ocurrencia");
            Console.WriteLine("9. Eliminar todas las ocurrencias");
            Console.WriteLine("10. Salir");

            Console.Write("Seleccione una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Ingrese el elemento: ");
                    string dato = Console.ReadLine();
                    list.AddInOrder(dato);
                    break;
                case "2":
                    list.ShowForward();
                    break;
                case "3":
                    list.ShowBackward();
                    break;
                case "4":
                    list.SortDescending();
                    Console.WriteLine("Lista ordenada de forma desendente.");
                    break;
                case "5":
                    list.ShowMode();
                    break;
                case "6":
                    list.ShowGraph();
                    break;
                case "7":
                    Console.Write("Ingrese el elemento que desea buscar: ");
                    string valor = Console.ReadLine();
                    list.Exists(valor);
                    break;
                case "8":
                    Console.Write("Ingrese el elemento que desea eliminar (una ocurrencia): ");
                    string eliminarUno = Console.ReadLine();
                    list.RemoveOne(eliminarUno);
                    break;
                case "9":
                    Console.Write("Ingrese el elemento que desea eliminar (todas las ocurrencias): ");
                    string eliminarTodos = Console.ReadLine();
                    list.RemoveAll(eliminarTodos);
                    break;
                case "10":
                    return;
                default:
                    Console.WriteLine("Opción no valida.");
                    break;
            }
        }
    }
}
