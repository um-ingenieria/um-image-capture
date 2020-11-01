/****** Script for SelectTopNRows command from SSMS  ******/


  update [UM_NEUROSKY].[dbo].[SESSION_EVENT] set event_date = DATEADD(DAY, 2, event_date) where session_id = 3066;

  SELECT TOP (1000) [id]
      ,[session_id]
      ,[test_name]
      ,[test_event]
      ,[STIMULI_TYPE]
      ,[STIMULI_ID]
      ,[event_date]
  FROM [UM_NEUROSKY].[dbo].[SESSION_EVENT] where session_id = 3066;