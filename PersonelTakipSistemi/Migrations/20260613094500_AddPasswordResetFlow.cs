using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using PersonelTakipSistemi.Data;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    [DbContext(typeof(TegmPersonelTakipDbContext))]
    [Migration("20260613094500_AddPasswordResetFlow")]
    public partial class AddPasswordResetFlow : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                IF COL_LENGTH('dbo.Personeller', 'SifreSifirlamaGerekli') IS NULL
                BEGIN
                    ALTER TABLE dbo.Personeller ADD SifreSifirlamaGerekli bit NOT NULL CONSTRAINT DF_Personeller_SifreSifirlamaGerekli DEFAULT(0);
                END;

                IF COL_LENGTH('dbo.Personeller', 'SifreSifirlamaTarihi') IS NULL
                BEGIN
                    ALTER TABLE dbo.Personeller ADD SifreSifirlamaTarihi datetime2 NULL;
                END;

                IF COL_LENGTH('dbo.Personeller', 'SifreSifirlayanPersonelId') IS NULL
                BEGIN
                    ALTER TABLE dbo.Personeller ADD SifreSifirlayanPersonelId int NULL;
                END;

                IF COL_LENGTH('dbo.Personeller', 'SifreSonDegistirmeTarihi') IS NULL
                BEGIN
                    ALTER TABLE dbo.Personeller ADD SifreSonDegistirmeTarihi datetime2 NULL;
                END;

                EXEC(N'
                UPDATE dbo.Personeller
                SET SifreSifirlamaGerekli = 1,
                    SifreSifirlamaTarihi = COALESCE(SifreSifirlamaTarihi, GETDATE()),
                    UpdatedAt = GETDATE()
                WHERE AktifMi = 1;
                ');
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                IF COL_LENGTH('dbo.Personeller', 'SifreSonDegistirmeTarihi') IS NOT NULL
                    ALTER TABLE dbo.Personeller DROP COLUMN SifreSonDegistirmeTarihi;

                IF COL_LENGTH('dbo.Personeller', 'SifreSifirlayanPersonelId') IS NOT NULL
                    ALTER TABLE dbo.Personeller DROP COLUMN SifreSifirlayanPersonelId;

                IF COL_LENGTH('dbo.Personeller', 'SifreSifirlamaTarihi') IS NOT NULL
                    ALTER TABLE dbo.Personeller DROP COLUMN SifreSifirlamaTarihi;

                IF COL_LENGTH('dbo.Personeller', 'SifreSifirlamaGerekli') IS NOT NULL
                BEGIN
                    DECLARE @constraintName sysname;
                    SELECT @constraintName = dc.name
                    FROM sys.default_constraints dc
                    INNER JOIN sys.columns c ON c.default_object_id = dc.object_id
                    WHERE dc.parent_object_id = OBJECT_ID('dbo.Personeller')
                      AND c.name = 'SifreSifirlamaGerekli';

                    IF @constraintName IS NOT NULL
                        EXEC(N'ALTER TABLE dbo.Personeller DROP CONSTRAINT [' + @constraintName + N']');

                    ALTER TABLE dbo.Personeller DROP COLUMN SifreSifirlamaGerekli;
                END;
                """);
        }
    }
}
