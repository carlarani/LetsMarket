﻿using BetterConsoleTables;
using Sharprompt;
using System.ComponentModel.DataAnnotations;

namespace LetsMarket
{
    public class Client
    {
        [Display(Name = "Nome")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Documento")]
        [Required(ErrorMessage = "O Documento é Obrigatório")]
        [MinLength(11)]
        [MaxLength(11)]
        public string Document { get; set; }


        [Display(Name = "Categoria")]
        public ClientCategory? Category { get; set; }

        public enum ClientCategory
        {
            Bronze,
            Prata,
            Ouro,
        }

        public static void ListCLients()
        {
            Console.WriteLine("Listando Clientes");
            Console.WriteLine();

            var table = new Table(TableConfiguration.UnicodeAlt());
            table.From(Database.Clients);
            Console.WriteLine(table.ToString());
        }

        public override string ToString()
        {
            return $"{Name} - {Document}";
        }

        public static void EditClients()
        {
            var client = Prompt.Select("Selecione o Cliente para Editar", Database.Clients, defaultValue: Database.Clients[0]);

            Prompt.Bind(client);

            Database.Save(DatabaseOption.Clients);
        }

        public static void RemoveClients()
        {
            if (Database.Clients.Count == 1)
            {
                ConsoleInputLogin.WriteError("Não é possível remover todos os usuários.");
                Console.ReadKey();
                return;
            }

            var client = Prompt.Select("Selecione o Cliente para Remover", Database.Clients);
            var confirm = Prompt.Confirm("Tem Certeza?", false);

            if (!confirm)
                return;

            Database.Clients.Remove(client);
            Database.Save(DatabaseOption.Clients);
        }
    }
}