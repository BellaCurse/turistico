using System;

namespace ApiCatalogoTurístico.Exceptions
{
    public class TurísticoNaoCadastradoException: Exception
    {
        public TurísticoNaoCadastradoException()
            :base("Este lugar turístico não está cadastrado")
        {}
    }
}
