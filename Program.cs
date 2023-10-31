using System;
using System.Collections.Generic;
using System.Linq;

class Jogador
{
    public string Nome { get; set; }
    public string Nickname { get; set; }
    public int Pontos { get; private set; }

    public Jogador(string nome, string nickname)
    {
        Nome = nome;
        Nickname = nickname;
        Pontos = 0;
    }

    public void Jogar()
    {
        Random random = new Random();
        int pontosGanhos = random.Next(1, 101);
        Pontos += pontosGanhos;
        Console.WriteLine($"{Nome} ganhou {pontosGanhos} pontos nesta partida.");
    }
}

class Equipe
{
    public string NomeEquipe { get; set; }
    public List<Jogador> Jogadores { get; private set; }

    public Equipe(string nomeEquipe)
    {
        NomeEquipe = nomeEquipe;
        Jogadores = new List<Jogador>();
    }

    public int PontosTotal()
    {
        return Jogadores.Sum(jogador => jogador.Pontos);
    }

    public void AdicionarJogador(Jogador jogador)
    {
        if (Jogadores.Count < 5)
        {
            Jogadores.Add(jogador);
            Console.WriteLine($"{jogador.Nome} foi adicionado à equipe {NomeEquipe}.");
        }
        else
        {
            Console.WriteLine("A equipe já possui 5 jogadores. Não é possível adicionar mais jogadores.");
        }
    }
}

class Campeonato
{
    public string NomeCampeonato { get; set; }
    public List<Equipe> EquipesParticipantes { get; private set; }

    public Campeonato(string nomeCampeonato)
    {
        NomeCampeonato = nomeCampeonato;
        EquipesParticipantes = new List<Equipe>();
    }

    public void IniciarPartida(Equipe e1, Equipe e2)
    {
        Console.WriteLine($"Iniciando partida entre {e1.NomeEquipe} e {e2.NomeEquipe}.");
        foreach (var jogador in e1.Jogadores.Concat(e2.Jogadores))
        {
            jogador.Jogar();
        }
    }

    public void Classificacao()
    {
        var classificacao = EquipesParticipantes.OrderByDescending(equipe => equipe.PontosTotal()).ToList();
        Console.WriteLine("Classificação:");
        for (int i = 0; i < classificacao.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {classificacao[i].NomeEquipe} - {classificacao[i].PontosTotal()} pontos");
        }
    }
}

class Program
{
    static void Main()
    {
        Console.WriteLine("Bem-vindo ao Gerenciador de Campeonato!");

        Campeonato campeonato = new Campeonato("Campeonato de Jogos");

        while (true)
        {
            Console.WriteLine("\nOpções:");
            Console.WriteLine("1. Criar Equipe");
            Console.WriteLine("2. Adicionar Jogador a Equipe");
            Console.WriteLine("3. Iniciar Partida");
            Console.WriteLine("4. Mostrar Classificação");
            Console.WriteLine("5. Sair");

            int opcao;
            if (int.TryParse(Console.ReadLine(), out opcao))
            {
                switch (opcao)
                {
                    case 1:
                        Console.Write("Nome da equipe: ");
                        string nomeEquipe = Console.ReadLine();
                        Equipe equipe = new Equipe(nomeEquipe);
                        campeonato.EquipesParticipantes.Add(equipe);
                        break;
                    case 2:
                        Console.Write("Nome do jogador: ");
                        string nomeJogador = Console.ReadLine();
                        Console.Write("Nickname do jogador: ");
                        string nicknameJogador = Console.ReadLine();

                        Jogador jogador = new Jogador(nomeJogador, nicknameJogador);

                        Console.WriteLine("Escolha uma equipe para adicionar o jogador:");
                        for (int i = 0; i < campeonato.EquipesParticipantes.Count; i++)
                        {
                            Console.WriteLine($"{i + 1}. {campeonato.EquipesParticipantes[i].NomeEquipe}");
                        }

                        if (int.TryParse(Console.ReadLine(), out int equipeEscolhida) && equipeEscolhida >= 1 && equipeEscolhida <= campeonato.EquipesParticipantes.Count)
                        {
                            campeonato.EquipesParticipantes[equipeEscolhida - 1].AdicionarJogador(jogador);
                        }
                        else
                        {
                            Console.WriteLine("Opção inválida.");
                        }
                        break;
                    case 3:
                        if (campeonato.EquipesParticipantes.Count < 2)
                        {
                            Console.WriteLine("É necessário pelo menos duas equipes para iniciar uma partida.");
                        }
                        else
                        {
                            Console.WriteLine("Escolha as equipes para a partida:");
                            for (int i = 0; i < campeonato.EquipesParticipantes.Count; i++)
                            {
                                Console.WriteLine($"{i + 1}. {campeonato.EquipesParticipantes[i].NomeEquipe}");
                            }

                            int equipe1, equipe2;
                            do
                            {
                                Console.Write("Equipe 1: ");
                            } while (!int.TryParse(Console.ReadLine(), out equipe1) || equipe1 < 1 || equipe1 > campeonato.EquipesParticipantes.Count);

                            do
                            {
                                Console.Write("Equipe 2: ");
                            } while (!int.TryParse(Console.ReadLine(), out equipe2) || equipe2 < 1 || equipe2 > campeonato.EquipesParticipantes.Count || equipe2 == equipe1);

                            campeonato.IniciarPartida(campeonato.EquipesParticipantes[equipe1 - 1], campeonato.EquipesParticipantes[equipe2 - 1]);
                            campeonato.Classificacao();
                        }
                        break;
                    case 4:
                        campeonato.Classificacao();
                        break;
                    case 5:
                        Console.WriteLine("Obrigado por usar o Gerenciador de Campeonato!");
                        return;
                    default:
                        Console.WriteLine("Opção inválida. Tente novamente.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Opção inválida. Tente novamente.");
            }
        }
    }
}
