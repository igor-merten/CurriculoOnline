﻿namespace CurriculoOnline.Models
{
    public class Cidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Estado Estado { get; set; }

        public Cidade() { }
        public Cidade(int id, string nome, Estado estado)
        {
            Id = id;
            Nome = nome;
            Estado = estado;
        }
    }
}
