drop table [dbo].[SESSION];
CREATE TABLE SESSION (
  id				INT						NOT NULL    IDENTITY    PRIMARY KEY,
  test_name			VARCHAR(100)			NOT NULL	UNIQUE , 
);

