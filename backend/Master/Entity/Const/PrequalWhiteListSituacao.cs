using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Master.Entity.Const
{
    [ExcludeFromCodeCoverage]
    public static class PrequalWhiteListSituacao
    {
        public const string Ativo = "Ativo";
        public const string Inativo = "Inativo";
        public const string Suspenso = "Suspenso";
        public const string Irregular = "Irregular";
        public const string Obito = "Óbito";

        public static readonly List<string> Lista = [ Ativo, Inativo, Suspenso, Irregular, Obito ];

        public static readonly List<EnumItem> Vector =
        [
            new EnumItem { Id = 1, Descricao = Ativo },
            new EnumItem { Id = 2, Descricao = Inativo },
            new EnumItem { Id = 3, Descricao = Suspenso },
            new EnumItem { Id = 4, Descricao = Irregular },
            new EnumItem { Id = 5, Descricao = Obito },
        ];
    }
}
