drop table [dbo].[PULSE_STATISTIC];
CREATE TABLE PULSE_STATISTIC (
  id				INT				NOT NULL    IDENTITY    PRIMARY KEY,
  secction			INT				NOT NULL, 
  identificador		INT				NOT NULL,
  relative_time		float			NOT NULL,
  hr				float,
  rr				float,
  hrv				float,
  uniformity		float,
  absolute_time		DATETIME2(3),
  score				INT,
);

