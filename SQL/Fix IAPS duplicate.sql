/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP (1000) COUNT([id_iaps])
      ,[id_iaps]
  FROM [UM_NEUROSKY].[dbo].[IAPS_ALL_SUBJECTS] group by id_iaps having COUNT([id_iaps])>1;

SELECT TOP (1000) set_id
      ,[id_iaps]
FROM [UM_NEUROSKY].[dbo].[IAPS_ALL_SUBJECTS] where id_iaps in (
1230,
1590,
1640,
1670,
2210,
3000,
3010,
4220,
4520,
6200,
9090);

delete from [UM_NEUROSKY].[dbo].[IAPS_ALL_SUBJECTS] where id_iaps in (
1230,
1590,
1640,
1670,
2210,
3000,
3010,
4220,
4520,
6200,
9090) and (set_id=2 OR set_id = 4);