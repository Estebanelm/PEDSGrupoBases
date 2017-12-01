SELECT * FROM	dbo.Publicacion

UPDATE dbo.Publicacion SET activo = 1 WHERE	id = 7

DELETE FROM dbo.Publicacion WHERE id = 5

SELECT * FROM dbo.Documento

DELETE FROM dbo.Documento

SELECT * FROM dbo.Tutoria

UPDATE dbo.Publicacion SET isTutoria = 1

SELECT * FROM dbo.Universidad

SELECT * FROM dbo.Comentario

SELECT * FROM dbo.Contenido

SELECT * FROM dbo.Tecnologia_x_publicacion

DELETE FROM dbo.Contenido

INSERT INTO dbo.Contenido
(
    id_publicacion,
    enlace_video,
    enlace_extra,
    id_documento
)
VALUES
(   7,  -- id_publicacion - int
    '', -- enlace_video - varchar(300)
    'ejemplo.com/enlace', -- enlace_extra - varchar(300)
    5   -- id_documento - int
    )

	SELECT * FROM dbo.Estudiante

SELECT * FROM dbo.Usuario

DELETE FROM dbo.Usuario WHERE id in ('20150809', '20150908')

DELETE FROM dbo.Estudiante WHERE id_usuario in ('20150809', '20150908')