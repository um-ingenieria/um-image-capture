USE [UM_NEUROSKY]
GO
/****** Object:  StoredProcedure [dbo].[SP_CargarDatos]    Script Date: 8/23/2020 3:18:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROCEDURE [dbo].[SP_CargarDatos] @Name nvarchar(30)
AS
declare @localhost nvarchar(40)

set @localhost = 'http://localhost/imagenes/public/imagen/'

truncate table [dbo].[TABLA_DATOS_COMPLETOS]

insert into [dbo].[TABLA_DATOS_COMPLETOS]
select 
	[NOMBREPRUEBA], 
	[SESSION_ID], 
	[IDENTIFICADOR],
	substring(DATE,12,8),
	[IMAGENESCRITORIO],
	substring([IMAGENESCRITORIO],40,33),
	CONCAT(@localhost,substring([IMAGENESCRITORIO],40,33)) LocalhostEscritorio,
	[IMAGENWEBCAM],
	substring([IMAGENWEBCAM],40,33) ,
	CONCAT(@localhost,substring([IMAGENWEBCAM],40,33)) LocalhostWeb,
	substring(HORARIO_DE_PC,12,8),
	[TEST_NAME] ,
	[TEST_COMMENT], 
	[PORT] ,
	[ROW_TIME], 
	[POORSIGNAL],
	case when ([ATTENTION] > 100) then 100 else [ATTENTION] end ,
	case when ([MEDITATION] > 100) then 100 else [MEDITATION] end ,
	[EGGPOWER] ,
	[EegPowerDelta] ,
	[EegPowerTheta],
	[EegPowerAlpha1],
	[EegPowerAlpha2], 
	[EegPowerBeta1] ,
	[EegPowerBeta2] ,
	[EegPowerGamma1] ,
	[EegPowerGamma2] 
	from [dbo].[NEUROSKY_IMAGENES] Capturas
	join [dbo].[NEUROSKY_DATOS] Mindwave on
	(Capturas.NOMBREPRUEBA = Mindwave.TEST_NAME and
	substring(Capturas.DATE,1,19) = substring(Mindwave.HORARIO_DE_PC,1,19))
	where [NOMBREPRUEBA] = @NAME AND SESSION_ID =  (SELECT MAX(SESSION_ID) FROM [NEUROSKY_IMAGENES] WHERE [NOMBREPRUEBA] = @Name)
	
	
