
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

