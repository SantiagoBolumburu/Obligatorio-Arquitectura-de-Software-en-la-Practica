--
-- PostgreSQL database dump
--

-- Dumped from database version 15.2
-- Dumped by pg_dump version 15.2

-- Started on 2023-06-29 07:15:56

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 4 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: pg_database_owner
--

CREATE SCHEMA public;


ALTER SCHEMA public OWNER TO pg_database_owner;

--
-- TOC entry 3359 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: pg_database_owner
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 218 (class 1259 OID 16835)
-- Name: ApplicationKeys; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."ApplicationKeys" (
    "Id" uuid NOT NULL,
    "SessionId" uuid NOT NULL,
    "EmpresaId" uuid NOT NULL,
    "UsuarioId" uuid NOT NULL,
    "Activa" boolean NOT NULL,
    "FechaCreacion" timestamp without time zone NOT NULL,
    "ApplicationKeyStr" text NOT NULL
);


ALTER TABLE public."ApplicationKeys" OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 16797)
-- Name: Empresas; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Empresas" (
    "Id" uuid NOT NULL,
    "Nombre" text NOT NULL
);


ALTER TABLE public."Empresas" OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 16804)
-- Name: Invitaciones; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Invitaciones" (
    "Id" uuid NOT NULL,
    "Email" text NOT NULL,
    "Rol" text NOT NULL,
    "Utilizada" boolean NOT NULL,
    "FechaVencimiento" timestamp without time zone NOT NULL,
    "EmpresaId" uuid NOT NULL,
    "InvitadorId" uuid DEFAULT '00000000-0000-0000-0000-000000000000'::uuid NOT NULL
);


ALTER TABLE public."Invitaciones" OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 16816)
-- Name: Usuarios; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Usuarios" (
    "Id" uuid NOT NULL,
    "Nombre" text NOT NULL,
    "Email" text NOT NULL,
    "Contrasenia" text NOT NULL,
    "Rol" text NOT NULL,
    "EmpresaId" uuid NOT NULL
);


ALTER TABLE public."Usuarios" OWNER TO postgres;

--
-- TOC entry 214 (class 1259 OID 16792)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- TOC entry 3353 (class 0 OID 16835)
-- Dependencies: 218
-- Data for Name: ApplicationKeys; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3350 (class 0 OID 16797)
-- Dependencies: 215
-- Data for Name: Empresas; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Empresas" VALUES ('f775de6f-dfdf-4aa6-8aa1-b19460a240a8', 'ORT');


--
-- TOC entry 3351 (class 0 OID 16804)
-- Dependencies: 216
-- Data for Name: Invitaciones; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3352 (class 0 OID 16816)
-- Dependencies: 217
-- Data for Name: Usuarios; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."Usuarios" VALUES ('2b91fc87-3c6e-40df-bb5e-2de4930ecda3', 'empleado', 'empleado@ort.com', 'Password1', 'empleado', 'f775de6f-dfdf-4aa6-8aa1-b19460a240a8');
INSERT INTO public."Usuarios" VALUES ('5f929c00-8ceb-4fbb-8ecb-90840dd30cc4', 'admin', 'admin@ort.com', 'Password1', 'administrador', 'f775de6f-dfdf-4aa6-8aa1-b19460a240a8');


--
-- TOC entry 3349 (class 0 OID 16792)
-- Dependencies: 214
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."__EFMigrationsHistory" VALUES ('20230606193008_Migracion001-MigracionInicial', '7.0.5');
INSERT INTO public."__EFMigrationsHistory" VALUES ('20230612055714_Migracion002-Agregue-ApplicationKey', '7.0.5');


--
-- TOC entry 3203 (class 2606 OID 16841)
-- Name: ApplicationKeys PK_ApplicationKeys; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."ApplicationKeys"
    ADD CONSTRAINT "PK_ApplicationKeys" PRIMARY KEY ("Id");


--
-- TOC entry 3193 (class 2606 OID 16803)
-- Name: Empresas PK_Empresas; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Empresas"
    ADD CONSTRAINT "PK_Empresas" PRIMARY KEY ("Id");


--
-- TOC entry 3197 (class 2606 OID 16810)
-- Name: Invitaciones PK_Invitaciones; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Invitaciones"
    ADD CONSTRAINT "PK_Invitaciones" PRIMARY KEY ("Id");


--
-- TOC entry 3201 (class 2606 OID 16822)
-- Name: Usuarios PK_Usuarios; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Usuarios"
    ADD CONSTRAINT "PK_Usuarios" PRIMARY KEY ("Id");


--
-- TOC entry 3190 (class 2606 OID 16796)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- TOC entry 3191 (class 1259 OID 16828)
-- Name: IX_Empresas_Nombre; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_Empresas_Nombre" ON public."Empresas" USING btree ("Nombre");


--
-- TOC entry 3194 (class 1259 OID 16829)
-- Name: IX_Invitaciones_EmpresaId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Invitaciones_EmpresaId" ON public."Invitaciones" USING btree ("EmpresaId");


--
-- TOC entry 3195 (class 1259 OID 16842)
-- Name: IX_Invitaciones_InvitadorId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Invitaciones_InvitadorId" ON public."Invitaciones" USING btree ("InvitadorId");


--
-- TOC entry 3198 (class 1259 OID 16830)
-- Name: IX_Usuarios_Email; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_Usuarios_Email" ON public."Usuarios" USING btree ("Email");


--
-- TOC entry 3199 (class 1259 OID 16831)
-- Name: IX_Usuarios_EmpresaId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Usuarios_EmpresaId" ON public."Usuarios" USING btree ("EmpresaId");


--
-- TOC entry 3204 (class 2606 OID 16811)
-- Name: Invitaciones FK_Invitaciones_Empresas_EmpresaId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Invitaciones"
    ADD CONSTRAINT "FK_Invitaciones_Empresas_EmpresaId" FOREIGN KEY ("EmpresaId") REFERENCES public."Empresas"("Id") ON DELETE CASCADE;


--
-- TOC entry 3205 (class 2606 OID 16843)
-- Name: Invitaciones FK_Invitaciones_Usuarios_InvitadorId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Invitaciones"
    ADD CONSTRAINT "FK_Invitaciones_Usuarios_InvitadorId" FOREIGN KEY ("InvitadorId") REFERENCES public."Usuarios"("Id") ON DELETE CASCADE;


--
-- TOC entry 3206 (class 2606 OID 16823)
-- Name: Usuarios FK_Usuarios_Empresas_EmpresaId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Usuarios"
    ADD CONSTRAINT "FK_Usuarios_Empresas_EmpresaId" FOREIGN KEY ("EmpresaId") REFERENCES public."Empresas"("Id") ON DELETE CASCADE;


-- Completed on 2023-06-29 07:15:56

--
-- PostgreSQL database dump complete
--

