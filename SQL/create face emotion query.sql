USE [UM_NEUROSKY]
GO

DROP TABLE [dbo].[FACE_EMOTION]

CREATE TABLE [dbo].[FACE_EMOTION](
	[SECCION] [float] NULL,
	[IDENTIFICADOR] [float] NULL,
	[ANGER] [float] NULL,
	[CONTEMPT] [float] NULL,
	[DISGUST] [float] NULL,
	[FEAR] [float] NULL,
	[HAPPINESS] [float] NULL,
	[NEUTRAL] [float] NULL,
	[SADNESS] [float] NULL,
	[SURPRISE] [float] NULL,
	[VALENCE] [float] NULL
) ON [PRIMARY]
GO


