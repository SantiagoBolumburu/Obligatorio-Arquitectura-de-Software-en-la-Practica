--
-- PostgreSQL database dump
--

-- Dumped from database version 15.2
-- Dumped by pg_dump version 15.2

-- Started on 2023-06-29 07:26:13

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
-- TOC entry 3349 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: pg_database_owner
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 216 (class 1259 OID 16940)
-- Name: CompraVentaSubscriptions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."CompraVentaSubscriptions" (
    "Id" uuid NOT NULL,
    "UsuarioId" uuid NOT NULL,
    "Email" text NOT NULL,
    "ProductoId" uuid NOT NULL
);


ALTER TABLE public."CompraVentaSubscriptions" OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 16760)
-- Name: Productos; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Productos" (
    "Id" uuid NOT NULL,
    "EmpresaId" uuid NOT NULL,
    "ProductoMainId" uuid DEFAULT '00000000-0000-0000-0000-000000000000'::uuid NOT NULL
);


ALTER TABLE public."Productos" OWNER TO postgres;

--
-- TOC entry 217 (class 1259 OID 16952)
-- Name: StockSubscriptions; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."StockSubscriptions" (
    "Id" uuid NOT NULL,
    "UsuarioId" uuid NOT NULL,
    "Email" text NOT NULL,
    "ProductoId" uuid NOT NULL
);


ALTER TABLE public."StockSubscriptions" OWNER TO postgres;

--
-- TOC entry 214 (class 1259 OID 16755)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- TOC entry 3342 (class 0 OID 16940)
-- Dependencies: 216
-- Data for Name: CompraVentaSubscriptions; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3341 (class 0 OID 16760)
-- Dependencies: 215
-- Data for Name: Productos; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3343 (class 0 OID 16952)
-- Dependencies: 217
-- Data for Name: StockSubscriptions; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3340 (class 0 OID 16755)
-- Dependencies: 214
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."__EFMigrationsHistory" VALUES ('20230606060439_Migracion001-MigracionInicial', '7.0.5');
INSERT INTO public."__EFMigrationsHistory" VALUES ('20230616054135_Migracion002-Reice-el-Dominio', '7.0.5');
INSERT INTO public."__EFMigrationsHistory" VALUES ('20230616063322_Migracion003-Agregue-Restricciones', '7.0.5');
INSERT INTO public."__EFMigrationsHistory" VALUES ('20230618062259_Migracion004-ProductoMainId-is-unique-now', '7.0.5');


--
-- TOC entry 3192 (class 2606 OID 16946)
-- Name: CompraVentaSubscriptions PK_CompraVentaSubscriptions; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."CompraVentaSubscriptions"
    ADD CONSTRAINT "PK_CompraVentaSubscriptions" PRIMARY KEY ("Id");


--
-- TOC entry 3189 (class 2606 OID 16766)
-- Name: Productos PK_Productos; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Productos"
    ADD CONSTRAINT "PK_Productos" PRIMARY KEY ("Id");


--
-- TOC entry 3195 (class 2606 OID 16958)
-- Name: StockSubscriptions PK_StockSubscriptions; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."StockSubscriptions"
    ADD CONSTRAINT "PK_StockSubscriptions" PRIMARY KEY ("Id");


--
-- TOC entry 3186 (class 2606 OID 16759)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- TOC entry 3190 (class 1259 OID 16967)
-- Name: IX_CompraVentaSubscriptions_ProductoId_UsuarioId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_CompraVentaSubscriptions_ProductoId_UsuarioId" ON public."CompraVentaSubscriptions" USING btree ("ProductoId", "UsuarioId");


--
-- TOC entry 3187 (class 1259 OID 16968)
-- Name: IX_Productos_ProductoMainId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_Productos_ProductoMainId" ON public."Productos" USING btree ("ProductoMainId");


--
-- TOC entry 3193 (class 1259 OID 16966)
-- Name: IX_StockSubscriptions_ProductoId_UsuarioId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_StockSubscriptions_ProductoId_UsuarioId" ON public."StockSubscriptions" USING btree ("ProductoId", "UsuarioId");


--
-- TOC entry 3196 (class 2606 OID 16947)
-- Name: CompraVentaSubscriptions FK_CompraVentaSubscriptions_Productos_ProductoId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."CompraVentaSubscriptions"
    ADD CONSTRAINT "FK_CompraVentaSubscriptions_Productos_ProductoId" FOREIGN KEY ("ProductoId") REFERENCES public."Productos"("Id") ON DELETE CASCADE;


--
-- TOC entry 3197 (class 2606 OID 16959)
-- Name: StockSubscriptions FK_StockSubscriptions_Productos_ProductoId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."StockSubscriptions"
    ADD CONSTRAINT "FK_StockSubscriptions_Productos_ProductoId" FOREIGN KEY ("ProductoId") REFERENCES public."Productos"("Id") ON DELETE CASCADE;


-- Completed on 2023-06-29 07:26:13

--
-- PostgreSQL database dump complete
--

