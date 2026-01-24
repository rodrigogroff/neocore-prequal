
CREATE TABLE IF NOT EXISTS public."Company" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."Company" ADD COLUMN if not exists "stName" character varying(250);
ALTER TABLE public."Company" ADD COLUMN if not exists "client_id" uuid;
ALTER TABLE public."Company" ADD COLUMN if not exists "stSecret" character varying(100);
ALTER TABLE public."Company" ADD COLUMN if not exists "bActive" boolean;

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


