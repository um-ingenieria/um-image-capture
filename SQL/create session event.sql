drop table [dbo].[SESSION_EVENT];
CREATE TABLE SESSION_EVENT (
  id				INT						NOT NULL    IDENTITY    PRIMARY KEY,
  session_id		INT						NOT NULL,
  test_name			VARCHAR(100)			NOT NULL, 
  test_event		VARCHAR(100)			NOT NULL, 
  event_date		DATETIME2(3)			NOT NULL,  
);
