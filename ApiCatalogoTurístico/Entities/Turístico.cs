using System;

namespace ExemploApiCatalogoTurístico.Entities
{
    public class Turístico
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Agencia { get; set; }
        public double Preco { get; set; }
    }
}
