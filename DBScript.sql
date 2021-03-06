USE [master]
GO
/****** Object:  Database [DigiTutorDB]    Script Date: 11/27/2017 7:05:43 PM ******/
CREATE DATABASE [DigiTutorDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DigiTutorDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\DigiTutorDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DigiTutorDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL14.SQLEXPRESS\MSSQL\DATA\DigiTutorDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [DigiTutorDB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DigiTutorDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DigiTutorDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DigiTutorDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DigiTutorDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DigiTutorDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DigiTutorDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [DigiTutorDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DigiTutorDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DigiTutorDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DigiTutorDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DigiTutorDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DigiTutorDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DigiTutorDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DigiTutorDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DigiTutorDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DigiTutorDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DigiTutorDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DigiTutorDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DigiTutorDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DigiTutorDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DigiTutorDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DigiTutorDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DigiTutorDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DigiTutorDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [DigiTutorDB] SET  MULTI_USER 
GO
ALTER DATABASE [DigiTutorDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DigiTutorDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DigiTutorDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DigiTutorDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DigiTutorDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DigiTutorDB] SET QUERY_STORE = OFF
GO
USE [DigiTutorDB]
GO
ALTER DATABASE SCOPED CONFIGURATION SET IDENTITY_CACHE = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET LEGACY_CARDINALITY_ESTIMATION = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET MAXDOP = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET PARAMETER_SNIFFING = PRIMARY;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION FOR SECONDARY SET QUERY_OPTIMIZER_HOTFIXES = PRIMARY;
GO
USE [DigiTutorDB]
GO
/****** Object:  Table [dbo].[Apoyo]    Script Date: 11/27/2017 7:05:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Apoyo](
	[id_estudianteApoyado] [varchar](30) NOT NULL,
	[id_estudianteDaApoyo] [varchar](30) NOT NULL,
	[id_tecnologia] [int] NOT NULL,
	[fecha] [date] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categoria]    Script Date: 11/27/2017 7:05:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categoria](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Categoria] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Comentario]    Script Date: 11/27/2017 7:05:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comentario](
	[id_estudiante] [varchar](30) NOT NULL,
	[id_publicacion] [int] NOT NULL,
	[contenido] [varchar](300) NOT NULL,
	[fecha_creacion] [date] NOT NULL,
	[activo] [bit] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contenido]    Script Date: 11/27/2017 7:05:43 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contenido](
	[id_publicacion] [int] NOT NULL,
	[enlace_video] [varchar](300) NULL,
	[enlace_extra] [varchar](300) NULL,
	[id_documento] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Documento]    Script Date: 11/27/2017 7:05:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Documento](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[tipo] [varchar](20) NOT NULL,
	[tamano] [int] NOT NULL,
	[contenido] [varchar](300) NOT NULL,
 CONSTRAINT [PK_Documento] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estudiante]    Script Date: 11/27/2017 7:05:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estudiante](
	[id_usuario] [varchar](30) NOT NULL,
	[id_pais] [int] NOT NULL,
	[id_universidad] [int] NOT NULL,
	[telefono_celular] [varchar](10) NOT NULL,
	[telefono_fijo] [varchar](10) NULL,
	[correo_secundario] [varchar](30) NULL,
	[foto] [varchar](200) NULL,
	[descripcion] [varchar](300) NULL,
	[reputacion] [int] NOT NULL,
	[participacion] [int] NOT NULL,
	[apoyos_disponibles] [int] NOT NULL,
	[numero_seguidores] [int] NOT NULL,
 CONSTRAINT [PK_Estudiante] PRIMARY KEY CLUSTERED 
(
	[id_usuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Estudiante_sigue_Estudiante]    Script Date: 11/27/2017 7:05:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Estudiante_sigue_Estudiante](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_estudianteSeguido] [varchar](30) NOT NULL,
	[id_estudianteSeguidor] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Estudiante_sigue_Estudiante] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Evaluacion]    Script Date: 11/27/2017 7:05:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Evaluacion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_publicacion] [int] NOT NULL,
	[id_estudiante] [varchar](30) NOT NULL,
	[positiva] [bit] NULL,
 CONSTRAINT [PK_Evaluacion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pais]    Script Date: 11/27/2017 7:05:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pais](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Pais] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Publicacion]    Script Date: 11/27/2017 7:05:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Publicacion](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_estudiante] [varchar](30) NOT NULL,
	[titulo] [varchar](30) NOT NULL,
	[descripcion] [varchar](300) NULL,
	[evaluaciones_negativas] [int] NOT NULL,
	[evaluaciones_positivas] [int] NOT NULL,
	[fecha_publicacion] [date] NOT NULL,
	[activo] [bit] NOT NULL,
 CONSTRAINT [PK_Publicacion] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RegistroTutoria]    Script Date: 11/27/2017 7:05:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RegistroTutoria](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_estudiante] [varchar](30) NOT NULL,
	[id_tutoria] [int] NOT NULL,
 CONSTRAINT [PK_RegistroTutoria] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tecnologia]    Script Date: 11/27/2017 7:05:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tecnologia](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](30) NOT NULL,
	[id_categoria] [int] NOT NULL,
 CONSTRAINT [PK_Tecnologia] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tecnologia_x_Estudiante]    Script Date: 11/27/2017 7:05:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tecnologia_x_Estudiante](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[id_tecnologia] [int] NOT NULL,
	[id_estudiante] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Tecnologia_x_Estudiante] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tutoria]    Script Date: 11/27/2017 7:05:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tutoria](
	[id_publicacion] [int] NOT NULL,
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fecha_tutoria] [date] NOT NULL,
	[lugar] [varchar](30) NOT NULL,
	[costo] [varchar](30) NULL,
 CONSTRAINT [PK_Tutoria] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Universidad]    Script Date: 11/27/2017 7:05:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Universidad](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [varchar](30) NOT NULL,
 CONSTRAINT [PK_Universidad] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuario]    Script Date: 11/27/2017 7:05:44 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuario](
	[id] [varchar](30) NOT NULL,
	[contrasena] [varchar](8) NOT NULL,
	[fecha_creacion] [date] NOT NULL,
	[nombre] [varchar](30) NOT NULL,
	[apellido] [varchar](30) NOT NULL,
	[correo_principal] [varchar](30) NOT NULL,
	[activo] [bit] NOT NULL,
	[is_admin] [bit] NOT NULL,
	[id_generado] [int] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Comentario] ADD  CONSTRAINT [DF_Comentario_activo]  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[Estudiante] ADD  CONSTRAINT [DF_Estudiante_reputacion]  DEFAULT ((0)) FOR [reputacion]
GO
ALTER TABLE [dbo].[Estudiante] ADD  CONSTRAINT [DF_Estudiante_participacion]  DEFAULT ((0)) FOR [participacion]
GO
ALTER TABLE [dbo].[Estudiante] ADD  CONSTRAINT [DF_Estudiante_apoyos_disponibles]  DEFAULT ((5)) FOR [apoyos_disponibles]
GO
ALTER TABLE [dbo].[Estudiante] ADD  CONSTRAINT [DF_Estudiante_cantidad_seguidores]  DEFAULT ((0)) FOR [numero_seguidores]
GO
ALTER TABLE [dbo].[Publicacion] ADD  CONSTRAINT [DF_Publicacion_evaluaciones_negativas]  DEFAULT ((0)) FOR [evaluaciones_negativas]
GO
ALTER TABLE [dbo].[Publicacion] ADD  CONSTRAINT [DF_Publicacion_evaluaciones_positivas]  DEFAULT ((0)) FOR [evaluaciones_positivas]
GO
ALTER TABLE [dbo].[Publicacion] ADD  CONSTRAINT [DF_Publicacion_activo]  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_activo]  DEFAULT ((1)) FOR [activo]
GO
ALTER TABLE [dbo].[Usuario] ADD  CONSTRAINT [DF_Usuario_is_admin]  DEFAULT ((0)) FOR [is_admin]
GO
ALTER TABLE [dbo].[Apoyo]  WITH CHECK ADD  CONSTRAINT [FK_Apoyado] FOREIGN KEY([id_estudianteApoyado])
REFERENCES [dbo].[Estudiante] ([id_usuario])
GO
ALTER TABLE [dbo].[Apoyo] CHECK CONSTRAINT [FK_Apoyado]
GO
ALTER TABLE [dbo].[Apoyo]  WITH CHECK ADD  CONSTRAINT [FK_Apoyo_Tecnologia] FOREIGN KEY([id_tecnologia])
REFERENCES [dbo].[Tecnologia] ([id])
GO
ALTER TABLE [dbo].[Apoyo] CHECK CONSTRAINT [FK_Apoyo_Tecnologia]
GO
ALTER TABLE [dbo].[Apoyo]  WITH CHECK ADD  CONSTRAINT [FK_DaApoyo] FOREIGN KEY([id_estudianteDaApoyo])
REFERENCES [dbo].[Estudiante] ([id_usuario])
GO
ALTER TABLE [dbo].[Apoyo] CHECK CONSTRAINT [FK_DaApoyo]
GO
ALTER TABLE [dbo].[Comentario]  WITH CHECK ADD  CONSTRAINT [FK_Comentario_Estudiante] FOREIGN KEY([id_estudiante])
REFERENCES [dbo].[Estudiante] ([id_usuario])
GO
ALTER TABLE [dbo].[Comentario] CHECK CONSTRAINT [FK_Comentario_Estudiante]
GO
ALTER TABLE [dbo].[Comentario]  WITH CHECK ADD  CONSTRAINT [FK_Comentario_Publicacion] FOREIGN KEY([id_publicacion])
REFERENCES [dbo].[Publicacion] ([id])
GO
ALTER TABLE [dbo].[Comentario] CHECK CONSTRAINT [FK_Comentario_Publicacion]
GO
ALTER TABLE [dbo].[Contenido]  WITH CHECK ADD  CONSTRAINT [FK_Contenido_Documento] FOREIGN KEY([id_documento])
REFERENCES [dbo].[Documento] ([id])
GO
ALTER TABLE [dbo].[Contenido] CHECK CONSTRAINT [FK_Contenido_Documento]
GO
ALTER TABLE [dbo].[Contenido]  WITH CHECK ADD  CONSTRAINT [FK_Contenido_Publicacion] FOREIGN KEY([id_publicacion])
REFERENCES [dbo].[Publicacion] ([id])
GO
ALTER TABLE [dbo].[Contenido] CHECK CONSTRAINT [FK_Contenido_Publicacion]
GO
ALTER TABLE [dbo].[Estudiante]  WITH CHECK ADD  CONSTRAINT [FK_Estudiante_Pais] FOREIGN KEY([id_pais])
REFERENCES [dbo].[Pais] ([id])
GO
ALTER TABLE [dbo].[Estudiante] CHECK CONSTRAINT [FK_Estudiante_Pais]
GO
ALTER TABLE [dbo].[Estudiante]  WITH CHECK ADD  CONSTRAINT [FK_Estudiante_Universidad] FOREIGN KEY([id_universidad])
REFERENCES [dbo].[Universidad] ([id])
GO
ALTER TABLE [dbo].[Estudiante] CHECK CONSTRAINT [FK_Estudiante_Universidad]
GO
ALTER TABLE [dbo].[Estudiante]  WITH CHECK ADD  CONSTRAINT [FK_Estudiante_Usuario] FOREIGN KEY([id_usuario])
REFERENCES [dbo].[Usuario] ([id])
GO
ALTER TABLE [dbo].[Estudiante] CHECK CONSTRAINT [FK_Estudiante_Usuario]
GO
ALTER TABLE [dbo].[Estudiante_sigue_Estudiante]  WITH CHECK ADD  CONSTRAINT [FK_Seguido] FOREIGN KEY([id_estudianteSeguido])
REFERENCES [dbo].[Estudiante] ([id_usuario])
GO
ALTER TABLE [dbo].[Estudiante_sigue_Estudiante] CHECK CONSTRAINT [FK_Seguido]
GO
ALTER TABLE [dbo].[Estudiante_sigue_Estudiante]  WITH CHECK ADD  CONSTRAINT [FK_Seguidor] FOREIGN KEY([id_estudianteSeguidor])
REFERENCES [dbo].[Estudiante] ([id_usuario])
GO
ALTER TABLE [dbo].[Estudiante_sigue_Estudiante] CHECK CONSTRAINT [FK_Seguidor]
GO
ALTER TABLE [dbo].[Evaluacion]  WITH CHECK ADD  CONSTRAINT [FK_Evaluacion_Estudiante] FOREIGN KEY([id_estudiante])
REFERENCES [dbo].[Estudiante] ([id_usuario])
GO
ALTER TABLE [dbo].[Evaluacion] CHECK CONSTRAINT [FK_Evaluacion_Estudiante]
GO
ALTER TABLE [dbo].[Evaluacion]  WITH CHECK ADD  CONSTRAINT [FK_Evaluacion_Publicacion] FOREIGN KEY([id_publicacion])
REFERENCES [dbo].[Publicacion] ([id])
GO
ALTER TABLE [dbo].[Evaluacion] CHECK CONSTRAINT [FK_Evaluacion_Publicacion]
GO
ALTER TABLE [dbo].[Publicacion]  WITH CHECK ADD  CONSTRAINT [FK_Publicacion_Estudiante] FOREIGN KEY([id_estudiante])
REFERENCES [dbo].[Estudiante] ([id_usuario])
GO
ALTER TABLE [dbo].[Publicacion] CHECK CONSTRAINT [FK_Publicacion_Estudiante]
GO
ALTER TABLE [dbo].[RegistroTutoria]  WITH CHECK ADD  CONSTRAINT [FK_RegistroTutoria_Estudiante] FOREIGN KEY([id_estudiante])
REFERENCES [dbo].[Estudiante] ([id_usuario])
GO
ALTER TABLE [dbo].[RegistroTutoria] CHECK CONSTRAINT [FK_RegistroTutoria_Estudiante]
GO
ALTER TABLE [dbo].[RegistroTutoria]  WITH CHECK ADD  CONSTRAINT [FK_RegistroTutoria_Tutoria] FOREIGN KEY([id_tutoria])
REFERENCES [dbo].[Tutoria] ([id])
GO
ALTER TABLE [dbo].[RegistroTutoria] CHECK CONSTRAINT [FK_RegistroTutoria_Tutoria]
GO
ALTER TABLE [dbo].[Tecnologia]  WITH CHECK ADD  CONSTRAINT [FK_Tecnologia_Categoria] FOREIGN KEY([id_categoria])
REFERENCES [dbo].[Categoria] ([id])
GO
ALTER TABLE [dbo].[Tecnologia] CHECK CONSTRAINT [FK_Tecnologia_Categoria]
GO
ALTER TABLE [dbo].[Tecnologia_x_Estudiante]  WITH CHECK ADD  CONSTRAINT [FK_Tecnologia_x_Estudiante_Estudiante] FOREIGN KEY([id_estudiante])
REFERENCES [dbo].[Estudiante] ([id_usuario])
GO
ALTER TABLE [dbo].[Tecnologia_x_Estudiante] CHECK CONSTRAINT [FK_Tecnologia_x_Estudiante_Estudiante]
GO
ALTER TABLE [dbo].[Tecnologia_x_Estudiante]  WITH CHECK ADD  CONSTRAINT [FK_Tecnologia_x_Estudiante_Tecnologia] FOREIGN KEY([id_tecnologia])
REFERENCES [dbo].[Tecnologia] ([id])
GO
ALTER TABLE [dbo].[Tecnologia_x_Estudiante] CHECK CONSTRAINT [FK_Tecnologia_x_Estudiante_Tecnologia]
GO
ALTER TABLE [dbo].[Tutoria]  WITH CHECK ADD  CONSTRAINT [FK_Tutoria_Publicacion] FOREIGN KEY([id_publicacion])
REFERENCES [dbo].[Publicacion] ([id])
GO
ALTER TABLE [dbo].[Tutoria] CHECK CONSTRAINT [FK_Tutoria_Publicacion]
GO
USE [master]
GO
ALTER DATABASE [DigiTutorDB] SET  READ_WRITE 
GO
