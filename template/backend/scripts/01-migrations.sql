﻿CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TABLE "Users" (
    "Id" uuid NOT NULL DEFAULT (gen_random_uuid()),
    "Username" character varying(50) NOT NULL,
    "Password" character varying(100) NOT NULL,
    "Phone" character varying(20) NOT NULL,
    "Email" character varying(100) NOT NULL,
    "Status" character varying(20) NOT NULL,
    "Role" character varying(20) NOT NULL,
    CONSTRAINT "PK_Users" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20241014011203_InitialMigrations', '8.0.10');

COMMIT;

START TRANSACTION;

ALTER TABLE "Users" ADD "CreatedAt" timestamp with time zone NOT NULL DEFAULT TIMESTAMPTZ '-infinity';

ALTER TABLE "Users" ADD "UpdatedAt" timestamp with time zone;

CREATE TABLE "Sales" (
    "Id" uuid NOT NULL,
    "Number" bigint GENERATED BY DEFAULT AS IDENTITY,
    "TotalValue" numeric(18,2) NOT NULL,
    "CustomerId" uuid NOT NULL,
    "CustomerName" character varying(100) NOT NULL,
    "BranchId" uuid NOT NULL,
    "BranchName" character varying(100) NOT NULL,
    "Status" character varying(20) NOT NULL,
    "UpdatedAt" timestamp with time zone,
    "CreatedAt" timestamp with time zone NOT NULL,
    CONSTRAINT "PK_Sales" PRIMARY KEY ("Id")
);

CREATE TABLE "SaleItems" (
    "SaleId" uuid NOT NULL,
    "ProductId" uuid NOT NULL,
    "ProductName" character varying(500) NOT NULL,
    "UnitPrice" numeric(10,2) NOT NULL,
    "Quantity" smallint NOT NULL,
    "Discount" numeric(10,2) NOT NULL,
    "Cancelled" boolean NOT NULL,
    CONSTRAINT "PK_SaleItems" PRIMARY KEY ("SaleId", "ProductId"),
    CONSTRAINT "FK_SaleItems_Sales_SaleId" FOREIGN KEY ("SaleId") REFERENCES "Sales" ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20250510032334_salesMigration', '8.0.10');

COMMIT;

