using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Master.Entity.Const
{
    [ExcludeFromCodeCoverage]
    public static class PrequalNaturezaJurica
    {
        public static List<EnumItem>? Busca(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return null;

            // confere se o texto é numero
            if (!int.TryParse(texto.Trim(), out var mId))
                return null;

            return Vector.Where(y => y.Descricao.StartsWith(texto)).ToList();
        }

        public static readonly List<EnumItem> Vector =
        [
            // 1. ADMINISTRAÇÃO PÚBLICA (101-134)
            // Poder Executivo
            new EnumItem { Id = 1, Descricao = "101-5 : Órgão Público do Poder Executivo Federal" },
            new EnumItem { Id = 2, Descricao = "102-3 : Órgão Público do Poder Executivo Estadual ou do Distrito Federal" },
            new EnumItem { Id = 3, Descricao = "103-1 : Órgão Público do Poder Executivo Municipal" },
            
            // Poder Legislativo
            new EnumItem { Id = 4, Descricao = "104-0 : Órgão Público do Poder Legislativo Federal" },
            new EnumItem { Id = 5, Descricao = "105-8 : Órgão Público do Poder Legislativo Estadual ou do Distrito Federal" },
            new EnumItem { Id = 6, Descricao = "106-6 : Órgão Público do Poder Legislativo Municipal" },
            
            // Poder Judiciário
            new EnumItem { Id = 7, Descricao = "107-4 : Órgão Público do Poder Judiciário Federal" },
            new EnumItem { Id = 8, Descricao = "108-2 : Órgão Público do Poder Judiciário Estadual" },
            
            // Autarquias
            new EnumItem { Id = 9, Descricao = "110-4 : Autarquia Federal" },
            new EnumItem { Id = 10, Descricao = "111-2 : Autarquia Estadual ou do Distrito Federal" },
            new EnumItem { Id = 11, Descricao = "112-0 : Autarquia Municipal" },
            
            // Fundações Públicas de Direito Público
            new EnumItem { Id = 12, Descricao = "113-9 : Fundação Pública de Direito Público Federal" },
            new EnumItem { Id = 13, Descricao = "114-7 : Fundação Pública de Direito Público Estadual ou do Distrito Federal" },
            new EnumItem { Id = 14, Descricao = "115-5 : Fundação Pública de Direito Público Municipal" },
            
            // Órgãos Públicos Autônomos
            new EnumItem { Id = 15, Descricao = "116-3 : Órgão Público Autônomo Federal" },
            new EnumItem { Id = 16, Descricao = "117-1 : Órgão Público Autônomo Estadual ou do Distrito Federal" },
            new EnumItem { Id = 17, Descricao = "118-0 : Órgão Público Autônomo Municipal" },
            
            // Outras Entidades Públicas
            new EnumItem { Id = 18, Descricao = "119-8 : Comissão Polinacional" },
            new EnumItem { Id = 19, Descricao = "121-0 : Consórcio Público de Direito Público (Associação Pública)" },
            new EnumItem { Id = 20, Descricao = "122-8 : Consórcio Público de Direito Privado" },
            new EnumItem { Id = 21, Descricao = "123-6 : Estado ou Distrito Federal" },
            new EnumItem { Id = 22, Descricao = "124-4 : Município" },
            
            // Fundações Públicas de Direito Privado
            new EnumItem { Id = 23, Descricao = "125-2 : Fundação Pública de Direito Privado Federal" },
            new EnumItem { Id = 24, Descricao = "126-0 : Fundação Pública de Direito Privado Estadual ou do Distrito Federal" },
            new EnumItem { Id = 25, Descricao = "127-9 : Fundação Pública de Direito Privado Municipal" },
            
            // Fundos Públicos
            new EnumItem { Id = 26, Descricao = "128-7 : Fundo Público da Administração Indireta Federal" },
            new EnumItem { Id = 27, Descricao = "129-5 : Fundo Público da Administração Indireta Estadual ou do Distrito Federal" },
            new EnumItem { Id = 28, Descricao = "130-9 : Fundo Público da Administração Indireta Municipal" },
            new EnumItem { Id = 29, Descricao = "131-7 : Fundo Público da Administração Direta Federal" },
            new EnumItem { Id = 30, Descricao = "132-5 : Fundo Público da Administração Direta Estadual ou do Distrito Federal" },
            new EnumItem { Id = 31, Descricao = "133-3 : Fundo Público da Administração Direta Municipal" },
            new EnumItem { Id = 32, Descricao = "134-1 : União" },
            
            // 2. ENTIDADES EMPRESARIAIS (201-235)
            // Empresas Públicas e Sociedades de Economia Mista
            new EnumItem { Id = 33, Descricao = "201-1 : Empresa Pública" },
            new EnumItem { Id = 34, Descricao = "203-8 : Sociedade de Economia Mista" },
            
            // Sociedades Anônimas (S.A.)
            new EnumItem { Id = 35, Descricao = "204-6 : Sociedade Anônima Aberta" },
            new EnumItem { Id = 36, Descricao = "205-4 : Sociedade Anônima Fechada" },
            
            // Sociedades Empresárias
            new EnumItem { Id = 37, Descricao = "206-2 : Sociedade Empresária Limitada" },
            new EnumItem { Id = 38, Descricao = "207-0 : Sociedade Empresária em Nome Coletivo" },
            new EnumItem { Id = 39, Descricao = "208-9 : Sociedade Empresária em Comandita Simples" },
            new EnumItem { Id = 40, Descricao = "209-7 : Sociedade Empresária em Comandita por Ações" },
            new EnumItem { Id = 41, Descricao = "212-7 : Sociedade em Conta de Participação" },
            
            // Empresário Individual e Cooperativas
            new EnumItem { Id = 42, Descricao = "213-5 : Empresário (Individual)" },
            new EnumItem { Id = 43, Descricao = "214-3 : Cooperativa" },
            new EnumItem { Id = 44, Descricao = "233-0 : Cooperativas de Consumo" },
            
            // Consórcios e Grupos
            new EnumItem { Id = 45, Descricao = "215-1 : Consórcio de Sociedades" },
            new EnumItem { Id = 46, Descricao = "216-0 : Grupo de Sociedades" },
            new EnumItem { Id = 47, Descricao = "228-3 : Consórcio de Empregadores" },
            new EnumItem { Id = 48, Descricao = "229-1 : Consórcio Simples" },
            
            // Estabelecimentos de Empresas Estrangeiras
            new EnumItem { Id = 49, Descricao = "217-8 : Estabelecimento, no Brasil, de Sociedade Estrangeira" },
            new EnumItem { Id = 50, Descricao = "219-4 : Estabelecimento, no Brasil, de Empresa Binacional Argentino-Brasileira" },
            new EnumItem { Id = 51, Descricao = "221-6 : Empresa Domiciliada no Exterior" },
            new EnumItem { Id = 52, Descricao = "227-5 : Empresa Binacional" },
            
            // Fundos e Investimentos
            new EnumItem { Id = 53, Descricao = "222-4 : Clube/Fundo de Investimento" },
            
            // Sociedades Simples
            new EnumItem { Id = 54, Descricao = "223-2 : Sociedade Simples Pura" },
            new EnumItem { Id = 55, Descricao = "224-0 : Sociedade Simples Limitada" },
            new EnumItem { Id = 56, Descricao = "225-9 : Sociedade Simples em Nome Coletivo" },
            new EnumItem { Id = 57, Descricao = "226-7 : Sociedade Simples em Comandita Simples" },
            
            // Empresa Individual de Responsabilidade Limitada (EIRELI)
            new EnumItem { Id = 58, Descricao = "230-5 : Empresa Individual de Responsabilidade Limitada (de Natureza Empresária)" },
            new EnumItem { Id = 59, Descricao = "231-3 : Empresa Individual de Responsabilidade Limitada (de Natureza Simples)" },
            
            // Outras Formas Empresariais
            new EnumItem { Id = 60, Descricao = "232-1 : Sociedade Unipessoal de Advogados" },
            new EnumItem { Id = 61, Descricao = "234-8 : Empresa Simples de Inovação (Inova Simples)" },
            new EnumItem { Id = 62, Descricao = "235-6 : Investidor Não Residente" },
            
            // 3. ENTIDADES SEM FINS LUCRATIVOS (303-399)
            // Serviços Especiais
            new EnumItem { Id = 63, Descricao = "303-4 : Serviço Notarial e Registral (Cartório)" },
            new EnumItem { Id = 64, Descricao = "307-7 : Serviço Social Autônomo" },
            
            // Fundações e Organizações
            new EnumItem { Id = 65, Descricao = "306-9 : Fundação Privada" },
            new EnumItem { Id = 66, Descricao = "330-1 : Organização Social (OS)" },
            new EnumItem { Id = 67, Descricao = "322-0 : Organização Religiosa" },
            
            // Condomínios
            new EnumItem { Id = 68, Descricao = "308-5 : Condomínio Edilício" },
            new EnumItem { Id = 69, Descricao = "331-0 : Demais Condomínios" },
            
            // Mediação e Conciliação
            new EnumItem { Id = 70, Descricao = "310-7 : Comissão de Conciliação Prévia" },
            new EnumItem { Id = 71, Descricao = "311-5 : Entidade de Mediação e Arbitragem" },
            
            // Entidades Sindicais e Políticas
            new EnumItem { Id = 72, Descricao = "313-1 : Entidade Sindical" },
            new EnumItem { Id = 73, Descricao = "325-5 : Órgão de Direção Nacional de Partido Político" },
            new EnumItem { Id = 74, Descricao = "326-3 : Órgão de Direção Regional de Partido Político" },
            new EnumItem { Id = 75, Descricao = "327-1 : Órgão de Direção Local de Partido Político" },
            new EnumItem { Id = 76, Descricao = "328-0 : Comitê Financeiro de Partido Político" },
            new EnumItem { Id = 77, Descricao = "329-8 : Frente Plebiscitária ou Referendária" },
            
            // Estabelecimentos de Entidades Estrangeiras
            new EnumItem { Id = 78, Descricao = "320-4 : Estabelecimento, no Brasil, de Fundação ou Associação Estrangeiras" },
            new EnumItem { Id = 79, Descricao = "321-2 : Fundação ou Associação Domiciliadas no Exterior" },
            
            // Comunidades e Fundos
            new EnumItem { Id = 80, Descricao = "323-9 : Comunidade Indígena" },
            new EnumItem { Id = 81, Descricao = "324-7 : Fundo Privado" },
            new EnumItem { Id = 82, Descricao = "332-8 : Plano de Benefícios de Previdência Complementar Fechada" },
            
            // Associações
            new EnumItem { Id = 83, Descricao = "399-9 : Associação Privada" },
            
            // 4. PESSOAS FÍSICAS (401-412)
            new EnumItem { Id = 84, Descricao = "401-4 : Empresa Individual Imobiliária" },
            new EnumItem { Id = 85, Descricao = "402-2 : Segurado Especial" },
            new EnumItem { Id = 86, Descricao = "408-1 : Contribuinte Individual" },
            new EnumItem { Id = 87, Descricao = "409-0 : Candidato a Cargo Político Eletivo" },
            new EnumItem { Id = 88, Descricao = "411-1 : Leiloeiro" },
            new EnumItem { Id = 89, Descricao = "412-0 : Produtor Rural (Pessoa Física)" },
            
            // 5. ORGANIZAÇÕES INTERNACIONAIS (501-503)
            new EnumItem { Id = 90, Descricao = "501-0 : Organização Internacional" },
            new EnumItem { Id = 91, Descricao = "502-9 : Representação Diplomática Estrangeira" },
            new EnumItem { Id = 92, Descricao = "503-7 : Outras Instituições Extraterritoriais" }
        ];
    }
}
