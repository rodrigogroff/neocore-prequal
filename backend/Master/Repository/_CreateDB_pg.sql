
CREATE TABLE IF NOT EXISTS public."Company" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."Company" ADD COLUMN if not exists "stName" character varying(250);
ALTER TABLE public."Company" ADD COLUMN if not exists "client_id" uuid;
ALTER TABLE public."Company" ADD COLUMN if not exists "stSecret" character varying(100);
ALTER TABLE public."Company" ADD COLUMN if not exists "bActive" boolean;

CREATE INDEX IF NOT EXISTS idx_company_client_id ON public."Company" ("client_id");

CREATE TABLE IF NOT EXISTS public."Feature" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."Feature" ADD COLUMN if not exists "stEndpoint" character varying(999);
ALTER TABLE public."Feature" ADD COLUMN if not exists "bActive" boolean;

CREATE INDEX IF NOT EXISTS idx_feature_endpoint ON public."Feature" ("stEndpoint");

CREATE TABLE IF NOT EXISTS public."CompanyFinanceiro" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyFinanceiro" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyFinanceiro" ADD COLUMN if not exists "bActiveSubL1" boolean;
ALTER TABLE public."CompanyFinanceiro" ADD COLUMN if not exists "bActiveSubL2" boolean;
ALTER TABLE public."CompanyFinanceiro" ADD COLUMN IF not exists "vrSubscriptionL1" numeric(10,2);
ALTER TABLE public."CompanyFinanceiro" ADD COLUMN IF not exists "vrL1Transaction" numeric(10,2);
ALTER TABLE public."CompanyFinanceiro" ADD COLUMN IF not exists "vrL1TransactionItem" numeric(10,2);
ALTER TABLE public."CompanyFinanceiro" ADD COLUMN IF not exists "vrSubscriptionL2" numeric(10,2);
ALTER TABLE public."CompanyFinanceiro" ADD COLUMN IF not exists "vrL2Transaction" numeric(10,2);
ALTER TABLE public."CompanyFinanceiro" ADD COLUMN IF not exists "vrL2TransactionItem" numeric(10,2);

CREATE INDEX IF NOT EXISTS idx_companyfinanceiro_fkcompany ON public."CompanyFinanceiro" ("fkCompany");

CREATE TABLE IF NOT EXISTS public."CompanyFatura" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyFatura" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyFatura" ADD COLUMN if not exists "nuYear" int;
ALTER TABLE public."CompanyFatura" ADD COLUMN if not exists "nuMonth" int;
ALTER TABLE public."CompanyFatura" ADD COLUMN IF not exists "vrSubscriptionL1" numeric(10,2);
ALTER TABLE public."CompanyFatura" ADD COLUMN IF not exists "vrL1Transaction" numeric(10,2);
ALTER TABLE public."CompanyFatura" ADD COLUMN IF not exists "vrL1TransactionItem" numeric(10,2);
ALTER TABLE public."CompanyFatura" ADD COLUMN IF not exists "vrL1TransactionTotal" numeric(10,2);
ALTER TABLE public."CompanyFatura" ADD COLUMN IF not exists "vrL1TransactionItemTotal" numeric(10,2);
ALTER TABLE public."CompanyFatura" ADD COLUMN if not exists "nuQtdL1Trans" int;
ALTER TABLE public."CompanyFatura" ADD COLUMN if not exists "nuQtdL1TransItem" int;
ALTER TABLE public."CompanyFatura" ADD COLUMN IF not exists "vrSubscriptionL2" numeric(10,2);
ALTER TABLE public."CompanyFatura" ADD COLUMN IF not exists "vrL2Transaction" numeric(10,2);
ALTER TABLE public."CompanyFatura" ADD COLUMN IF not exists "vrL2TransactionItem" numeric(10,2);
ALTER TABLE public."CompanyFatura" ADD COLUMN IF not exists "vrL2TransactionTotal" numeric(10,2);
ALTER TABLE public."CompanyFatura" ADD COLUMN IF not exists "vrL2TransactionItemTotal" numeric(10,2);
ALTER TABLE public."CompanyFatura" ADD COLUMN if not exists "nuQtdL2Trans" int;
ALTER TABLE public."CompanyFatura" ADD COLUMN if not exists "nuQtdL2TransItem" int;
ALTER TABLE public."CompanyFatura" ADD COLUMN IF not exists "vrSubTotal" numeric(10,2);
ALTER TABLE public."CompanyFatura" ADD COLUMN IF not exists "vrImpostos" numeric(10,2);
ALTER TABLE public."CompanyFatura" ADD COLUMN IF not exists "vrTotal" numeric(10,2);

CREATE INDEX IF NOT EXISTS idx_companyfatura_company_year_month ON public."CompanyFatura" ("fkCompany", "nuYear", "nuMonth");

CREATE TABLE IF NOT EXISTS public."User" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."User" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."User" ADD COLUMN if not exists "stEmail" character varying(500);
ALTER TABLE public."User" ADD COLUMN if not exists "stCPF" character varying(20);
ALTER TABLE public."User" ADD COLUMN if not exists "stName" character varying(200);
ALTER TABLE public."User" ADD COLUMN if not exists "stPhoneNumber" character varying(50);
ALTER TABLE public."User" ADD COLUMN if not exists "stPassword" character varying(20);
ALTER TABLE public."User" ADD COLUMN if not exists "bActive" boolean;
ALTER TABLE public."User" ADD COLUMN if not exists "bAdmin" boolean;

CREATE INDEX IF NOT EXISTS idx_user ON public."User" ("fkCompany");
CREATE INDEX IF NOT EXISTS idx_user_stemail ON public."User" ("stEmail");

CREATE TABLE IF NOT EXISTS public."PrequalLeilaoConfig" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."PrequalLeilaoConfig" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."PrequalLeilaoConfig" ADD COLUMN if not exists "bEmpregadorCnpj" boolean;
ALTER TABLE public."PrequalLeilaoConfig" ADD COLUMN if not exists "bEmpregadorCpf" boolean;
ALTER TABLE public."PrequalLeilaoConfig" ADD COLUMN if not exists "bPep" boolean;
ALTER TABLE public."PrequalLeilaoConfig" ADD COLUMN if not exists "bAlertaSaude" boolean;
ALTER TABLE public."PrequalLeilaoConfig" ADD COLUMN if not exists "bAlertaAvisoPrevio" boolean;
ALTER TABLE public."PrequalLeilaoConfig" ADD COLUMN if not exists "vrLibMin" int;
ALTER TABLE public."PrequalLeilaoConfig" ADD COLUMN if not exists "vrLibMax" int;
ALTER TABLE public."PrequalLeilaoConfig" ADD COLUMN if not exists "nuParcMin" int;
ALTER TABLE public."PrequalLeilaoConfig" ADD COLUMN if not exists "nuParcMax" int;
ALTER TABLE public."PrequalLeilaoConfig" ADD COLUMN if not exists "nuIdadeMin" int;
ALTER TABLE public."PrequalLeilaoConfig" ADD COLUMN if not exists "nuIdadeMax" int;
ALTER TABLE public."PrequalLeilaoConfig" ADD COLUMN if not exists "vrMargemMin" int;
ALTER TABLE public."PrequalLeilaoConfig" ADD COLUMN if not exists "vrMargemMax" int;
ALTER TABLE public."PrequalLeilaoConfig" ADD COLUMN if not exists "nuMesesAdmissaoMin" int;
ALTER TABLE public."PrequalLeilaoConfig" ADD COLUMN if not exists "nuMesesAdmissaoMax" int;
ALTER TABLE public."PrequalLeilaoConfig" ADD COLUMN if not exists "nuMesesAberturaEmpresaMin" int;

CREATE INDEX IF NOT EXISTS idx_prequalleilaoconfig_fkcompany ON public."PrequalLeilaoConfig" ("fkCompany");

CREATE TABLE IF NOT EXISTS public."LogProcPrequalLeilao" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "dtLog" timestamp without time zone;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuYear" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuMonth" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuDay" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuHour" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuMinute" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuTotMS" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuTotProcs" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuTotQualificadas" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuTotRejeitadas" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN IF NOT EXISTS "nuPctFilter" numeric(10,2);
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuFilter1" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuFilter2" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuFilter3" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuFilter4" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuFilter5" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuFilter6" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuFilter7" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuFilter8" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuFilter9" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuFilter10" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuFilter11" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuFilter12" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuFilter13" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuFilter14" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuFilter15" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuFilter16" int;
ALTER TABLE public."LogProcPrequalLeilao" ADD COLUMN if not exists "nuFilter17" int;

CREATE INDEX IF NOT EXISTS idx_logprocprequalleilao_company_year_month ON public."LogProcPrequalLeilao" ("fkCompany", "nuYear", "nuMonth");

CREATE TABLE IF NOT EXISTS public."DadosEmpresa" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."DadosEmpresa" ADD COLUMN if not exists "dtExpire" timestamp without time zone;
ALTER TABLE public."DadosEmpresa" ADD COLUMN if not exists "stCNPJ" character varying(20);
ALTER TABLE public."DadosEmpresa" ADD COLUMN if not exists "dtAberturaL1" timestamp without time zone;
ALTER TABLE public."DadosEmpresa" ADD COLUMN if not exists "stSituacaoCadL1" character varying(50);
ALTER TABLE public."DadosEmpresa" ADD COLUMN if not exists "stSituacaoCadMotivL1" character varying(500);
ALTER TABLE public."DadosEmpresa" ADD COLUMN if not exists "stNomeL1" character varying(500);
ALTER TABLE public."DadosEmpresa" ADD COLUMN if not exists "stFantasiaL1" character varying(500);
ALTER TABLE public."DadosEmpresa" ADD COLUMN if not exists "stPorteL1" character varying(100);
ALTER TABLE public."DadosEmpresa" ADD COLUMN if not exists "stMunicipioL1" character varying(500);
ALTER TABLE public."DadosEmpresa" ADD COLUMN if not exists "stUfL1" character varying(10);
ALTER TABLE public."DadosEmpresa" ADD COLUMN if not exists "stCepL1" character varying(20);
ALTER TABLE public."DadosEmpresa" ADD COLUMN if not exists "stCnaeL1" character varying(100);
ALTER TABLE public."DadosEmpresa" ADD COLUMN if not exists "stCnaeDescL1" character varying(500);
ALTER TABLE public."DadosEmpresa" ADD COLUMN if not exists "stCdNatJurL1" character varying(500);

CREATE INDEX IF NOT EXISTS idx_dadosempresa_stcnpj ON public."DadosEmpresa" ("stCNPJ");

-- # ====================================================
-- # Tabela de endpoints / feature toggle
-- # ====================================================

INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/scheduler','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/scheduler');

INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/authenticate-user','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/authenticate-user');
INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/authenticate-server','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/authenticate-server');

INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/company-get','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/company-get');
INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/company-listing','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/company-listing');
INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/company-update','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/company-update');

INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/user-get','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/user-get');
INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/user-listing','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/user-listing');
INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/user-update','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/user-update');

INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/consulta-pj-basic','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/consulta-pj-basic');

INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/precificacao','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/precificacao');
INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/fatura-servicos','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/fatura-servicos');
INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/fatura-servicos-detalhada','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/fatura-servicos-detalhada');

INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/propostas-leilao-ctps','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/propostas-leilao-ctps');
INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/propostas-leilao-ctps-node','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/propostas-leilao-ctps-node');

INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/config-prequal-leilao-update','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/config-prequal-leilao-update');
INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/config-prequal-leilao','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/config-prequal-leilao');

INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/setup-cbo','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/setup-cbo');
INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/setup-cnae','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/setup-cnae');
INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/setup-nat-jur','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/setup-nat-jur');
INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/setup-porte','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/setup-porte');
INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/setup-tipo-pessoa','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/setup-tipo-pessoa');
INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/setup-whitelist-situacao','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/setup-whitelist-situacao');

INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/stats-dashboard','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/stats-dashboard');
INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/stats-dashboard-traffic','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/stats-dashboard-traffic');
INSERT INTO public."Feature" ("stEndpoint","bActive") SELECT 'api/stats-dashboard-prequal-info','1' WHERE NOT EXISTS (SELECT 1 FROM public."Feature" WHERE "stEndpoint" = 'api/stats-dashboard-prequal-info');


