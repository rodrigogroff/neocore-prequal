using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace Master.Entity.Const
{
    [ExcludeFromCodeCoverage]
    public static class PrequalCnae
    {
        public static EnumItem? Busca(string texto)
        {
            if (string.IsNullOrEmpty(texto))
                return null;

            if (!int.TryParse(texto.Trim(), out var mId))
                return null;

            return Vector.FirstOrDefault(y => y.Id == mId);
        }

        public static readonly List<EnumItem> Vector =
        [
            // SEÇÃO A - AGRICULTURA, PECUÁRIA, PRODUÇÃO FLORESTAL, PESCA E AQUICULTURA
            new EnumItem { Id = 1, Descricao = "SEÇÃO A - AGRICULTURA, PECUÁRIA, PRODUÇÃO FLORESTAL, PESCA E AQUICULTURA -- 01 - Agricultura, pecuária e serviços relacionados" },
            new EnumItem { Id = 2, Descricao = "SEÇÃO A - AGRICULTURA, PECUÁRIA, PRODUÇÃO FLORESTAL, PESCA E AQUICULTURA -- 02 - Produção florestal" },
            new EnumItem { Id = 3, Descricao = "SEÇÃO A - AGRICULTURA, PECUÁRIA, PRODUÇÃO FLORESTAL, PESCA E AQUICULTURA -- 03 - Pesca e aquicultura" },
            
            // SEÇÃO B - INDÚSTRIAS EXTRATIVAS
            new EnumItem { Id = 4, Descricao = "SEÇÃO B - INDÚSTRIAS EXTRATIVAS -- 05 - Extração de carvão mineral" },
            new EnumItem { Id = 5, Descricao = "SEÇÃO B - INDÚSTRIAS EXTRATIVAS -- 06 - Extração de petróleo e gás natural" },
            new EnumItem { Id = 6, Descricao = "SEÇÃO B - INDÚSTRIAS EXTRATIVAS -- 07 - Extração de minerais metálicos" },
            new EnumItem { Id = 7, Descricao = "SEÇÃO B - INDÚSTRIAS EXTRATIVAS -- 08 - Extração de minerais não-metálicos" },
            new EnumItem { Id = 8, Descricao = "SEÇÃO B - INDÚSTRIAS EXTRATIVAS -- 09 - Atividades de apoio à extração de minerais" },
            
            // SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO
            new EnumItem { Id = 9, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 10 - Fabricação de produtos alimentícios" },
            new EnumItem { Id = 10, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 11 - Fabricação de bebidas" },
            new EnumItem { Id = 11, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 12 - Fabricação de produtos do fumo" },
            new EnumItem { Id = 12, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 13 - Fabricação de produtos têxteis" },
            new EnumItem { Id = 13, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 14 - Confecção de artigos do vestuário e acessórios" },
            new EnumItem { Id = 14, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 15 - Preparação de couros e fabricação de artefatos de couro, artigos para viagem e calçados" },
            new EnumItem { Id = 15, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 16 - Fabricação de produtos de madeira" },
            new EnumItem { Id = 16, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 17 - Fabricação de celulose, papel e produtos de papel" },
            new EnumItem { Id = 17, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 18 - Impressão e reprodução de gravações" },
            new EnumItem { Id = 18, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 19 - Fabricação de coque, de produtos derivados do petróleo e de biocombustíveis" },
            new EnumItem { Id = 19, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 20 - Fabricação de produtos químicos" },
            new EnumItem { Id = 20, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 21 - Fabricação de produtos farmoquímicos e farmacêuticos" },
            new EnumItem { Id = 21, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 22 - Fabricação de produtos de borracha e de material plástico" },
            new EnumItem { Id = 22, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 23 - Fabricação de produtos de minerais não-metálicos" },
            new EnumItem { Id = 23, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 24 - Metalurgia" },
            new EnumItem { Id = 24, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 25 - Fabricação de produtos de metal, exceto máquinas e equipamentos" },
            new EnumItem { Id = 25, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 26 - Fabricação de equipamentos de informática, produtos eletrônicos e ópticos" },
            new EnumItem { Id = 26, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 27 - Fabricação de máquinas, aparelhos e materiais elétricos" },
            new EnumItem { Id = 27, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 28 - Fabricação de máquinas e equipamentos" },
            new EnumItem { Id = 28, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 29 - Fabricação de veículos automotores, reboques e carrocerias" },
            new EnumItem { Id = 29, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 30 - Fabricação de outros equipamentos de transporte, exceto veículos automotores" },
            new EnumItem { Id = 30, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 31 - Fabricação de móveis" },
            new EnumItem { Id = 31, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 32 - Fabricação de produtos diversos" },
            new EnumItem { Id = 32, Descricao = "SEÇÃO C - INDÚSTRIAS DE TRANSFORMAÇÃO -- 33 - Manutenção, reparação e instalação de máquinas e equipamentos" },
            
            // SEÇÃO D - ELETRICIDADE E GÁS
            new EnumItem { Id = 33, Descricao = "SEÇÃO D - ELETRICIDADE E GÁS -- 35 - Eletricidade, gás e outras utilidades" },
            
            // SEÇÃO E - ÁGUA, ESGOTO, ATIVIDADES DE GESTÃO DE RESÍDUOS E DESCONTAMINAÇÃO
            new EnumItem { Id = 34, Descricao = "SEÇÃO E - ÁGUA, ESGOTO, ATIVIDADES DE GESTÃO DE RESÍDUOS E DESCONTAMINAÇÃO -- 36 - Captação, tratamento e distribuição de água" },
            new EnumItem { Id = 35, Descricao = "SEÇÃO E - ÁGUA, ESGOTO, ATIVIDADES DE GESTÃO DE RESÍDUOS E DESCONTAMINAÇÃO -- 37 - Esgoto e atividades relacionadas" },
            new EnumItem { Id = 36, Descricao = "SEÇÃO E - ÁGUA, ESGOTO, ATIVIDADES DE GESTÃO DE RESÍDUOS E DESCONTAMINAÇÃO -- 38 - Coleta, tratamento e disposição de resíduos; recuperação de materiais" },
            new EnumItem { Id = 37, Descricao = "SEÇÃO E - ÁGUA, ESGOTO, ATIVIDADES DE GESTÃO DE RESÍDUOS E DESCONTAMINAÇÃO -- 39 - Descontaminação e outros serviços de gestão de resíduos" },
            
            // SEÇÃO F - CONSTRUÇÃO
            new EnumItem { Id = 38, Descricao = "SEÇÃO F - CONSTRUÇÃO -- 41 - Construção de edifícios" },
            new EnumItem { Id = 39, Descricao = "SEÇÃO F - CONSTRUÇÃO -- 42 - Obras de infraestrutura" },
            new EnumItem { Id = 40, Descricao = "SEÇÃO F - CONSTRUÇÃO -- 43 - Serviços especializados para construção" },
            
            // SEÇÃO G - COMÉRCIO; REPARAÇÃO DE VEÍCULOS AUTOMOTORES E MOTOCICLETAS
            new EnumItem { Id = 41, Descricao = "SEÇÃO G - COMÉRCIO; REPARAÇÃO DE VEÍCULOS AUTOMOTORES E MOTOCICLETAS -- 45 - Comércio e reparação de veículos automotores e motocicletas" },
            new EnumItem { Id = 42, Descricao = "SEÇÃO G - COMÉRCIO; REPARAÇÃO DE VEÍCULOS AUTOMOTORES E MOTOCICLETAS -- 46 - Comércio por atacado, exceto veículos automotores e motocicletas" },
            new EnumItem { Id = 43, Descricao = "SEÇÃO G - COMÉRCIO; REPARAÇÃO DE VEÍCULOS AUTOMOTORES E MOTOCICLETAS -- 47 - Comércio varejista" },
            
            // SEÇÃO H - TRANSPORTE, ARMAZENAGEM E CORREIO
            new EnumItem { Id = 44, Descricao = "SEÇÃO H - TRANSPORTE, ARMAZENAGEM E CORREIO -- 49 - Transporte terrestre" },
            new EnumItem { Id = 45, Descricao = "SEÇÃO H - TRANSPORTE, ARMAZENAGEM E CORREIO -- 50 - Transporte aquaviário" },
            new EnumItem { Id = 46, Descricao = "SEÇÃO H - TRANSPORTE, ARMAZENAGEM E CORREIO -- 51 - Transporte aéreo" },
            new EnumItem { Id = 47, Descricao = "SEÇÃO H - TRANSPORTE, ARMAZENAGEM E CORREIO -- 52 - Armazenamento e atividades auxiliares dos transportes" },
            new EnumItem { Id = 48, Descricao = "SEÇÃO H - TRANSPORTE, ARMAZENAGEM E CORREIO -- 53 - Correio e outras atividades de entrega" },
            
            // SEÇÃO I - ALOJAMENTO E ALIMENTAÇÃO
            new EnumItem { Id = 49, Descricao = "SEÇÃO I - ALOJAMENTO E ALIMENTAÇÃO -- 55 - Alojamento" },
            new EnumItem { Id = 50, Descricao = "SEÇÃO I - ALOJAMENTO E ALIMENTAÇÃO -- 56 - Alimentação" },
            
            // SEÇÃO J - INFORMAÇÃO E COMUNICAÇÃO
            new EnumItem { Id = 51, Descricao = "SEÇÃO J - INFORMAÇÃO E COMUNICAÇÃO -- 58 - Edição e edição integrada à impressão" },
            new EnumItem { Id = 52, Descricao = "SEÇÃO J - INFORMAÇÃO E COMUNICAÇÃO -- 59 - Atividades cinematográficas, produção de vídeos e de programas de televisão; gravação de som e edição de música" },
            new EnumItem { Id = 53, Descricao = "SEÇÃO J - INFORMAÇÃO E COMUNICAÇÃO -- 60 - Atividades de rádio e de televisão" },
            new EnumItem { Id = 54, Descricao = "SEÇÃO J - INFORMAÇÃO E COMUNICAÇÃO -- 61 - Telecomunicações" },
            new EnumItem { Id = 55, Descricao = "SEÇÃO J - INFORMAÇÃO E COMUNICAÇÃO -- 62 - Atividades dos serviços de tecnologia da informação" },
            new EnumItem { Id = 56, Descricao = "SEÇÃO J - INFORMAÇÃO E COMUNICAÇÃO -- 63 - Atividades de prestação de serviços de informação" },
            
            // SEÇÃO K - ATIVIDADES FINANCEIRAS, DE SEGUROS E SERVIÇOS RELACIONADOS
            new EnumItem { Id = 57, Descricao = "SEÇÃO K - ATIVIDADES FINANCEIRAS, DE SEGUROS E SERVIÇOS RELACIONADOS -- 64 - Atividades de serviços financeiros" },
            new EnumItem { Id = 58, Descricao = "SEÇÃO K - ATIVIDADES FINANCEIRAS, DE SEGUROS E SERVIÇOS RELACIONADOS -- 65 - Seguros, resseguros, previdência complementar e planos de saúde" },
            new EnumItem { Id = 59, Descricao = "SEÇÃO K - ATIVIDADES FINANCEIRAS, DE SEGUROS E SERVIÇOS RELACIONADOS -- 66 - Atividades auxiliares dos serviços financeiros, seguros, previdência complementar e planos de saúde" },
            
            // SEÇÃO L - ATIVIDADES IMOBILIÁRIAS
            new EnumItem { Id = 60, Descricao = "SEÇÃO L - ATIVIDADES IMOBILIÁRIAS -- 68 - Atividades imobiliárias" },
            
            // SEÇÃO M - ATIVIDADES PROFISSIONAIS, CIENTÍFICAS E TÉCNICAS
            new EnumItem { Id = 61, Descricao = "SEÇÃO M - ATIVIDADES PROFISSIONAIS, CIENTÍFICAS E TÉCNICAS -- 69 - Atividades jurídicas, de contabilidade e de auditoria" },
            new EnumItem { Id = 62, Descricao = "SEÇÃO M - ATIVIDADES PROFISSIONAIS, CIENTÍFICAS E TÉCNICAS -- 70 - Atividades de sedes de empresas e de consultoria em gestão empresarial" },
            new EnumItem { Id = 63, Descricao = "SEÇÃO M - ATIVIDADES PROFISSIONAIS, CIENTÍFICAS E TÉCNICAS -- 71 - Serviços de arquitetura e engenharia; testes e análises técnicas" },
            new EnumItem { Id = 64, Descricao = "SEÇÃO M - ATIVIDADES PROFISSIONAIS, CIENTÍFICAS E TÉCNICAS -- 72 - Pesquisa e desenvolvimento científico" },
            new EnumItem { Id = 65, Descricao = "SEÇÃO M - ATIVIDADES PROFISSIONAIS, CIENTÍFICAS E TÉCNICAS -- 73 - Publicidade e pesquisa de mercado" },
            new EnumItem { Id = 66, Descricao = "SEÇÃO M - ATIVIDADES PROFISSIONAIS, CIENTÍFICAS E TÉCNICAS -- 74 - Outras atividades profissionais, científicas e técnicas" },
            new EnumItem { Id = 67, Descricao = "SEÇÃO M - ATIVIDADES PROFISSIONAIS, CIENTÍFICAS E TÉCNICAS -- 75 - Atividades veterinárias" },
            
            // SEÇÃO N - ATIVIDADES ADMINISTRATIVAS E SERVIÇOS COMPLEMENTARES
            new EnumItem { Id = 68, Descricao = "SEÇÃO N - ATIVIDADES ADMINISTRATIVAS E SERVIÇOS COMPLEMENTARES -- 77 - Aluguéis não-imobiliários e gestão de ativos intangíveis não-financeiros" },
            new EnumItem { Id = 69, Descricao = "SEÇÃO N - ATIVIDADES ADMINISTRATIVAS E SERVIÇOS COMPLEMENTARES -- 78 - Seleção, agenciamento e locação de mão-de-obra" },
            new EnumItem { Id = 70, Descricao = "SEÇÃO N - ATIVIDADES ADMINISTRATIVAS E SERVIÇOS COMPLEMENTARES -- 79 - Agências de viagens, operadores turísticos e serviços de reservas" },
            new EnumItem { Id = 71, Descricao = "SEÇÃO N - ATIVIDADES ADMINISTRATIVAS E SERVIÇOS COMPLEMENTARES -- 80 - Atividades de vigilância, segurança e investigação" },
            new EnumItem { Id = 72, Descricao = "SEÇÃO N - ATIVIDADES ADMINISTRATIVAS E SERVIÇOS COMPLEMENTARES -- 81 - Serviços para edifícios e atividades paisagísticas" },
            new EnumItem { Id = 73, Descricao = "SEÇÃO N - ATIVIDADES ADMINISTRATIVAS E SERVIÇOS COMPLEMENTARES -- 82 - Serviços de escritório, de apoio administrativo e outros serviços prestados às empresas" },
            
            // SEÇÃO O - ADMINISTRAÇÃO PÚBLICA, DEFESA E SEGURIDADE SOCIAL
            new EnumItem { Id = 74, Descricao = "SEÇÃO O - ADMINISTRAÇÃO PÚBLICA, DEFESA E SEGURIDADE SOCIAL -- 84 - Administração pública, defesa e seguridade social" },
            
            // SEÇÃO P - EDUCAÇÃO
            new EnumItem { Id = 75, Descricao = "SEÇÃO P - EDUCAÇÃO -- 85 - Educação" },
            
            // SEÇÃO Q - SAÚDE HUMANA E SERVIÇOS SOCIAIS
            new EnumItem { Id = 76, Descricao = "SEÇÃO Q - SAÚDE HUMANA E SERVIÇOS SOCIAIS -- 86 - Atividades de atenção à saúde humana" },
            new EnumItem { Id = 77, Descricao = "SEÇÃO Q - SAÚDE HUMANA E SERVIÇOS SOCIAIS -- 87 - Atividades de atenção à saúde humana integradas com assistência social, prestadas em residências coletivas e particulares" },
            new EnumItem { Id = 78, Descricao = "SEÇÃO Q - SAÚDE HUMANA E SERVIÇOS SOCIAIS -- 88 - Serviços de assistência social sem alojamento" },
            
            // SEÇÃO R - ARTES, CULTURA, ESPORTE E RECREAÇÃO
            new EnumItem { Id = 79, Descricao = "SEÇÃO R - ARTES, CULTURA, ESPORTE E RECREAÇÃO -- 90 - Atividades artísticas, criativas e de espetáculos" },
            new EnumItem { Id = 80, Descricao = "SEÇÃO R - ARTES, CULTURA, ESPORTE E RECREAÇÃO -- 91 - Atividades ligadas ao patrimônio cultural e ambiental" },
            new EnumItem { Id = 81, Descricao = "SEÇÃO R - ARTES, CULTURA, ESPORTE E RECREAÇÃO -- 92 - Atividades de exploração de jogos de azar e apostas" },
            new EnumItem { Id = 82, Descricao = "SEÇÃO R - ARTES, CULTURA, ESPORTE E RECREAÇÃO -- 93 - Atividades esportivas e de recreação e lazer" },
            
            // SEÇÃO S - OUTRAS ATIVIDADES DE SERVIÇOS
            new EnumItem { Id = 83, Descricao = "SEÇÃO S - OUTRAS ATIVIDADES DE SERVIÇOS -- 94 - Atividades de organizações associativas" },
            new EnumItem { Id = 84, Descricao = "SEÇÃO S - OUTRAS ATIVIDADES DE SERVIÇOS -- 95 - Reparação e manutenção de equipamentos de informática e comunicação e de objetos pessoais e domésticos" },
            new EnumItem { Id = 85, Descricao = "SEÇÃO S - OUTRAS ATIVIDADES DE SERVIÇOS -- 96 - Outras atividades de serviços pessoais" },
            
            // SEÇÃO T - SERVIÇOS DOMÉSTICOS
            new EnumItem { Id = 86, Descricao = "SEÇÃO T - SERVIÇOS DOMÉSTICOS -- 97 - Serviços domésticos" },
            
            // SEÇÃO U - ORGANISMOS INTERNACIONAIS E OUTRAS INSTITUIÇÕES EXTRATERRITORIAIS
            new EnumItem { Id = 87, Descricao = "SEÇÃO U - ORGANISMOS INTERNACIONAIS E OUTRAS INSTITUIÇÕES EXTRATERRITORIAIS -- 99 - Organismos internacionais e outras instituições extraterritoriais" }
        ];
    }
}