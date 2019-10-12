namespace CurriculoOnline.Models
{
    public class Estado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string UF { get; set; }

        public Estado() { }

        public Estado(int id, string nome, string uf)
        {
            Id = id;
            Nome = nome;
            UF = uf;
        }
    }
}
