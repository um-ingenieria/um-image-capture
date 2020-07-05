drop table [dbo].[SKIN_STATISTIC];
CREATE TABLE SKIN_STATISTIC (
  id				INT				NOT NULL    IDENTITY    PRIMARY KEY,
  secction			INT				NOT NULL, 
  identificador		INT				NOT NULL,
  relative_time		float			NOT NULL,
  micro_siemens		float,
  absolute_time		DATETIME2(3),
  scr				float,
  scr_min			float,
);