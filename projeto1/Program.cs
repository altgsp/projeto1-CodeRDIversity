using System;
using System.Collections.Generic;

class Program
{
    private static int proximoNumeroDeContainer = 0;

    static void Main()
    {
        Geladeira geladeira = new Geladeira();

        //containers com numeração
        var hortifrutiContainers = CriarContainers("Hortifrúti", 2);
        var laticiniosEnlatadosContainers = CriarContainers("Laticínios Enlatados", 2);
        var carnesEOvosContainers = CriarContainers("Carnes Ovos", 2);

        geladeira.AdicionarAndar(new Andar(1, hortifrutiContainers));
        geladeira.AdicionarAndar(new Andar(2, laticiniosEnlatadosContainers));
        geladeira.AdicionarAndar(new Andar(3, carnesEOvosContainers));

        bool continuar = true;
        while (continuar)
        {
            //switch case para comandos e depois ele tem que exibir a geladeira atulizada com itens que ficaram apos exclusi ou adicionar
            Console.WriteLine("\nDigite o comando:");
            string comando = Console.ReadLine().ToLower();
            switch (comando)
            {
                case "adicionar":
                    AdicionarItem(geladeira);
                    break;
                case "remover":
                    RemoverItem(geladeira);
                    break;
                case "sair":
                    continuar = false;
                    break;
                default:
                    Console.WriteLine("Comando não reconhecido.");
                    break;
            }
            geladeira.ExibirResumoItens();
        }
    }

    static List<Container> CriarContainers(string prefixo, int quantidade)
    {
        List<Container> containers = new List<Container>();
        for (int i = 0; i < quantidade; i++)
        {
            containers.Add(new Container($"{prefixo} {proximoNumeroDeContainer++}", new List<string>(), 4));
        }
        return containers;
    }

    static void AdicionarItem(Geladeira geladeira)
    {
        Console.Write("Digite o nome do item: ");
        string item = Console.ReadLine();
        Console.Write("Digite o número do andar: ");
        int numeroDoAndar = Convert.ToInt32(Console.ReadLine()) - 1; // Ajuste para indexação baseada em 0
        Console.Write("Digite o número do container no andar: ");
        int numeroDoContainer = Convert.ToInt32(Console.ReadLine()) - 1; // Ajuste para indexação baseada em 0
        Console.Write("Digite a posição no container para adicionar o item: ");
        int posicao = Convert.ToInt32(Console.ReadLine());

        if (numeroDoAndar >= 0 && numeroDoAndar < geladeira.Andares.Count &&
            numeroDoContainer >= 0 && numeroDoContainer < geladeira.Andares[numeroDoAndar].Containers.Count)
        {
            Andar andar = geladeira.Andares[numeroDoAndar];
            Container container = andar.Containers[numeroDoContainer];
            //tratamento de erro
            if (container.AdicionarItem(item, posicao))
            {
                Console.WriteLine($"Item '{item}' adicionado ao container '{container.Nome}' na posição {posicao}.");
            }
            else
            {
                Console.WriteLine("Não foi possível adicionar o item.");
            }
        }
        else
        {
            Console.WriteLine("Andar ou container não encontrado.");
        }
    }

    static void RemoverItem(Geladeira geladeira)
    {
        Console.Write("Digite o nome do item: ");
        string item = Console.ReadLine();
        Console.Write("Digite o número do andar: ");
        int numeroDoAndar = Convert.ToInt32(Console.ReadLine()) - 1; 
        Console.Write("Digite o número do container no andar: ");
        int numeroDoContainer = Convert.ToInt32(Console.ReadLine()) - 1; 
        Console.Write("Digite a posição no container para remover o item: ");
        int posicao = Convert.ToInt32(Console.ReadLine());

        if (numeroDoAndar >= 0 && numeroDoAndar < geladeira.Andares.Count &&
            numeroDoContainer >= 0 && numeroDoContainer < geladeira.Andares[numeroDoAndar].Containers.Count)
        {
            Andar andar = geladeira.Andares[numeroDoAndar];
            Container container = andar.Containers[numeroDoContainer];

            if (container.RemoverItem(item, posicao))
            {
                Console.WriteLine($"Item '{item}' removido do container '{container.Nome}' na posição {posicao}.");
            }
            else
            {
                Console.WriteLine("O item não foi encontrado ou a posição está vazia.");
            }
        }
        else
        {
            Console.WriteLine("Andar ou container não encontrado.");
        }
    }
}

public class Geladeira
{
    public List<Andar> Andares { get; set; } = new List<Andar>();

    public void AdicionarAndar(Andar andar)
    {
        Andares.Add(andar);
    }

    public void ExibirResumoItens()
    {
        foreach (var andar in Andares)
        {
            Console.WriteLine($"Andar {andar.Numero}:");
            foreach (var container in andar.Containers)
            {
                Console.WriteLine($" - {container.Nome}: {string.Join(", ", container.Itens)}");
            }
        }
    }
}

public class Andar
{
    public int Numero { get; set; }
    public List<Container> Containers { get; set; } = new List<Container>();

    public Andar(int numero, List<Container> containers)
    {
        Numero = numero;
        Containers.AddRange(containers);
    }
}

public class Container
{
    public string Nome { get; set; }
    public List<string> Itens { get; set; } = new List<string>();
    public int CapacidadeMaxima { get; set; }

    public Container(string nome, List<string> itens, int capacidadeMaxima)
    {
        Nome = nome;
        Itens.AddRange(itens);
        CapacidadeMaxima = capacidadeMaxima;
    }

    public bool AdicionarItem(string item, int posicao)
    {
        if (posicao < 1 || posicao > CapacidadeMaxima)
        {
            Console.WriteLine("Posição inválida.");
            return false;
        }

        if (Itens.Count >= CapacidadeMaxima)
        {
            Console.WriteLine($"O container '{Nome}' já está cheio.");
            return false;
        }

        Itens.Insert(posicao - 1, item); 
        return true;
    }

    //posicao container e tem que comaparar com capacidade do container
    public bool RemoverItem(string item, int posicao)
    {
        if (posicao < 1 || posicao > CapacidadeMaxima)
        {
            Console.WriteLine("Posição inválida.");
            return false;
        }

        if (Itens[posicao - 1] != item) 
        {
            Console.WriteLine($"O item '{item}' não foi encontrado na posição {posicao} do container '{Nome}'.");
            return false;
        }

        Itens.RemoveAt(posicao - 1); 
        return true;
    }
}
