using System;

namespace ExemploApiCatalogoTurístico.ViewModel
{
    public class TurísticoViewModel
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Agencia { get; set; }
        public double Preco { get; set; }
    }
}
