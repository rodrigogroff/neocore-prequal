
CREATE TABLE IF NOT EXISTS public."Company" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."Company" ADD COLUMN if not exists "stName" character varying(250);
ALTER TABLE public."Company" ADD COLUMN if not exists "stCode" character varying(6);
ALTER TABLE public."Company" ADD COLUMN if not exists "stEletronicInfo" character varying(99);
ALTER TABLE public."Company" ADD COLUMN if not exists "ext_id" uuid;
ALTER TABLE public."Company" ADD COLUMN if not exists "stColorBG" character varying(10);
ALTER TABLE public."Company" ADD COLUMN if not exists "stColorBGx" character varying(10);
ALTER TABLE public."Company" ADD COLUMN if not exists "stColorBG2" character varying(10);
ALTER TABLE public."Company" ADD COLUMN if not exists "stColorBG3" character varying(10);
ALTER TABLE public."Company" ADD COLUMN if not exists "stColorText" character varying(10);
ALTER TABLE public."Company" ADD COLUMN if not exists "stColorTitle" character varying(10);
ALTER TABLE public."Company" ADD COLUMN if not exists "stColorSubtitle" character varying(10);
ALTER TABLE public."Company" ADD COLUMN if not exists "stFont" character varying(10);
ALTER TABLE public."Company" ADD COLUMN if not exists "dtLog" timestamp without time zone;
ALTER TABLE public."Company" ADD COLUMN if not exists "nuArea" int;
ALTER TABLE public."Company" ADD COLUMN if not exists "bActive" boolean;
ALTER TABLE public."Company" ADD COLUMN if not exists "stDbHost" character varying(500);
ALTER TABLE public."Company" ADD COLUMN if not exists "stWS" character varying(500);
ALTER TABLE public."Company" ADD COLUMN if not exists "stLandingPage" character varying(500);
ALTER TABLE public."Company" ADD COLUMN if not exists "stPicture" character varying(500);
ALTER TABLE public."Company" ADD COLUMN if not exists "nuType" int;

CREATE INDEX IF NOT EXISTS idx_company_name ON public."Company" ("stName");
CREATE INDEX IF NOT EXISTS idx_company_eletronic_info ON public."Company" ("stEletronicInfo");

CREATE TABLE IF NOT EXISTS public."CompanyFooter" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyFooter" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyFooter" ADD COLUMN if not exists "stHtml" character varying(2000);

CREATE TABLE IF NOT EXISTS public."CompanyMenu" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyMenu" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyMenu" ADD COLUMN if not exists "nuOrder" int;
ALTER TABLE public."CompanyMenu" ADD COLUMN if not exists "stName" character varying(2000);
ALTER TABLE public."CompanyMenu" ADD COLUMN if not exists "bActive" boolean;

CREATE INDEX IF NOT EXISTS idx_companymenu_name ON public."CompanyMenu" ("fkCompany");

CREATE TABLE IF NOT EXISTS public."CompanySubmenu" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanySubmenu" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanySubmenu" ADD COLUMN if not exists "fkCompanyMenu" int;
ALTER TABLE public."CompanySubmenu" ADD COLUMN if not exists "nuOrder" int;
ALTER TABLE public."CompanySubmenu" ADD COLUMN if not exists "stName" character varying(2000);
ALTER TABLE public."CompanySubmenu" ADD COLUMN if not exists "bActive" boolean;

CREATE INDEX IF NOT EXISTS idx_companysubmenu ON public."CompanySubmenu" ("fkCompany");
CREATE INDEX IF NOT EXISTS idx_companysubmenu_menu ON public."CompanySubmenu" ("fkCompany","fkCompanyMenu");

CREATE TABLE IF NOT EXISTS public."CompanyItemMenu" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyItemMenu" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyItemMenu" ADD COLUMN if not exists "fkParentItem" int;
ALTER TABLE public."CompanyItemMenu" ADD COLUMN if not exists "fkCompanySubmenu" int;
ALTER TABLE public."CompanyItemMenu" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."CompanyItemMenu" ADD COLUMN if not exists "ext_id" uuid;
ALTER TABLE public."CompanyItemMenu" ADD COLUMN if not exists "stMenuTitle" character varying(60);
ALTER TABLE public."CompanyItemMenu" ADD COLUMN if not exists "stTitle" character varying(200);
ALTER TABLE public."CompanyItemMenu" ADD COLUMN if not exists "stSubTitle" character varying(500);
ALTER TABLE public."CompanyItemMenu" ADD COLUMN if not exists "dtLog" timestamp without time zone;
ALTER TABLE public."CompanyItemMenu" ADD COLUMN if not exists "stText" character varying(20000);
ALTER TABLE public."CompanyItemMenu" ADD COLUMN if not exists "nuMonth" int; 
ALTER TABLE public."CompanyItemMenu" ADD COLUMN if not exists "nuYear" int; 
ALTER TABLE public."CompanyItemMenu" ADD COLUMN if not exists "bActive" boolean; 
ALTER TABLE public."CompanyItemMenu" ADD COLUMN if not exists "nuOrder" int;

CREATE INDEX IF NOT EXISTS idx_companyitemmenu ON public."CompanyItemMenu" ("fkCompany");

CREATE TABLE IF NOT EXISTS public."CompanyItemMenuLike" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyItemMenuLike" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyItemMenuLike" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."CompanyItemMenuLike" ADD COLUMN if not exists "fkCompanyItemMenu" int;
ALTER TABLE public."CompanyItemMenuLike" ADD COLUMN if not exists "dtLog" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_companyitemmenulike ON public."CompanyItemMenuLike" ("fkCompany", "fkCompanyItemMenu");

CREATE TABLE IF NOT EXISTS public."CompanyItemMenuView" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyItemMenuView" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyItemMenuView" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."CompanyItemMenuView" ADD COLUMN if not exists "fkCompanyItemMenu" int;
ALTER TABLE public."CompanyItemMenuView" ADD COLUMN if not exists "dtLog" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_companyitemmenuview ON public."CompanyItemMenuView" ("fkCompany","fkCompanyItemMenu");

CREATE TABLE IF NOT EXISTS public."CompanyItemMenuComment" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyItemMenuComment" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyItemMenuComment" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."CompanyItemMenuComment" ADD COLUMN if not exists "fkCompanyItemMenu" int;
ALTER TABLE public."CompanyItemMenuComment" ADD COLUMN if not exists "stText" character varying(2000);
ALTER TABLE public."CompanyItemMenuComment" ADD COLUMN if not exists "dtLog" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_companyitemmenucomment ON public."CompanyItemMenuComment" ("fkCompany","fkCompanyItemMenu");

CREATE TABLE IF NOT EXISTS public."CompanyPrice" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyPrice" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyPrice" ADD COLUMN if not exists "vrActiveUser" int;
ALTER TABLE public."CompanyPrice" ADD COLUMN if not exists "vrMenu" int;
ALTER TABLE public."CompanyPrice" ADD COLUMN if not exists "vrSubmenu" int;
ALTER TABLE public."CompanyPrice" ADD COLUMN if not exists "vrItemMenu" int;
ALTER TABLE public."CompanyPrice" ADD COLUMN if not exists "vrNewsItemMonth" int;
ALTER TABLE public."CompanyPrice" ADD COLUMN if not exists "vrSurveyMonth" int;
ALTER TABLE public."CompanyPrice" ADD COLUMN if not exists "vrPicture" int;
ALTER TABLE public."CompanyPrice" ADD COLUMN if not exists "vrBasePlan" int;
ALTER TABLE public."CompanyPrice" ADD COLUMN if not exists "vrGeneralDiscount" int;

CREATE INDEX IF NOT EXISTS idx_companyprice ON public."CompanyPrice" ("fkCompany");

CREATE TABLE IF NOT EXISTS public."CompanyItemNews" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyItemNews" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyItemNews" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."CompanyItemNews" ADD COLUMN if not exists "ext_id" uuid;
ALTER TABLE public."CompanyItemNews" ADD COLUMN if not exists "stTitle" character varying(200);
ALTER TABLE public."CompanyItemNews" ADD COLUMN if not exists "stText" character varying(20000);
ALTER TABLE public."CompanyItemNews" ADD COLUMN if not exists "stSubTitle" character varying(500);
ALTER TABLE public."CompanyItemNews" ADD COLUMN if not exists "dtLog" timestamp without time zone;
ALTER TABLE public."CompanyItemNews" ADD COLUMN if not exists "nuMonth" int; 
ALTER TABLE public."CompanyItemNews" ADD COLUMN if not exists "nuYear" int; 
ALTER TABLE public."CompanyItemNews" ADD COLUMN if not exists "bActive" boolean; 
ALTER TABLE public."CompanyItemNews" ADD COLUMN if not exists "stThumbPicture" character varying(500);
ALTER TABLE public."CompanyItemNews" ADD COLUMN if not exists "bFrontCover" boolean;
ALTER TABLE public."CompanyItemNews" ADD COLUMN if not exists "dtCoverExpires" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_companyitemnews ON public."CompanyItemNews" ("fkCompany");
CREATE INDEX IF NOT EXISTS idx_companyitemnews_date ON public."CompanyItemNews" ("fkCompany", "dtLog");
CREATE INDEX IF NOT EXISTS idx_companyitemnews_month ON public."CompanyItemNews" ("fkCompany", "nuMonth", "nuYear");

CREATE TABLE IF NOT EXISTS public."CompanyNewsTag" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyNewsTag" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyNewsTag" ADD COLUMN if not exists "stName" int;

CREATE INDEX IF NOT EXISTS idx_companynewtag ON public."CompanyNewsTag" ("fkCompany");

CREATE TABLE IF NOT EXISTS public."CompanyItemNewsTag" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyItemNewsTag" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyItemNewsTag" ADD COLUMN if not exists "fkItemNews" int;
ALTER TABLE public."CompanyItemNewsTag" ADD COLUMN if not exists "fkNewsTag" int;

CREATE INDEX IF NOT EXISTS idx_companyitemnewtag ON public."CompanyItemNewsTag" ("fkCompany","fkItemNews", "fkNewsTag");

CREATE TABLE IF NOT EXISTS public."CompanyItemNewsLike" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyItemNewsLike" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyItemNewsLike" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."CompanyItemNewsLike" ADD COLUMN if not exists "fkCompanyItemNews" int;
ALTER TABLE public."CompanyItemNewsLike" ADD COLUMN if not exists "dtLog" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_companyitemnewslike ON public."CompanyItemNewsLike" ("fkCompany","fkCompanyItemNews");

CREATE TABLE IF NOT EXISTS public."CompanyItemNewsView" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyItemNewsView" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyItemNewsView" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."CompanyItemNewsView" ADD COLUMN if not exists "fkCompanyItemNews" int;
ALTER TABLE public."CompanyItemNewsView" ADD COLUMN if not exists "dtLog" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_companyitemnewsview ON public."CompanyItemNewsView" ("fkCompany","fkCompanyItemNews");

CREATE TABLE IF NOT EXISTS public."CompanyItemNewsComment" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyItemNewsComment" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyItemNewsComment" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."CompanyItemNewsComment" ADD COLUMN if not exists "fkCompanyItemNews" int;
ALTER TABLE public."CompanyItemNewsComment" ADD COLUMN if not exists "stText" character varying(2000);
ALTER TABLE public."CompanyItemNewsComment" ADD COLUMN if not exists "dtLog" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_companyitemnewscomment ON public."CompanyItemNewsComment" ("fkCompany","fkCompanyItemNews");

CREATE TABLE IF NOT EXISTS public."CompanyItemSurvey" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyItemSurvey" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyItemSurvey" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."CompanyItemSurvey" ADD COLUMN if not exists "ext_id" uuid;
ALTER TABLE public."CompanyItemSurvey" ADD COLUMN if not exists "stTitle" character varying(200);
ALTER TABLE public."CompanyItemSurvey" ADD COLUMN if not exists "stText" character varying(20000);
ALTER TABLE public."CompanyItemSurvey" ADD COLUMN if not exists "stSubTitle" character varying(500);
ALTER TABLE public."CompanyItemSurvey" ADD COLUMN if not exists "dtLog" timestamp without time zone;
ALTER TABLE public."CompanyItemSurvey" ADD COLUMN if not exists "nuMonth" int; 
ALTER TABLE public."CompanyItemSurvey" ADD COLUMN if not exists "nuYear" int; 
ALTER TABLE public."CompanyItemSurvey" ADD COLUMN if not exists "bActive" boolean; 
ALTER TABLE public."CompanyItemSurvey" ADD COLUMN if not exists "bAnswerEnable" boolean; 
ALTER TABLE public."CompanyItemSurvey" ADD COLUMN if not exists "bAnonymous" boolean; 

CREATE INDEX IF NOT EXISTS idx_companyitemsurvey ON public."CompanyItemSurvey" ("fkCompany");
CREATE INDEX IF NOT EXISTS idx_companyitemsurvey_date ON public."CompanyItemSurvey" ("fkCompany", "dtLog");
CREATE INDEX IF NOT EXISTS idx_companyitemsurvey_month ON public."CompanyItemSurvey" ("fkCompany", "nuMonth", "nuYear");

CREATE TABLE IF NOT EXISTS public."CompanyItemSurveyLike" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyItemSurveyLike" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyItemSurveyLike" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."CompanyItemSurveyLike" ADD COLUMN if not exists "fkCompanyItemSurvey" int;
ALTER TABLE public."CompanyItemSurveyLike" ADD COLUMN if not exists "dtLog" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_companyitemsurveylike ON public."CompanyItemSurveyLike" ("fkCompany", "fkCompanyItemSurvey");

CREATE TABLE IF NOT EXISTS public."CompanyItemSurveyView" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyItemSurveyView" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyItemSurveyView" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."CompanyItemSurveyView" ADD COLUMN if not exists "fkCompanyItemSurvey" int;
ALTER TABLE public."CompanyItemSurveyView" ADD COLUMN if not exists "dtLog" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_companyitemsurveyview ON public."CompanyItemSurveyView" ("fkCompany", "fkCompanyItemSurvey");

CREATE TABLE IF NOT EXISTS public."CompanyItemSurveyComment" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyItemSurveyComment" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyItemSurveyComment" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."CompanyItemSurveyComment" ADD COLUMN if not exists "fkCompanyItemSurvey" int;
ALTER TABLE public."CompanyItemSurveyComment" ADD COLUMN if not exists "stText" character varying(2000);
ALTER TABLE public."CompanyItemSurveyComment" ADD COLUMN if not exists "dtLog" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_companyitemsurveycomment ON public."CompanyItemSurveyComment" ("fkCompany", "fkCompanyItemSurvey");

CREATE TABLE IF NOT EXISTS public."CompanyItemSurveyVote" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyItemSurveyVote" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyItemSurveyVote" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."CompanyItemSurveyVote" ADD COLUMN if not exists "fkCompanySurvey" int;
ALTER TABLE public."CompanyItemSurveyVote" ADD COLUMN if not exists "fkCompanySurveyOption" int;
ALTER TABLE public."CompanyItemSurveyVote" ADD COLUMN if not exists "dtLog" timestamp without time zone;
ALTER TABLE public."CompanyItemSurveyVote" ADD COLUMN if not exists "stAnswer" character varying(20000);

CREATE INDEX IF NOT EXISTS idx_companyitemsurveyvote ON public."CompanyItemSurveyVote" ("fkCompany", "fkCompanySurvey");

CREATE TABLE IF NOT EXISTS public."CompanyItemSurveyOption" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyItemSurveyOption" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyItemSurveyOption" ADD COLUMN if not exists "fkCompanyItemSurvey" int;
ALTER TABLE public."CompanyItemSurveyOption" ADD COLUMN if not exists "stText" character varying(200);
ALTER TABLE public."CompanyItemSurveyOption" ADD COLUMN if not exists "nuOrder" int;

CREATE INDEX IF NOT EXISTS idx_companyitemsurveyoption ON public."CompanyItemSurveyOption" ("fkCompany", "fkCompanyItemSurvey");

CREATE TABLE IF NOT EXISTS public."CompanyEvent" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."CompanyEvent" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CompanyEvent" ADD COLUMN if not exists "nuDay" int;
ALTER TABLE public."CompanyEvent" ADD COLUMN if not exists "nuMonth" int;
ALTER TABLE public."CompanyEvent" ADD COLUMN if not exists "nuYear" int;
ALTER TABLE public."CompanyEvent" ADD COLUMN if not exists "stText" character varying(200);
ALTER TABLE public."CompanyEvent" ADD COLUMN if not exists "dtLog" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_companyevent ON public."CompanyEvent" ("fkCompany");
CREATE INDEX IF NOT EXISTS idx_companyevent_date ON public."CompanyEvent" ("fkCompany", "dtLog");
CREATE INDEX IF NOT EXISTS idx_companyevent_month ON public."CompanyEvent" ("fkCompany", "nuMonth", "nuYear");

CREATE TABLE IF NOT EXISTS public."User" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."User" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."User" ADD COLUMN if not exists "fkBusinessArea" int;
ALTER TABLE public."User" ADD COLUMN if not exists "fkBusinessUnit" int;
ALTER TABLE public."User" ADD COLUMN if not exists "nuBDay" int;
ALTER TABLE public."User" ADD COLUMN if not exists "nuBMonth" int;
ALTER TABLE public."User" ADD COLUMN if not exists "nuBYear" int;
ALTER TABLE public."User" ADD COLUMN if not exists "nuStartDay" int;
ALTER TABLE public."User" ADD COLUMN if not exists "nuStartMonth" int;
ALTER TABLE public."User" ADD COLUMN if not exists "nuStartYear" int;
ALTER TABLE public."User" ADD COLUMN if not exists "nuEmailIndex" int;
ALTER TABLE public."User" ADD COLUMN if not exists "nuAllocationType" int;
ALTER TABLE public."User" ADD COLUMN if not exists "nuSex" int;
ALTER TABLE public."User" ADD COLUMN if not exists "stEmail" character varying(500);
ALTER TABLE public."User" ADD COLUMN if not exists "stCorporateEmail" character varying(500);
ALTER TABLE public."User" ADD COLUMN if not exists "stUserPicture" character varying(500);
ALTER TABLE public."User" ADD COLUMN if not exists "stCPF" character varying(20);
ALTER TABLE public."User" ADD COLUMN if not exists "stName" character varying(200);
ALTER TABLE public."User" ADD COLUMN if not exists "stPhoneNumber" character varying(50);
ALTER TABLE public."User" ADD COLUMN if not exists "stPassword" character varying(20);
ALTER TABLE public."User" ADD COLUMN if not exists "bActive" boolean;
ALTER TABLE public."User" ADD COLUMN if not exists "bAdmin" boolean;
ALTER TABLE public."User" ADD COLUMN if not exists "ext_admin" uuid;
ALTER TABLE public."User" ADD COLUMN if not exists "dtLog" timestamp without time zone;
ALTER TABLE public."User" ADD COLUMN if not exists "dtExit" timestamp without time zone;
ALTER TABLE public."User" ADD COLUMN if not exists "stExitMotive" character varying(200);
ALTER TABLE public."User" ADD COLUMN if not exists "dtBDay" timestamp without time zone;
ALTER TABLE public."User" ADD COLUMN if not exists "dtSDay" timestamp without time zone;
ALTER TABLE public."User" ADD COLUMN if not exists "bManageNews" boolean;
ALTER TABLE public."User" ADD COLUMN if not exists "ext_news" uuid;
ALTER TABLE public."User" ADD COLUMN if not exists "bManageSurveys" boolean;
ALTER TABLE public."User" ADD COLUMN if not exists "ext_surveys" uuid;
ALTER TABLE public."User" ADD COLUMN if not exists "bManageUsers" boolean;
ALTER TABLE public."User" ADD COLUMN if not exists "ext_users" uuid;
ALTER TABLE public."User" ADD COLUMN if not exists "bManageEvents" boolean;
ALTER TABLE public."User" ADD COLUMN if not exists "ext_events" uuid;
ALTER TABLE public."User" ADD COLUMN if not exists "bManageBusiness" boolean;
ALTER TABLE public."User" ADD COLUMN if not exists "ext_business" uuid;

CREATE INDEX IF NOT EXISTS idx_user ON public."User" ("fkCompany");
CREATE INDEX IF NOT EXISTS idx_user_email ON public."User" ("stEmail", "nuEmailIndex");

CREATE TABLE IF NOT EXISTS public."BusinessArea" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."BusinessArea" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."BusinessArea" ADD COLUMN if not exists "fkHeadUser" int;
ALTER TABLE public."BusinessArea" ADD COLUMN if not exists "fkBusinessUnit" int;
ALTER TABLE public."BusinessArea" ADD COLUMN if not exists "stName" character varying(200);
ALTER TABLE public."BusinessArea" ADD COLUMN if not exists "stCountry" character varying(200);
ALTER TABLE public."BusinessArea" ADD COLUMN if not exists "stRegion" character varying(200);
ALTER TABLE public."BusinessArea" ADD COLUMN if not exists "stCity" character varying(200);
ALTER TABLE public."BusinessArea" ADD COLUMN if not exists "stAddress" character varying(500);
ALTER TABLE public."BusinessArea" ADD COLUMN if not exists "stAddressNumber" character varying(500);
ALTER TABLE public."BusinessArea" ADD COLUMN if not exists "stAddressCompl" character varying(500);
ALTER TABLE public."BusinessArea" ADD COLUMN if not exists "stAddressZipCode" character varying(500);
ALTER TABLE public."BusinessArea" ADD COLUMN if not exists "stPhone" character varying(20);
ALTER TABLE public."BusinessArea" ADD COLUMN if not exists "bActive" boolean;
ALTER TABLE public."BusinessArea" ADD COLUMN if not exists "dtCreationDate" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_businessarea ON public."BusinessArea" ("fkCompany","fkBusinessUnit", "bActive");

CREATE TABLE IF NOT EXISTS public."BusinessUnit" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."BusinessUnit" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."BusinessUnit" ADD COLUMN if not exists "fkHeadUser" int;
ALTER TABLE public."BusinessUnit" ADD COLUMN if not exists "stName" character varying(200);
ALTER TABLE public."BusinessUnit" ADD COLUMN if not exists "stCountry" character varying(200);
ALTER TABLE public."BusinessUnit" ADD COLUMN if not exists "stRegion" character varying(200);
ALTER TABLE public."BusinessUnit" ADD COLUMN if not exists "stCity" character varying(200);
ALTER TABLE public."BusinessUnit" ADD COLUMN if not exists "stAddress" character varying(500);
ALTER TABLE public."BusinessUnit" ADD COLUMN if not exists "stAddressNumber" character varying(500);
ALTER TABLE public."BusinessUnit" ADD COLUMN if not exists "stAddressCompl" character varying(500);
ALTER TABLE public."BusinessUnit" ADD COLUMN if not exists "stAddressZipCode" character varying(500);
ALTER TABLE public."BusinessUnit" ADD COLUMN if not exists "stPhone" character varying(20);
ALTER TABLE public."BusinessUnit" ADD COLUMN if not exists "bActive" boolean;
ALTER TABLE public."BusinessUnit" ADD COLUMN if not exists "dtCreationDate" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_businessunit ON public."BusinessUnit" ("fkCompany", "bActive");

CREATE TABLE IF NOT EXISTS public."Team" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."Team" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."Team" ADD COLUMN if not exists "fkBusinessArea" int;
ALTER TABLE public."Team" ADD COLUMN if not exists "fkBusinessUnit" int;
ALTER TABLE public."Team" ADD COLUMN if not exists "stName" character varying(200);
ALTER TABLE public."Team" ADD COLUMN if not exists "bActive" boolean;
ALTER TABLE public."Team" ADD COLUMN if not exists "fkHeadUser" int;
ALTER TABLE public."Team" ADD COLUMN if not exists "stTeamPicture" character varying(500);
ALTER TABLE public."Team" ADD COLUMN if not exists "stDescription" character varying(5000);

CREATE INDEX IF NOT EXISTS idx_team ON public."Team" ("fkCompany", "bActive");
CREATE INDEX IF NOT EXISTS idx_team_fks ON public."Team" ("fkCompany", "fkBusinessArea", "fkBusinessUnit");

CREATE TABLE IF NOT EXISTS public."UserTeam" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."UserTeam" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."UserTeam" ADD COLUMN if not exists "fkTeam" int;
ALTER TABLE public."UserTeam" ADD COLUMN if not exists "fkUser" int;

CREATE INDEX IF NOT EXISTS idx_userteam ON public."UserTeam" ("fkCompany", "fkTeam", "fkUser");

CREATE TABLE IF NOT EXISTS public."UserTeamSub" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."UserTeamSub" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."UserTeamSub" ADD COLUMN if not exists "fkTeam" int;
ALTER TABLE public."UserTeamSub" ADD COLUMN if not exists "fkTeamSub" int;

CREATE INDEX IF NOT EXISTS idx_userteamsub ON public."UserTeamSub" ("fkCompany", "fkTeam", "fkTeamSub");

CREATE TABLE IF NOT EXISTS public."UserAddress" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."UserAddress" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."UserAddress" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."UserAddress" ADD COLUMN if not exists "stCountry" character varying(200);
ALTER TABLE public."UserAddress" ADD COLUMN if not exists "stRegion" character varying(200);
ALTER TABLE public."UserAddress" ADD COLUMN if not exists "stCity" character varying(200);
ALTER TABLE public."UserAddress" ADD COLUMN if not exists "stAddress" character varying(500);
ALTER TABLE public."UserAddress" ADD COLUMN if not exists "stAddressNumber" character varying(500);
ALTER TABLE public."UserAddress" ADD COLUMN if not exists "stAddressCompl" character varying(500);
ALTER TABLE public."UserAddress" ADD COLUMN if not exists "stAddressZipCode" character varying(500);

CREATE INDEX IF NOT EXISTS idx_useraddress ON public."UserAddress" ("fkCompany", "fkUser");

CREATE TABLE IF NOT EXISTS public."UserCLT" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."UserCLT" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."UserCLT" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."UserCLT" ADD COLUMN if not exists "nuTipoContrato" int;
ALTER TABLE public."UserCLT" ADD COLUMN if not exists "stRG" character varying(20);
ALTER TABLE public."UserCLT" ADD COLUMN if not exists "stCTPS" character varying(20);
ALTER TABLE public."UserCLT" ADD COLUMN if not exists "stPisPasep" character varying(20);
ALTER TABLE public."UserCLT" ADD COLUMN if not exists "stSexo" character varying(120);
ALTER TABLE public."UserCLT" ADD COLUMN if not exists "stInstrucao" character varying(200);
ALTER TABLE public."UserCLT" ADD COLUMN if not exists "stNomeMae" character varying(200);
ALTER TABLE public."UserCLT" ADD COLUMN if not exists "stCBO" character varying(500);
ALTER TABLE public."UserCLT" ADD COLUMN if not exists "stOcupacao" character varying(500);
ALTER TABLE public."UserCLT" ADD COLUMN if not exists "stCategTrab" character varying(500);
ALTER TABLE public."UserCLT" ADD COLUMN if not exists "stBanco" character varying(20);
ALTER TABLE public."UserCLT" ADD COLUMN if not exists "stAgencia" character varying(20);
ALTER TABLE public."UserCLT" ADD COLUMN if not exists "stConta" character varying(20);

CREATE INDEX IF NOT EXISTS idx_userclt ON public."UserCLT" ("fkCompany", "fkUser");

-- campos específicos do brasil ficarão em PT-br
CREATE TABLE IF NOT EXISTS public."UserPJ" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."UserPJ" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."UserPJ" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."UserPJ" ADD COLUMN if not exists "stCNPJ" character varying(50);
ALTER TABLE public."UserPJ" ADD COLUMN if not exists "stRazaoSocial" character varying(500);
ALTER TABLE public."UserPJ" ADD COLUMN if not exists "stFantasia" character varying(500);
ALTER TABLE public."UserPJ" ADD COLUMN if not exists "stRegion" character varying(200);
ALTER TABLE public."UserPJ" ADD COLUMN if not exists "stCity" character varying(200);
ALTER TABLE public."UserPJ" ADD COLUMN if not exists "stCountry" character varying(200);
ALTER TABLE public."UserPJ" ADD COLUMN if not exists "stAddress" character varying(500);
ALTER TABLE public."UserPJ" ADD COLUMN if not exists "stAddressNumber" character varying(500);
ALTER TABLE public."UserPJ" ADD COLUMN if not exists "stAddressCompl" character varying(500);
ALTER TABLE public."UserPJ" ADD COLUMN if not exists "stAddressZipCode" character varying(500);
ALTER TABLE public."UserPJ" ADD COLUMN if not exists "stContato" character varying(200);
ALTER TABLE public."UserPJ" ADD COLUMN if not exists "stBanco" character varying(20);
ALTER TABLE public."UserPJ" ADD COLUMN if not exists "stAgencia" character varying(20);
ALTER TABLE public."UserPJ" ADD COLUMN if not exists "stConta" character varying(20);
ALTER TABLE public."UserPJ" ADD COLUMN if not exists "stContrato" character varying(200);

CREATE INDEX IF NOT EXISTS idx_userpj ON public."UserPJ" ("fkCompany", "fkUser");

CREATE TABLE IF NOT EXISTS public."UserFilePJ" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);

ALTER TABLE public."UserFilePJ" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."UserFilePJ" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."UserFilePJ" ADD COLUMN if not exists "stFile" character varying(500);
ALTER TABLE public."UserFilePJ" ADD COLUMN if not exists "stDescription" character varying(900);
ALTER TABLE public."UserFilePJ" ADD COLUMN if not exists "dtLog" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_userfilepj ON public."UserFilePJ" ("fkCompany", "fkUser", "stFile");

CREATE TABLE IF NOT EXISTS public."UserFileCLT" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);

ALTER TABLE public."UserFileCLT" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."UserFileCLT" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."UserFileCLT" ADD COLUMN if not exists "stFile" character varying(500);
ALTER TABLE public."UserFileCLT" ADD COLUMN if not exists "stDescription" character varying(900);
ALTER TABLE public."UserFileCLT" ADD COLUMN if not exists "dtLog" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_userfileclt ON public."UserFileCLT" ("fkCompany", "fkUser", "stFile");

CREATE TABLE IF NOT EXISTS public."UserContractCLT" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);

ALTER TABLE public."UserContractCLT" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."UserContractCLT" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."UserContractCLT" ADD COLUMN if not exists "stRole" character varying(500);
ALTER TABLE public."UserContractCLT" ADD COLUMN if not exists "vrBasePayment" int;
ALTER TABLE public."UserContractCLT" ADD COLUMN if not exists "vrHourlyPayment" int;
ALTER TABLE public."UserContractCLT" ADD COLUMN if not exists "dtStart" timestamp without time zone;
ALTER TABLE public."UserContractCLT" ADD COLUMN if not exists "dtEnd" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_usercontractclt ON public."UserContractCLT" ("fkCompany", "fkUser");

CREATE TABLE IF NOT EXISTS public."UserContractPJ" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);

ALTER TABLE public."UserContractPJ" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."UserContractPJ" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."UserContractPJ" ADD COLUMN if not exists "stRole" character varying(500);
ALTER TABLE public."UserContractPJ" ADD COLUMN if not exists "vrBasePayment" int;
ALTER TABLE public."UserContractPJ" ADD COLUMN if not exists "vrHourlyPayment" int;
ALTER TABLE public."UserContractPJ" ADD COLUMN if not exists "dtStart" timestamp without time zone;
ALTER TABLE public."UserContractPJ" ADD COLUMN if not exists "dtEnd" timestamp without time zone;
ALTER TABLE public."UserContractPJ" ADD COLUMN if not exists "nuMaxHoursWeek" int;

CREATE INDEX IF NOT EXISTS idx_usercontractpj ON public."UserContractPJ" ("fkCompany", "fkUser");

CREATE TABLE IF NOT EXISTS public."UserHealthCertificate" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);

ALTER TABLE public."UserHealthCertificate" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."UserHealthCertificate" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."UserHealthCertificate" ADD COLUMN if not exists "stDescription" character varying(500);
ALTER TABLE public."UserHealthCertificate" ADD COLUMN if not exists "stFile" character varying(500);
ALTER TABLE public."UserHealthCertificate" ADD COLUMN if not exists "dtStart" timestamp without time zone;
ALTER TABLE public."UserHealthCertificate" ADD COLUMN if not exists "dtEnd" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_userhealth ON public."UserHealthCertificate" ("fkCompany", "fkUser");

CREATE TABLE IF NOT EXISTS public."UserResource" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);

ALTER TABLE public."UserResource" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."UserResource" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."UserResource" ADD COLUMN if not exists "stDescription" character varying(500);
ALTER TABLE public."UserResource" ADD COLUMN if not exists "dtStart" timestamp without time zone;
ALTER TABLE public."UserResource" ADD COLUMN if not exists "dtEnd" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_userresource ON public."UserResource" ("fkCompany", "fkUser");

CREATE TABLE IF NOT EXISTS public."UserVacationRequest" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);

ALTER TABLE public."UserVacationRequest" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."UserVacationRequest" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."UserVacationRequest" ADD COLUMN if not exists "fkUserResp" int;
ALTER TABLE public."UserVacationRequest" ADD COLUMN if not exists "nuState" int;
ALTER TABLE public."UserVacationRequest" ADD COLUMN if not exists "dtStart" timestamp without time zone;
ALTER TABLE public."UserVacationRequest" ADD COLUMN if not exists "dtEnd" timestamp without time zone;
ALTER TABLE public."UserVacationRequest" ADD COLUMN if not exists "stAnswer" character varying(500);

CREATE INDEX IF NOT EXISTS idx_uservacationrequest ON public."UserVacationRequest" ("fkCompany", "fkUser");

CREATE TABLE IF NOT EXISTS public."UserVacation" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);

ALTER TABLE public."UserVacation" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."UserVacation" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."UserVacation" ADD COLUMN if not exists "dtStart" timestamp without time zone;
ALTER TABLE public."UserVacation" ADD COLUMN if not exists "dtEnd" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_uservacation ON public."UserVacation" ("fkCompany", "fkUser");

CREATE TABLE IF NOT EXISTS public."UserVacationRequestComment" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);

ALTER TABLE public."UserVacationRequestComment" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."UserVacationRequestComment" ADD COLUMN if not exists "fkUserVacationRequest" int;
ALTER TABLE public."UserVacationRequestComment" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."UserVacationRequestComment" ADD COLUMN if not exists "stAnswer" character varying(500);
ALTER TABLE public."UserVacationRequestComment" ADD COLUMN if not exists "dtLog" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_uservacationreqcomment ON public."UserVacationRequestComment" ("fkCompany", "fkUserVacationRequest");

CREATE TABLE IF NOT EXISTS public."Customer" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);

ALTER TABLE public."Customer" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."Customer" ADD COLUMN if not exists "fkUserResp" int;
ALTER TABLE public."Customer" ADD COLUMN if not exists "stCode" character varying(10);
ALTER TABLE public."Customer" ADD COLUMN if not exists "stName" character varying(200);
ALTER TABLE public."Customer" ADD COLUMN if not exists "stCnpj" character varying(20);
ALTER TABLE public."Customer" ADD COLUMN if not exists "stCountry" character varying(200);
ALTER TABLE public."Customer" ADD COLUMN if not exists "stRegion" character varying(200);
ALTER TABLE public."Customer" ADD COLUMN if not exists "stCity" character varying(200);
ALTER TABLE public."Customer" ADD COLUMN if not exists "stAddress" character varying(500);
ALTER TABLE public."Customer" ADD COLUMN if not exists "stAddressNumber" character varying(500);
ALTER TABLE public."Customer" ADD COLUMN if not exists "stAddressCompl" character varying(500);
ALTER TABLE public."Customer" ADD COLUMN if not exists "stAddressZipCode" character varying(500);
ALTER TABLE public."Customer" ADD COLUMN if not exists "stPhone" character varying(20);
ALTER TABLE public."Customer" ADD COLUMN if not exists "bActive" boolean;
ALTER TABLE public."Customer" ADD COLUMN if not exists "dtCreationDate" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_customer ON public."Customer" ("fkCompany","stName","stCode","bActive");

CREATE TABLE IF NOT EXISTS public."Project" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);

ALTER TABLE public."Project" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."Project" ADD COLUMN if not exists "fkCustomer" int;
ALTER TABLE public."Project" ADD COLUMN if not exists "stCode" character varying(10);
ALTER TABLE public."Project" ADD COLUMN if not exists "stName" character varying(200);
ALTER TABLE public."Project" ADD COLUMN if not exists "bActive" boolean;
ALTER TABLE public."Project" ADD COLUMN if not exists "dtCreationDate" timestamp without time zone;
ALTER TABLE public."Project" ADD COLUMN if not exists "stCountry" character varying(200);
ALTER TABLE public."Project" ADD COLUMN if not exists "stRegion" character varying(200);
ALTER TABLE public."Project" ADD COLUMN if not exists "stCity" character varying(200);

CREATE INDEX IF NOT EXISTS idx_project ON public."Project" ("fkCompany","stName","stCode","bActive");

CREATE TABLE IF NOT EXISTS public."Prospect" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);

ALTER TABLE public."Prospect" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."Prospect" ADD COLUMN if not exists "stCode" character varying(10);
ALTER TABLE public."Prospect" ADD COLUMN if not exists "stName" character varying(200);
ALTER TABLE public."Prospect" ADD COLUMN if not exists "stCnpj" character varying(20);
ALTER TABLE public."Prospect" ADD COLUMN if not exists "stCountry" character varying(200);
ALTER TABLE public."Prospect" ADD COLUMN if not exists "stRegion" character varying(200);
ALTER TABLE public."Prospect" ADD COLUMN if not exists "stCity" character varying(200);
ALTER TABLE public."Prospect" ADD COLUMN if not exists "stAddress" character varying(500);
ALTER TABLE public."Prospect" ADD COLUMN if not exists "stAddressNumber" character varying(500);
ALTER TABLE public."Prospect" ADD COLUMN if not exists "stAddressCompl" character varying(500);
ALTER TABLE public."Prospect" ADD COLUMN if not exists "stAddressZipCode" character varying(500);
ALTER TABLE public."Prospect" ADD COLUMN if not exists "stPhone" character varying(20);
ALTER TABLE public."Prospect" ADD COLUMN if not exists "bActive" boolean;
ALTER TABLE public."Prospect" ADD COLUMN if not exists "dtCreationDate" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_prospect ON public."Prospect" ("fkCompany","stName","stCode","bActive");

CREATE TABLE IF NOT EXISTS public."Supplier" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);

ALTER TABLE public."Supplier" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."Supplier" ADD COLUMN if not exists "stCode" character varying(10);
ALTER TABLE public."Supplier" ADD COLUMN if not exists "stName" character varying(200);
ALTER TABLE public."Supplier" ADD COLUMN if not exists "stCnpj" character varying(20);
ALTER TABLE public."Supplier" ADD COLUMN if not exists "stCountry" character varying(200);
ALTER TABLE public."Supplier" ADD COLUMN if not exists "stRegion" character varying(200);
ALTER TABLE public."Supplier" ADD COLUMN if not exists "stCity" character varying(200);
ALTER TABLE public."Supplier" ADD COLUMN if not exists "stAddress" character varying(500);
ALTER TABLE public."Supplier" ADD COLUMN if not exists "stAddressNumber" character varying(500);
ALTER TABLE public."Supplier" ADD COLUMN if not exists "stAddressCompl" character varying(500);
ALTER TABLE public."Supplier" ADD COLUMN if not exists "stAddressZipCode" character varying(500);
ALTER TABLE public."Supplier" ADD COLUMN if not exists "stPhone" character varying(20);
ALTER TABLE public."Supplier" ADD COLUMN if not exists "bActive" boolean;
ALTER TABLE public."Supplier" ADD COLUMN if not exists "dtCreationDate" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_supplier ON public."Supplier" ("fkCompany","stName","stCode","bActive");

CREATE TABLE IF NOT EXISTS public."Contact" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);

ALTER TABLE public."Contact" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."Contact" ADD COLUMN if not exists "stName" character varying(200);
ALTER TABLE public."Contact" ADD COLUMN if not exists "stCpf" character varying(20);
ALTER TABLE public."Contact" ADD COLUMN if not exists "stCountry" character varying(200);
ALTER TABLE public."Contact" ADD COLUMN if not exists "stRegion" character varying(200);
ALTER TABLE public."Contact" ADD COLUMN if not exists "stCity" character varying(200);
ALTER TABLE public."Contact" ADD COLUMN if not exists "stAddress" character varying(500);
ALTER TABLE public."Contact" ADD COLUMN if not exists "stAddressNumber" character varying(500);
ALTER TABLE public."Contact" ADD COLUMN if not exists "stAddressCompl" character varying(500);
ALTER TABLE public."Contact" ADD COLUMN if not exists "stAddressZipCode" character varying(500);
ALTER TABLE public."Contact" ADD COLUMN if not exists "stPhone" character varying(20);
ALTER TABLE public."Contact" ADD COLUMN if not exists "bActive" boolean;
ALTER TABLE public."Contact" ADD COLUMN if not exists "dtCreationDate" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_contact ON public."Contact" ("fkCompany","stName","bActive");

CREATE TABLE IF NOT EXISTS public."TypeCustomerInteraction" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);

ALTER TABLE public."TypeCustomerInteraction" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."TypeCustomerInteraction" ADD COLUMN if not exists "stName" character varying(200);

CREATE INDEX IF NOT EXISTS idx_typecustomerinteraction ON public."TypeCustomerInteraction" ("fkCompany","stName");

CREATE TABLE IF NOT EXISTS public."CustomerInteraction" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);

ALTER TABLE public."CustomerInteraction" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."CustomerInteraction" ADD COLUMN if not exists "fkCustomer" int;
ALTER TABLE public."CustomerInteraction" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."CustomerInteraction" ADD COLUMN if not exists "fkTypeCustomerInteraction" int;
ALTER TABLE public."CustomerInteraction" ADD COLUMN if not exists "stDescription" character varying(2000);
ALTER TABLE public."CustomerInteraction" ADD COLUMN if not exists "dtLog" timestamp without time zone;

CREATE INDEX IF NOT EXISTS idx_customerinteraction ON public."CustomerInteraction" ("fkCompany","fkCustomer","fkUser");

CREATE TABLE IF NOT EXISTS public."BuffonBasePadrao" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."BuffonBasePadrao" ADD COLUMN if not exists "fkBuffonDistribuidora" int;
ALTER TABLE public."BuffonBasePadrao" ADD COLUMN if not exists "id_scrape" int;
ALTER TABLE public."BuffonBasePadrao" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."BuffonBasePadrao" ADD COLUMN if not exists "stName" character varying(200);
ALTER TABLE public."BuffonBasePadrao" ADD COLUMN if not exists "stCidade" character varying(300);
ALTER TABLE public."BuffonBasePadrao" ADD COLUMN if not exists "stEstado" character varying(300);
ALTER TABLE public."BuffonBasePadrao" ADD COLUMN if not exists "stCnpj" character varying(20);

CREATE INDEX IF NOT EXISTS idx_BuffonBasePadrao ON public."BuffonBasePadrao" ("stName");

CREATE TABLE IF NOT EXISTS public."BuffonDistribuidora" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."BuffonDistribuidora" ADD COLUMN if not exists "id_scrape" int;
ALTER TABLE public."BuffonDistribuidora" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."BuffonDistribuidora" ADD COLUMN if not exists "stName" character varying(200);
ALTER TABLE public."BuffonDistribuidora" ADD COLUMN if not exists "bOutrasDist" boolean;
ALTER TABLE public."BuffonDistribuidora" ADD COLUMN if not exists "fkDistSegmento" int;
ALTER TABLE public."BuffonDistribuidora" ADD COLUMN if not exists "stAssessores" character varying(900);
ALTER TABLE public."BuffonDistribuidora" ADD COLUMN if not exists "stTelefones" character varying(900);
ALTER TABLE public."BuffonDistribuidora" ADD COLUMN if not exists "stEmails" character varying(900);

CREATE INDEX IF NOT EXISTS idx_BuffonDistribuidora ON public."BuffonDistribuidora" ("stName");

CREATE TABLE IF NOT EXISTS public."BuffonPosto" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "nuFilial" int;
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "stName" character varying(300);
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "stCidade" character varying(300);
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "stEstado" character varying(300);
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "stCnpj" character varying(20);
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "stCodigo" character varying(20);
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "stAddress" character varying(300);
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "stAddressNumber" character varying(20);
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "stAddressCompl" character varying(20);
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "stZipCode" character varying(20);
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "bPostoBB" boolean;
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "stGerente" character varying(200);
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "stTelefone" character varying(20);
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "stSupervisor" character varying(200);
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "nuLitros_etanol" int;
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "nuLitros_etanola" int;
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "nuLitros_ga" int;
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "nuLitros_gc" int;
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "nuLitros_gp" int;
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "nuLitros_ds10" int;
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "nuLitros_ds500" int;
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "nuLitros_ds10a" int;
ALTER TABLE public."BuffonPosto" ADD COLUMN if not exists "nuLitros_ds500a" int;

CREATE INDEX IF NOT EXISTS idx_BuffonPostoLocal ON public."BuffonPosto" ("stCidade", "stEstado");
CREATE INDEX IF NOT EXISTS idx_BuffonPostoCnpj ON public."BuffonPosto" ("stCnpj" );
CREATE INDEX IF NOT EXISTS idx_BuffonPostoCodigo ON public."BuffonPosto" ("stCodigo");

CREATE TABLE IF NOT EXISTS public."BuffonTipoCombustivel" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."BuffonTipoCombustivel" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."BuffonTipoCombustivel" ADD COLUMN if not exists "id_scrape" int;
ALTER TABLE public."BuffonTipoCombustivel" ADD COLUMN if not exists "stName" character varying(500);
ALTER TABLE public."BuffonTipoCombustivel" ADD COLUMN if not exists "stDesc" character varying(2000);
ALTER TABLE public."BuffonTipoCombustivel" ADD COLUMN if not exists "nuOrder" int;
ALTER TABLE public."BuffonTipoCombustivel" ADD COLUMN if not exists "fkCategoria" int;

CREATE INDEX IF NOT EXISTS idx_BuffonTipoCombustivel ON public."BuffonTipoCombustivel" ("stName");

CREATE TABLE IF NOT EXISTS public."BuffonCombustivel" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."BuffonCombustivel" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."BuffonCombustivel" ADD COLUMN if not exists "id_scrape" int;
ALTER TABLE public."BuffonCombustivel" ADD COLUMN if not exists "fkTipoCombustivel" int;
ALTER TABLE public."BuffonCombustivel" ADD COLUMN if not exists "fkDistribuidora" int;
ALTER TABLE public."BuffonCombustivel" ADD COLUMN if not exists "stName" character varying(500);

CREATE INDEX IF NOT EXISTS idx_BuffonCombustivel ON public."BuffonCombustivel" ("stName");

CREATE TABLE IF NOT EXISTS public."BuffonPostoBaseDist" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."BuffonPostoBaseDist" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."BuffonPostoBaseDist" ADD COLUMN if not exists "fkBuffonPosto" int;
ALTER TABLE public."BuffonPostoBaseDist" ADD COLUMN if not exists "id_scrape_posto" int;
ALTER TABLE public."BuffonPostoBaseDist" ADD COLUMN if not exists "id_scrape_dist" int;
ALTER TABLE public."BuffonPostoBaseDist" ADD COLUMN if not exists "fkBuffonDistribuidora" int;
ALTER TABLE public."BuffonPostoBaseDist" ADD COLUMN if not exists "stCodigo" character varying(500);
ALTER TABLE public."BuffonPostoBaseDist" ADD COLUMN if not exists "stLocal" character varying(500);
ALTER TABLE public."BuffonPostoBaseDist" ADD COLUMN if not exists "vrFrete" numeric(10,5);

CREATE TABLE IF NOT EXISTS public."BuffonPriceChange" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."BuffonPriceChange" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."BuffonPriceChange" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."BuffonPriceChange" ADD COLUMN if not exists "fkBuffonDist" int;
ALTER TABLE public."BuffonPriceChange" ADD COLUMN if not exists "fkBuffonBase" int;
ALTER TABLE public."BuffonPriceChange" ADD COLUMN if not exists "fkBuffonPosto" int;
ALTER TABLE public."BuffonPriceChange" ADD COLUMN if not exists "fkBuffonComb" int;
ALTER TABLE public."BuffonPriceChange" ADD COLUMN if not exists "fkBuffonTipoComb" int;
ALTER TABLE public."BuffonPriceChange" ADD COLUMN if not exists "id_scrape_price" int;
ALTER TABLE public."BuffonPriceChange" ADD COLUMN if not exists "dtLog" timestamp without time zone;
ALTER TABLE public."BuffonPriceChange" ADD COLUMN if not exists "nuDay" int;
ALTER TABLE public."BuffonPriceChange" ADD COLUMN if not exists "stVlrOrig" character varying(20);
ALTER TABLE public."BuffonPriceChange" ADD COLUMN if not exists "stVlrNew" character varying(20);

CREATE TABLE IF NOT EXISTS public."BuffonConfig" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."BuffonConfig" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."BuffonConfig" ADD COLUMN if not exists "stCmdScrape" character varying(500);
ALTER TABLE public."BuffonConfig" ADD COLUMN if not exists "bReprocScrape" boolean;
ALTER TABLE public."BuffonConfig" ADD COLUMN if not exists "dtReprocStart" timestamp without time zone;

INSERT INTO public."BuffonConfig" ("bReprocScrape") SELECT FALSE WHERE NOT EXISTS (SELECT 1 FROM public."BuffonConfig" WHERE "bReprocScrape" = FALSE);

CREATE TABLE IF NOT EXISTS public."BuffonDistPrice" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."BuffonDistPrice" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."BuffonDistPrice" ADD COLUMN if not exists "dtLog" timestamp without time zone;
ALTER TABLE public."BuffonDistPrice" ADD COLUMN if not exists "dtDay" date;
ALTER TABLE public."BuffonDistPrice" ADD COLUMN if not exists "fkBuffonTipoComb" int;
ALTER TABLE public."BuffonDistPrice" ADD COLUMN if not exists "vrPrice" numeric(10,5);
ALTER TABLE public."BuffonDistPrice" ADD COLUMN if not exists "stLocal" character varying(500);
ALTER TABLE public."BuffonDistPrice" ADD COLUMN if not exists "stDist" character varying(500);

CREATE TABLE IF NOT EXISTS public."BuffonDistFixedPrice" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."BuffonDistFixedPrice" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."BuffonDistFixedPrice" ADD COLUMN if not exists "dtLog" timestamp without time zone;
ALTER TABLE public."BuffonDistFixedPrice" ADD COLUMN if not exists "fkUser" int;
ALTER TABLE public."BuffonDistFixedPrice" ADD COLUMN if not exists "dtDayStart" timestamp without time zone;
ALTER TABLE public."BuffonDistFixedPrice" ADD COLUMN if not exists "dtDayFinish" timestamp without time zone;
ALTER TABLE public."BuffonDistFixedPrice" ADD COLUMN if not exists "fkBuffonDist" int;
ALTER TABLE public."BuffonDistFixedPrice" ADD COLUMN if not exists "fkBuffonBase" int;
ALTER TABLE public."BuffonDistFixedPrice" ADD COLUMN if not exists "fkBuffonTipoComb" int;
ALTER TABLE public."BuffonDistFixedPrice" ADD COLUMN if not exists "vrPrice" numeric(10,5);

CREATE TABLE IF NOT EXISTS public."BuffonDistPostoComb" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "fk_id_scrape_posto" int;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "fk_id_scrape_base" int;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "fk_id_scrape_dist" int;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "bEtanol" boolean;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "bEtanolAditivado" boolean;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "bGasComum" boolean;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "bGasAdit" boolean;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "bDieselS500" boolean;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "bDieselS10" boolean;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "bDieselS10Adit" boolean;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "bGasolinaPremium" boolean;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "bGNV" boolean;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "bQuerosene" boolean;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "bDieselS500Aditivado" boolean;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "bOcB1BPF" boolean;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "bOcP1BTE" boolean;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "bOdBS10" boolean;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "bOdBS10Adt" boolean;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "bOdBS50Inv" boolean;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "bOdBS500" boolean;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "bOdBS500Inv" boolean;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "bOdBS500Adt" boolean;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "bXisto" boolean;
ALTER TABLE public."BuffonDistPostoComb" ADD COLUMN if not exists "bDieselMar" boolean;

CREATE TABLE IF NOT EXISTS public."BuffonFrete" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."BuffonFrete" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."BuffonFrete" ADD COLUMN if not exists "id_scrape_dist" int;
ALTER TABLE public."BuffonFrete" ADD COLUMN if not exists "id_scrape_base" int;
ALTER TABLE public."BuffonFrete" ADD COLUMN if not exists "id_scrape_posto" int;
ALTER TABLE public."BuffonFrete" ADD COLUMN if not exists "nuKM" int;
ALTER TABLE public."BuffonFrete" ADD COLUMN if not exists "vrPrice" numeric(10,5);

CREATE TABLE IF NOT EXISTS public."BuffonRegional" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."BuffonRegional" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."BuffonRegional" ADD COLUMN if not exists "stLocal" character varying(200);

CREATE TABLE IF NOT EXISTS public."BuffonPostoBomba" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."BuffonPostoBomba" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."BuffonPostoBomba" ADD COLUMN if not exists "fkBuffonTipoCombustivel" int; -- gas comum
ALTER TABLE public."BuffonPostoBomba" ADD COLUMN if not exists "nuFilial" int;  -- 32
ALTER TABLE public."BuffonPostoBomba" ADD COLUMN if not exists "nuMaxLitros" int; -- 30000
ALTER TABLE public."BuffonPostoBomba" ADD COLUMN if not exists "nuCurLitros" int; -- 12000
ALTER TABLE public."BuffonPostoBomba" ADD COLUMN if not exists "nuVendasDia" int; -- 120
ALTER TABLE public."BuffonPostoBomba" ADD COLUMN if not exists "stSerialId" character varying(200); -- numero de série
ALTER TABLE public."BuffonPostoBomba" ADD COLUMN if not exists "stInfo" character varying(200); -- informações extras

CREATE TABLE IF NOT EXISTS public."BuffonFatMonth" ( id bigserial NOT NULL, PRIMARY KEY (id)) WITH (OIDS = FALSE);
ALTER TABLE public."BuffonFatMonth" ADD COLUMN if not exists "fkCompany" int;
ALTER TABLE public."BuffonFatMonth" ADD COLUMN if not exists "stTipoCombustivel" character varying(200); 
ALTER TABLE public."BuffonFatMonth" ADD COLUMN if not exists "nuFilial" int;
ALTER TABLE public."BuffonFatMonth" ADD COLUMN if not exists "nuLitros" int;
ALTER TABLE public."BuffonFatMonth" ADD COLUMN if not exists "nuDay" int; 
ALTER TABLE public."BuffonFatMonth" ADD COLUMN if not exists "nuMonth" int;
ALTER TABLE public."BuffonFatMonth" ADD COLUMN if not exists "nuYear" int;
ALTER TABLE public."BuffonFatMonth" ADD COLUMN if not exists "stD1" character varying(20); 
ALTER TABLE public."BuffonFatMonth" ADD COLUMN if not exists "stD2" character varying(20); 
ALTER TABLE public."BuffonFatMonth" ADD COLUMN if not exists "stV0" character varying(20); 
ALTER TABLE public."BuffonFatMonth" ADD COLUMN if not exists "stV1" character varying(20); 
ALTER TABLE public."BuffonFatMonth" ADD COLUMN if not exists "stV2" character varying(20); 
