using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Master.Entity.Const
{
    [ExcludeFromCodeCoverage]
    public static class PrequalPorteEmpresa
    {
        public static readonly List<EnumItem> Vector =
        [
            new EnumItem { Id = 1, Descricao = "ME - MICRO EMPRESA" },
            new EnumItem { Id = 2, Descricao = "EPP - EMPRESA DE PEQUENO PORTE" },
            new EnumItem { Id = 2, Descricao = "DEMAIS - MÉDIO OU GRANDE PORTE" },
        ];
    }    
}
