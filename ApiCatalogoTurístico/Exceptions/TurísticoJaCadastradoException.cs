using System;

namespace ExemploApiCatalogoTurístico.Exceptions
{
    public class TurísticoJaCadastradoException : Exception
    {
        public TurísticoJaCadastradoException()
            : base("Este já lugar turístico está cadastrado")
        { }
    }
}
