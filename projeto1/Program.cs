using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        
        Geladeira geladeira = new Geladeira();

        // itens por andar
        geladeira.AdicionarAndar(new Andar(0, "Hortifrúti", new List<string> { "Maçã", "Banana", "Laranja", "Manga", "Pera" }));
        geladeira.AdicionarAndar(new Andar(1, "Laticínios e Enlatados", new List<string> { "Leite", "Iogurte", "Queijo", "Requeijão", "Creme de Leite" }));
        geladeira.AdicionarAndar(new Andar(2, "Carnes e Ovos", new List<string> { "Bife", "Frango", "Peixe", "Ovo", "Presunto" }));

        Console.WriteLine("Itens na Geladeira:");
        foreach (var andar in geladeira.Andares)
        {
            // categorias e numeros dos andares
            Console.WriteLine($"Andar {andar.Numero + 1}: Categoria {andar.Categoria.Nome}");
            foreach (var item in andar.Categoria.Itens)
            {
                Console.WriteLine($"  {item}");
            }
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
}

public class Andar
{
    public int Numero { get; set; }
    public Categoria Categoria { get; set; }

    public Andar(int numero, string nomeCategoria, List<string> itens)
    {
        Numero = numero;
        Categoria = new Categoria(nomeCategoria, itens);
    }
}

public class Categoria
{
    public string Nome { get; set; }
    public List<string> Itens { get; set; } = new List<string>();

    public Categoria(string nome, List<string> itens)
    {
        Nome = nome;
        foreach (var item in itens)
        {
            Itens.Add(item);
        }
    }
}
}