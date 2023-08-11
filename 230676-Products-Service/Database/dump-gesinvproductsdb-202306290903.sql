--
-- PostgreSQL database dump
--

-- Dumped from database version 15.2
-- Dumped by pg_dump version 15.2

-- Started on 2023-06-29 09:03:01

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
-- TOC entry 3380 (class 0 OID 0)
-- Dependencies: 4
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: pg_database_owner
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 217 (class 1259 OID 16870)
-- Name: Compras; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Compras" (
    "Id" uuid NOT NULL,
    "FechaCompra" timestamp without time zone NOT NULL,
    "CostoTotalEnPesos" numeric NOT NULL,
    "EmpresaId" uuid NOT NULL,
    "ProveedorId" uuid NOT NULL
);


ALTER TABLE public."Compras" OWNER TO postgres;

--
-- TOC entry 218 (class 1259 OID 16882)
-- Name: DetallesComprasProductos; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."DetallesComprasProductos" (
    "Id" uuid NOT NULL,
    "Cantidad" integer NOT NULL,
    "ProductoId" uuid NOT NULL,
    "CompraId" uuid NOT NULL,
    "StockDespuesDeCompra" integer DEFAULT 0 NOT NULL
);


ALTER TABLE public."DetallesComprasProductos" OWNER TO postgres;

--
-- TOC entry 220 (class 1259 OID 16909)
-- Name: DetallesVentasProductos; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."DetallesVentasProductos" (
    "Id" uuid NOT NULL,
    "Cantidad" integer NOT NULL,
    "ProductoId" uuid NOT NULL,
    "VentaId" uuid NOT NULL,
    "StockDespuesDeVenta" integer DEFAULT 0 NOT NULL
);


ALTER TABLE public."DetallesVentasProductos" OWNER TO postgres;

--
-- TOC entry 215 (class 1259 OID 16854)
-- Name: Productos; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Productos" (
    "Id" uuid NOT NULL,
    "Nombre" text NOT NULL,
    "Descripcion" text NOT NULL,
    "ImagenPath" text NOT NULL,
    "Precio" numeric NOT NULL,
    "CantidadEnInventarioInicial" integer NOT NULL,
    "Accesible" boolean NOT NULL,
    "CantidadVendida" integer NOT NULL,
    "CantidadComprada" integer NOT NULL,
    "EmpresaId" uuid NOT NULL
);


ALTER TABLE public."Productos" OWNER TO postgres;

--
-- TOC entry 216 (class 1259 OID 16862)
-- Name: Proveedores; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Proveedores" (
    "Id" uuid NOT NULL,
    "Nombre" text NOT NULL,
    "Direccion" text NOT NULL,
    "Email" text NOT NULL,
    "Telefono" text NOT NULL,
    "Accesible" boolean NOT NULL,
    "EmpresaId" uuid NOT NULL
);


ALTER TABLE public."Proveedores" OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 16902)
-- Name: Ventas; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Ventas" (
    "Id" uuid NOT NULL,
    "NombreCliente" text NOT NULL,
    "FechaVenta" timestamp without time zone NOT NULL,
    "MontoTotalEnPesos" numeric NOT NULL,
    "EmpresaId" uuid NOT NULL,
    "Programada" boolean DEFAULT false NOT NULL,
    "Realizada" boolean DEFAULT false NOT NULL
);


ALTER TABLE public."Ventas" OWNER TO postgres;

--
-- TOC entry 214 (class 1259 OID 16849)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- TOC entry 3371 (class 0 OID 16870)
-- Dependencies: 217
-- Data for Name: Compras; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3372 (class 0 OID 16882)
-- Dependencies: 218
-- Data for Name: DetallesComprasProductos; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3374 (class 0 OID 16909)
-- Dependencies: 220
-- Data for Name: DetallesVentasProductos; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3369 (class 0 OID 16854)
-- Dependencies: 215
-- Data for Name: Productos; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3370 (class 0 OID 16862)
-- Dependencies: 216
-- Data for Name: Proveedores; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3373 (class 0 OID 16902)
-- Dependencies: 219
-- Data for Name: Ventas; Type: TABLE DATA; Schema: public; Owner: postgres
--



--
-- TOC entry 3368 (class 0 OID 16849)
-- Dependencies: 214
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."__EFMigrationsHistory" VALUES ('20230613064146_Migracion001-Migracion-Inicial', '7.0.5');
INSERT INTO public."__EFMigrationsHistory" VALUES ('20230613074254_Migracion002-Agregue-Proveedores', '7.0.5');
INSERT INTO public."__EFMigrationsHistory" VALUES ('20230613172421_Migracion003-Agregue-Compras', '7.0.5');
INSERT INTO public."__EFMigrationsHistory" VALUES ('20230613222541_Migracion004-Agregue-Ventas', '7.0.5');
INSERT INTO public."__EFMigrationsHistory" VALUES ('20230614193456_Migracion005-Agregue-Ventas-Programadas', '7.0.5');
INSERT INTO public."__EFMigrationsHistory" VALUES ('20230629113515_Migracion006-Correccion-DetalleCompraProducto', '7.0.5');


--
-- TOC entry 3210 (class 2606 OID 16876)
-- Name: Compras PK_Compras; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Compras"
    ADD CONSTRAINT "PK_Compras" PRIMARY KEY ("Id");


--
-- TOC entry 3214 (class 2606 OID 16901)
-- Name: DetallesComprasProductos PK_DetallesComprasProductos; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."DetallesComprasProductos"
    ADD CONSTRAINT "PK_DetallesComprasProductos" PRIMARY KEY ("Id");


--
-- TOC entry 3220 (class 2606 OID 16913)
-- Name: DetallesVentasProductos PK_DetallesVentasProductos; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."DetallesVentasProductos"
    ADD CONSTRAINT "PK_DetallesVentasProductos" PRIMARY KEY ("Id");


--
-- TOC entry 3204 (class 2606 OID 16860)
-- Name: Productos PK_Productos; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Productos"
    ADD CONSTRAINT "PK_Productos" PRIMARY KEY ("Id");


--
-- TOC entry 3207 (class 2606 OID 16868)
-- Name: Proveedores PK_Proveedores; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Proveedores"
    ADD CONSTRAINT "PK_Proveedores" PRIMARY KEY ("Id");


--
-- TOC entry 3216 (class 2606 OID 16908)
-- Name: Ventas PK_Ventas; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Ventas"
    ADD CONSTRAINT "PK_Ventas" PRIMARY KEY ("Id");


--
-- TOC entry 3201 (class 2606 OID 16853)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- TOC entry 3208 (class 1259 OID 16897)
-- Name: IX_Compras_ProveedorId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_Compras_ProveedorId" ON public."Compras" USING btree ("ProveedorId");


--
-- TOC entry 3211 (class 1259 OID 16898)
-- Name: IX_DetallesComprasProductos_CompraId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_DetallesComprasProductos_CompraId" ON public."DetallesComprasProductos" USING btree ("CompraId");


--
-- TOC entry 3212 (class 1259 OID 16899)
-- Name: IX_DetallesComprasProductos_ProductoId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_DetallesComprasProductos_ProductoId" ON public."DetallesComprasProductos" USING btree ("ProductoId");


--
-- TOC entry 3217 (class 1259 OID 16924)
-- Name: IX_DetallesVentasProductos_ProductoId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_DetallesVentasProductos_ProductoId" ON public."DetallesVentasProductos" USING btree ("ProductoId");


--
-- TOC entry 3218 (class 1259 OID 16925)
-- Name: IX_DetallesVentasProductos_VentaId; Type: INDEX; Schema: public; Owner: postgres
--

CREATE INDEX "IX_DetallesVentasProductos_VentaId" ON public."DetallesVentasProductos" USING btree ("VentaId");


--
-- TOC entry 3202 (class 1259 OID 16861)
-- Name: IX_Productos_EmpresaId_Nombre; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_Productos_EmpresaId_Nombre" ON public."Productos" USING btree ("EmpresaId", "Nombre");


--
-- TOC entry 3205 (class 1259 OID 16869)
-- Name: IX_Proveedores_EmpresaId_Nombre; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_Proveedores_EmpresaId_Nombre" ON public."Proveedores" USING btree ("EmpresaId", "Nombre");


--
-- TOC entry 3221 (class 2606 OID 16877)
-- Name: Compras FK_Compras_Proveedores_ProveedorId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Compras"
    ADD CONSTRAINT "FK_Compras_Proveedores_ProveedorId" FOREIGN KEY ("ProveedorId") REFERENCES public."Proveedores"("Id") ON DELETE CASCADE;


--
-- TOC entry 3222 (class 2606 OID 16926)
-- Name: DetallesComprasProductos FK_DetallesComprasProductos_Compras_CompraId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."DetallesComprasProductos"
    ADD CONSTRAINT "FK_DetallesComprasProductos_Compras_CompraId" FOREIGN KEY ("CompraId") REFERENCES public."Compras"("Id") ON DELETE CASCADE;


--
-- TOC entry 3223 (class 2606 OID 16931)
-- Name: DetallesComprasProductos FK_DetallesComprasProductos_Productos_ProductoId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."DetallesComprasProductos"
    ADD CONSTRAINT "FK_DetallesComprasProductos_Productos_ProductoId" FOREIGN KEY ("ProductoId") REFERENCES public."Productos"("Id") ON DELETE CASCADE;


--
-- TOC entry 3224 (class 2606 OID 16914)
-- Name: DetallesVentasProductos FK_DetallesVentasProductos_Productos_ProductoId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."DetallesVentasProductos"
    ADD CONSTRAINT "FK_DetallesVentasProductos_Productos_ProductoId" FOREIGN KEY ("ProductoId") REFERENCES public."Productos"("Id") ON DELETE CASCADE;


--
-- TOC entry 3225 (class 2606 OID 16919)
-- Name: DetallesVentasProductos FK_DetallesVentasProductos_Ventas_VentaId; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."DetallesVentasProductos"
    ADD CONSTRAINT "FK_DetallesVentasProductos_Ventas_VentaId" FOREIGN KEY ("VentaId") REFERENCES public."Ventas"("Id") ON DELETE CASCADE;


-- Completed on 2023-06-29 09:03:01

--
-- PostgreSQL database dump complete
--

