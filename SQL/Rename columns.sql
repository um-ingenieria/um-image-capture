USE [UM_NEUROSKY];  
GO 
-- Cambios SP
/*EXEC sp_rename 'IAPS_ALL_SUBJECTS.valence_standard_deviation', 'valence_sd', 'COLUMN';  
EXEC sp_rename 'IAPS_ALL_SUBJECTS.arousal_standard_deviation', 'arousal_sd', 'COLUMN';  
EXEC sp_rename 'IAPS_FEMALE_SUBJECTS.valence_standard_deviation', 'valence_sd', 'COLUMN';  
EXEC sp_rename 'IAPS_FEMALE_SUBJECTS.arousal_standard_deviation', 'arousal_sd', 'COLUMN'; 
EXEC sp_rename 'IAPS_MALE_SUBJECTS.valence_standard_deviation', 'valence_sd', 'COLUMN';  
EXEC sp_rename 'IAPS_MALE_SUBJECTS.arousal_standard_deviation', 'arousal_sd', 'COLUMN'; 
GO*/

-- Cambios NOMBREPRUEBA
EXEC sp_rename 'NEUROSKY_IMAGENES.NOMBREPRUEBA', 'test_name', 'COLUMN';
EXEC sp_rename 'EXCITACION_VALENCIA.NAME_TEST', 'test_name', 'COLUMN';
GO

-- Cambios IDENTIFICADOR
EXEC sp_rename 'NEUROSKY_IMAGENES.IDENTIFICADOR', 'id', 'COLUMN';
EXEC sp_rename 'FACE_EMOTION.IDENTIFICADOR', 'image_id', 'COLUMN';
GO

-- Cambios IDENTIFICADOR
EXEC sp_rename 'EXCITACION_VALENCIA.SECCION', 'session_id', 'COLUMN';
EXEC sp_rename 'FACE_EMOTION.SECCION', 'session_id', 'COLUMN';
EXEC sp_rename 'NEUROSKY_IMAGENES.SECCION', 'session_id', 'COLUMN';
EXEC sp_rename 'SKIN_STATISTIC.secction', 'session_id', 'COLUMN';
EXEC sp_rename 'PULSE_STATISTIC.secction', 'session_id', 'COLUMN';
GO